var input = $("body > pre").innerText
var lines = input.split("\n")
// lines.pop()

var arrival = Number(lines[0])

var buses = []

var splits = lines[1].split(",")
// var splits = "7,13,x,x,59,x,31,19".split(",")
// var splits = "17,x,13,19".split(",")
// var splits = "67,7,59,61".split(",")
// var splits = "67,x,7,59,61".split(",")
// var splits = "67,7,x,59,61".split(",")
// var splits = "1789,37,47,1889".split(",")

for (var i = 0; i < splits.length; i++)
{
	if (splits[i] == "x")
	{
		// buses.push({"Id": "x"})
		continue;
	}
	
	var id = Number(splits[i])
	
	buses.push({"Id": id, "arrival": id, "offset": i})
}

// buses.sort((b1, b2) => b1.Id - b2.Id)

var time = 0;
var increment = buses[0].Id;

for (var i = 1; i < buses.length; i++)
{
	while (((time + buses[i].offset) % buses[i].Id) != 0)
	{
		time += increment;
	}
	
	increment *= buses[i].Id
}

console.log(time)

// var time = 0;
// var increment = buses[0].Id;

// for (var i = 1; i < buses.length; i++)
// {
	// if (buses[i].Id == "x")
		// continue;
	
	// console.log("x: " + x)
	// for (var j = 0; ; j++)
	// {
		// console.log((x + (n * j)))
		// if ((x + (n * j)) % buses[i].Id == buses[i].offset)
		// {
			// x = x + (n * j);
			// n = n * buses[i].Id
			// break;
		// }
	// }
// }

// console.log(x)

// for (var i = 1; i < buses.length; i++)
// {
	// if (buses[i].Id == "x")
		// continue;
	
	// while (buses[i].arrival < buses[0].arrival)
		// buses[i].arrival += buses[i].Id;
	
	// if (buses[i].arrival - buses[0].arrival != buses[i].offset)
	// {
		// buses[0].arrival += buses[0].Id;
		// i = 0;
	// }
// }

// console.log(buses[0].arrival)

// part 1

// for (var i = 0; i < buses.length; i++)
// {
	// var bus = buses[i]
	
	// while (bus.arrival < arrival)
	// {
		// bus.arrival += bus.Id;
	// }
// }

// buses.sort((b1, b2) => b1.arrival - b2.arrival)

// console.log(buses[0].arrival)