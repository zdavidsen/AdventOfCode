var input = $("body > pre").innerText
var lines = input.split("\n")
lines.pop()

var zeroMask = []
var oneMask = []

var memory = {}

for (var i = 0; i < lines.length; i++)
{
	var match = /mask = ([01X]{36})/.exec(lines[i])
	
	if (match != null)
	{
		var mask = match[1]
		
		for (var j = 0; j < 36; j++)
		{
			zeroMask[35 - j] = mask[j] != 0
			oneMask[35 - j] = mask[j] == 1
		}
		
		continue;
	}
	
	match = /mem\[(\d+)\] = (\d+)/
	
	var addr = mem
}