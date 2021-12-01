var input = $("body > pre").innerText
var lines = input.split("\n")
lines.pop()

var seats = []
var newSeats = []

for (var i = 0; i < lines.length; i++)
{
	seats.push([])
	newSeats.push([])
	
	for (var j = 0; j < lines[0].length; j++)
	{
		seats[i].push(lines[i][j])
		newSeats[i].push(lines[i][j])
	}
}

var updated = true;

while (updated)
{
	updated = false;
	for (var i = 0; i < seats.length; i++)
	{
		for (var j = 0; j < seats[0].length; j++)
		{
			var seat = seats[i][j]
			
			if (seat == ".")
				continue;
			
			var occupied = 0;
			
			for (var k = -1; k < 2; k++)
			{
				for (var l = -1; l < 2; l++)
				{
					if (k == 0 && l == 0)
						continue;
					
					for (var m = 1; ; m++)
					{
						var row = seats[i + k * m]
						if (row == undefined)
							break;
						var visible = row[j + l * m]
						if (visible == undefined)
							break;
						if (visible == "L")
							break;
						if (visible == "#")
						{
							occupied++;
							break;
						}
					}						
					
					// part 1
					// if (seats[i + k] == undefined)
						// continue;
					// if (seats[i + k][j + l] == "#")
						// occupied++
				}
			}
			
			if (seat == "#")
			{
				if (occupied >= 5)
				{
					newSeats[i][j] = "L"
					updated = true;
				}
				else
					newSeats[i][j] = "#"
			}
			else
			{
				if (occupied == 0)
				{
					newSeats[i][j] = "#"
					updated = true;
				}
				else
					newSeats[i][j] = "L"
			}
			
		}
	}
	var temp = seats;
	seats = newSeats;
	newSeats = temp;
}

var occupied = 0;

for (var i = 0; i < seats.length; i++)
{
	for (var j = 0; j < seats[0].length; j++)
	{
		if (seats[i][j] == "#")
			occupied++;
	}
}

console.log(occupied)