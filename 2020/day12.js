var input = $("body > pre").innerText
var lines = input.split("\n")
lines.pop()

var wx = 10, wy = 1;
var x = 0, y = 0;
var dir = 0;

var sin = 
	rad =>
	{
		return Math.round(Math.sin(rad * Math.PI / 180))
	}

var cos = 
	rad =>
	{
		return Math.round(Math.cos(rad * Math.PI / 180))
	}

for (var i = 0; i < lines.length; i++)
{
	var match = /([NSEWRLF])(\d+)/.exec(lines[i])
	var op = match[1]
	var num = Number(match[2])
	switch (op)
	{
		case "N":
			wy += num;
			break;
		case "S":
			wy -= num;
			break;
		case "E":
			wx += num;
			break;
		case "W":
			wx -= num;
			break;
		case "R":
			var newwx = cos(-num) * wx - sin(-num) * wy;
			wy = cos(-num) * wy + sin(-num) * wx;
			wx = newwx
			break;
		case "L":
			var newwx = cos(num) * wx - sin(num) * wy;
			wy = cos(num) * wy + sin(num) * wx;
			wx = newwx
			break;
		case "F":
			x += wx * num
			y += wy * num
			break;
	}	
}

console.log("x: " + x)
console.log("y: " + y);
console.log(Math.abs(x)+Math.abs(y))

// for (var i = 0; i < lines.length; i++)
// {
	// var match = /([NSEWRLF])(\d+)/.exec(lines[i])
	// var op = match[1]
	// var num = Number(match[2])
	// switch (op)
	// {
		// case "N":
			// y += num;
			// break;
		// case "S":
			// y -= num;
			// break;
		// case "E":
			// x += num;
			// break;
		// case "W":
			// x -= num;
			// break;
		// case "R":
			// dir -= num;
			// dir %= 360;
			// break;
		// case "L":
			// dir += num;
			// dir %= 360;
			// break;
		// case "F":
			// x += Math.round(num * Math.cos(dir * Math.PI / 180))
			// y += Math.round(num * Math.sin(dir * Math.PI / 180))
			// break;
	// }	
// }

// console.log("x: " + x)
// console.log("y: " + y);
// console.log(Math.abs(x)+Math.abs(y))