var input = $("body > pre").innerText
var lines = input.split("\n")

var nums = [];

// for (var i = 0; i < 25; i++)
// {
	// nums.push(Number(lines[i]));
// }

// for (var i = 25; i < lines.length; i++)
// {
	// if (lines[i].length == 0)
		// break;
	
	// var valid = false;
	
	// for (var j = 0; j < 24; j++)
	// {
		// for (var k = 1; k < 25; k++)
		// {
			// if (nums[j] + nums[k] == lines[i] && nums[j] != nums[k])
				// valid = true;
		// }
	// }
	
	// nums.shift();
	// nums.push(Number(lines[i]))
	
	// if (!valid)
	// {
		// console.log(lines[i])
		// break;
	// }
// }

var invalid = 375054920;
var sum = 0;

for (var i = 0; i < lines.length; i++)
{
	if (lines[i].length == 0)
		break;
	
	var num = Number(lines[i])
	
	sum += num;
	nums.push(num);
	
	while (sum > invalid)
	{
		var kick = nums.shift();
		sum -= kick;
	}
	
	if (sum == invalid)
	{
		nums.sort((n1, n2) => n1 - n2)
		console.log(nums[0] + nums[nums.length - 1])
		break;
	}
}