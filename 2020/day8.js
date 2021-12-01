var input = $("body > pre").innerText
var lines = input.split("\n")

var replaced = {};
var prevState = null;
var state = {
	"pc": 0,
	"acc": 0,
	"visited": {}
}

while (true)
{
	if (state.visited[state.pc])
	{
		// console.log(state)
		// break;
		console.log("reverting")
		console.log(prevState)
		state = prevState;
		prevState = null;
		continue;
	}
	
	var line = lines[state.pc];
	
	if (line.length == 0)
		break;
	
	var match = /(\w{3}) ([+-]\d+)/.exec(line)
	
	var op = match[1]
	var arg = Number(match[2])
	
	if (prevState == null && (op == "jmp" || op == "nop") && !replaced[state.pc])
	{
		console.log("replacing")
		replaced[state.pc] = true;
		prevState = {
			"pc": state.pc,
			"acc": state.acc,
			"visited": {}
		};
		Object.assign(prevState.visited, state.visited);
		if (op == "jmp")
			op = "nop"
		else
			op = "jmp"
	}
	
	state.visited[state.pc] = true;
	
	switch (op)
	{
		case "acc":
			state.acc += arg;
			break;
		case "jmp":
			state.pc += arg - 1;
			break;
		case "nop":
			break;
	}
	
	state.pc++;
}

console.log(state.acc);

// var input = $("body > pre").innerText
// var lines = input.split("\n")

// var pc = 0;
// var acc = 0;
// var visited = {} ;

// while (true)
// {
	// if (visited[pc])
		// break;
	// visited[pc] = true;
	
	// var line = lines[pc++];
	// var match = /(\w{3}) ([+-]\d+)/.exec(line)
	
	// var arg = Number(match[2])
	
	// switch (match[1])
	// {
		// case "acc":
			// acc += arg;
			// break;
		// case "jmp":
			// pc += arg - 1;
			// break;
		// case "nop":
			// break;
	// }
// }

// console.log(acc)