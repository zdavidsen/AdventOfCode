// var input = "12,20,0,6,1,17,7".split(",")
// var input = "0,3,6".split(",")

// var nums = []

// for (var i = 0; i < input.length; i++)
// {
	// nums.push(Number(input[i]))
// }

// var lastNum = nums.pop();

// for (var i = nums.length; i < 30000000; i++)
// {
	// var index = nums.lastIndexOf(lastNum)
	// if (index == -1)
		// index = nums.length;
	
	// nums.push(lastNum)
	
	// lastNum = nums.length - 1 - index;
// }

// console.log(nums[30000000 - 1])

var input = "12,20,0,6,1,17,7".split(",")
// var input = "0,3,6".split(",")

var nums = []

for (var i = 0; i < input.length - 1; i++)
{
	nums[Number(input[i])] = i
}

var lastNum = Number(input[input.length - 1])

for (var i = input.length - 1; i < 30000000 - 1; i++)
{
	var newNum = i - nums[lastNum];
	
	nums[lastNum] = i;
	
	if (isNaN(newNum))
		newNum = 0;
	
	lastNum = newNum;
}

console.log(lastNum)