using System.Diagnostics;
using System.Numerics;
using System.Text;

namespace AoC2022
{
  [AoCChallenge(16, 2021)]
  public class Day16 : IAoCChallenge
  {
    public bool Part2Ready => true;

    public string RunPart1(IEnumerable<string> input)
    {
      var transmission = input.First().Trim();
      var nibbles = transmission.Select(c => (byte)int.Parse(c.ToString(), System.Globalization.NumberStyles.HexNumber)).ToArray();

      var bitsRead = ParsePacket(nibbles, 0, out Packet packet);

      //Debug.WriteLine("Comparing input to re-generated output:");
      //Debug.WriteLine(String.Join("", nibbles.Select(n => Convert.ToString(n, 2).PadLeft(4, '0'))));
      //Debug.WriteLine(GenerateBinaryPacket(packet, false));
      //Debug.WriteLine(GenerateBinaryPacket(packet, true));

      long sum = SumVersions(packet);

      return $"{sum}";

      long SumVersions(Packet packet)
      {
        if (packet.SubPackets == null)
          return packet.Version;

        return packet.SubPackets.Aggregate((long)packet.Version, (sum, packet) => sum + SumVersions(packet));
      }
    }

    private uint ParsePacket(Span<byte> nibbles, uint bitOffset, out Packet packet)
    {
      // Pretty sure this is a terrible idea, but I'm already committed so I'll see it through
      if (bitOffset >= 4)
        throw new Exception("Requested too many bits!");

      uint consumedBits = 0;
      uint workingValue = nibbles[0];
      uint workingBits = 4 - bitOffset;
      int nextByteIndex = 1;

      uint mask = 0xFFFFFFFF;
      mask = mask >> (int)(32 - workingBits);
      workingValue = workingValue & mask;

      var version = ConsumeBits(3, nibbles);
      var typeId = ConsumeBits(3, nibbles);

      packet =
        new Packet
        {
          Version = version,
          TypeId = typeId
        };

      if (typeId == 4)
      {
        BigInteger value = 0;

        while (true)
        {
          uint nibble = ConsumeBits(5, nibbles);

          var terminal = (nibble & 0b00010000) == 0;
          nibble = nibble & 0b00001111;

          // So apparently shift operations don't trigger overflow exceptions, which I guess makes
          //  sense, but man this was a pain to track down
          value = (value << 4) + nibble;

          if (terminal)
            break;
        }

        packet = packet with { Value = value, BitLength = consumedBits };

        return consumedBits;
      }
      else
      {
        var lengthType = ConsumeBits(1, nibbles);

        packet = packet with { SubPackets = new List<Packet>(), LengthType = lengthType };

        if (lengthType == 0)
        {
          var subLength = ConsumeBits(15, nibbles);

          packet = packet with { SubLength = subLength };

          uint consumed = 0;

          while (consumed < subLength)
          {
            // Implicit assumption that integer division floors
            var newSpan = nibbles.Slice((int)(consumedBits + bitOffset) / 4);
            var newOffset = (consumedBits + bitOffset) % 4;
            var readBits = ParsePacket(newSpan, newOffset, out Packet sub);
            consumed += readBits;
            consumedBits += readBits;
            packet = packet with { SubPackets = packet.SubPackets.Append(sub) };
          }
        }
        else if (lengthType == 1)
        {
          var numPackets = ConsumeBits(11, nibbles);

          for (int i = 0; i < numPackets; i++)
          {
            var newSpan = nibbles.Slice((int)(consumedBits + bitOffset) / 4);
            var newOffset = (consumedBits + bitOffset) % 4;
            var readBits = ParsePacket(newSpan, newOffset, out Packet sub);
            consumedBits += readBits;
            packet = packet with { SubPackets = packet.SubPackets.Append(sub) };
          }
        }
        else
        {
          throw new Exception($"Unexpected length type ID: '{lengthType}'");
        }

        packet = packet with { BitLength = consumedBits };

        return consumedBits;
      }

      //void ExtendWorkingValue(Span<byte> bytes)
      //{
      //  workingValue = (workingValue << 8) + bytes[nextByteIndex++];
      //  workingBits += 8;
      //}

      uint ConsumeBits(uint count, Span<byte> nibbles)
      {
        while (count > workingBits)
        {
          workingValue = (workingValue << 4) + nibbles[nextByteIndex++];
          workingBits += 4;
        }
        uint mask = 0xFFFFFFFF;
        mask = mask >> (int)(32 - count);
        mask = mask << (int)(workingBits - count);
        uint value = workingValue & mask;
        workingValue = workingValue ^ value;
        value = value >> (int)(workingBits - count);
        workingBits -= count;
        consumedBits += count;
        return value;
      }
    }

    private string GenerateBinaryPacket(Packet packet, bool showAnnotations)
    {
      StringBuilder sb = new();

      if (showAnnotations) sb.Append('{');
      sb.Append(Convert.ToString(packet.Version, 2).PadLeft(3, '0'));
      if (showAnnotations) sb.Append('|');
      sb.Append(Convert.ToString(packet.TypeId, 2).PadLeft(3, '0'));
      if (showAnnotations) sb.Append('_');

      if (packet.TypeId == 4)
      {
        if (packet.Value == null)
          throw new Exception("Ill formed packet: value packet does not have a value");
        var nibbles = new List<byte>();
        var value = packet.Value;
        while (true)
        {
          var nibble = (byte)(value & 0b00001111)!;
          value >>= 4;
          nibbles.Add(nibble);
          if (value == 0)
            break;
        }
        for (int i = nibbles.Count - 1; i >= 1; i--)
        {
          sb.Append('1').Append(Convert.ToString(nibbles[i], 2).PadLeft(4, '0'));
          if (showAnnotations) sb.Append('+');
        }
        sb.Append('0').Append(Convert.ToString(nibbles[0], 2).PadLeft(4, '0'));

        //Debug.Write(sb.ToString());
      }
      else
      {
        sb.Append(Convert.ToString(packet.LengthType, 2));
        if (showAnnotations) sb.Append('-');

        //Debug.Write(sb.ToString());

        if (packet.LengthType > 1)
        {
          throw new Exception("Operator packet does not have a valid length type");
        }

        if (packet.SubPackets == null || packet.SubPackets.Count() == 0)
          throw new Exception("Operator packet must have sub packets");

        if (packet.LengthType == 0)
        {
          sb.Append(Convert.ToString(packet.SubLength.Value, 2).PadLeft(15, '0'));
        }
        else if (packet.LengthType == 1)
        {
          sb.Append(Convert.ToString(packet.SubPackets.Count(), 2).PadLeft(11, '0'));
        }

        foreach (var subP in packet.SubPackets)
        {
          var sub = GenerateBinaryPacket(subP, showAnnotations);
          sb.Append(sub);
          //Debug.Write(sub);
        }
      }

      //Debug.WriteLine("");

      if (showAnnotations) sb.Append('}');
      return sb.ToString();
    }

    public string RunPart2(IEnumerable<string> input)
    {
      var transmission = input.First().Trim();
      var nibbles = transmission.Select(c => (byte)int.Parse(c.ToString(), System.Globalization.NumberStyles.HexNumber)).ToArray();

      var bitsRead = ParsePacket(nibbles, 0, out Packet packet);

      var recv = string.Join("", nibbles.Select(n => Convert.ToString(n, 2).PadLeft(4, '0')));
      var parsed = GenerateBinaryPacket(packet, false);

      if (parsed.TrimEnd('0') != recv.TrimEnd('0'))
      {
        Debug.WriteLine("Comparing input to re-generated output:");
        Debug.WriteLine(string.Join("", nibbles.Select(n => Convert.ToString(n, 2).PadLeft(4, '0'))));
        Debug.WriteLine(GenerateBinaryPacket(packet, false));
        Debug.WriteLine(GenerateBinaryPacket(packet, true));
        Debug.WriteLine("");
      }

      BigInteger value = EvaluatePacket(packet);

      return $"{value}";

      BigInteger EvaluatePacket(Packet packet)
      {
        switch (packet.TypeId)
        {
          case 4:
            return packet.Value.Value;
          case 0:
            return packet.SubPackets.Aggregate(new BigInteger(0), (sum, p) => sum + EvaluatePacket(p));
          case 1:
            return packet.SubPackets.Aggregate(new BigInteger(1), (prod, p) => prod * EvaluatePacket(p));
          case 2:
            return packet.SubPackets.Min(p => EvaluatePacket(p));
          case 3:
            return packet.SubPackets.Max(p => EvaluatePacket(p));
          case 5:
            return EvaluatePacket(packet.SubPackets.ElementAt(0)) > EvaluatePacket(packet.SubPackets.ElementAt(1))
                    ? 1
                    : 0;
          case 6:
            return EvaluatePacket(packet.SubPackets.ElementAt(0)) < EvaluatePacket(packet.SubPackets.ElementAt(1))
                    ? 1
                    : 0;
          case 7:
            return EvaluatePacket(packet.SubPackets.ElementAt(0)) == EvaluatePacket(packet.SubPackets.ElementAt(1))
                    ? 1
                    : 0;
          default:
            throw new ArgumentOutOfRangeException();
        }
      }
    }

    private record Packet
    {
      //byte[] Raw;
      public uint Version;
      public uint TypeId;

      public BigInteger? Value;

      public uint LengthType;
      public uint? SubLength;
      public IEnumerable<Packet>? SubPackets;

      public uint BitLength;
    }
  }
}
