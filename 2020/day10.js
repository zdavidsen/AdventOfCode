var input = $("body > pre").innerText
var lines = input.split("\n")

var nums = [0]

for (var i = 0; i < lines.length; i++)
{
	if (lines[i].length == 0)
		continue;
	nums.push(Number(lines[i]))
}

nums.sort((n1, n2) => n1 - n2)

nums.push(nums[nums.length - 1] + 3)

var diffs = []
var splitDiffs = []

for (var i = 1; i < nums.length; i++)
{
	var diff = nums[i] - nums[i - 1]
	if (diff == 3)
	{
		splitDiffs.push(diffs)
		diffs = []
	}
	else
	{
		diffs.push(diff)
	}
}

var perms = 1;

for (var i = 0; i < splitDiffs.length; i++)
{
	var split = splitDiffs[i]
	switch (split.length)
	{
		case 0:
			break;
		case 1:
			break;
		case 2:
			perms *= 2;
			break;
		case 3:
			perms *= 4;
			break;
		case 4: 
			perms *= 7;
			break;
	}
}

console.log(perms)

// var ones = 0;
// var threes = 0;

// for (var i = 1; i < nums.length; i++)
// {
	// if (nums[i - 1] == nums[i] - 1)
		// ones++
	// if (nums[i - 1] == nums[i] - 3)
		// threes++
// }

// console.log(ones * threes)