module leetcode.solutions.two_pointers

// 977. https://leetcode.com/problems/squares-of-a-sorted-array/
let sortedSquares (nums: int []) =
    let mutable left = 0
    let mutable right = nums.Length - 1
    let mutable index = right
    let result = Array.zeroCreate nums.Length

    while index >= 0 do
        if (abs (nums[left]) >= abs (nums[right])) then
            result[index] <- nums[left] * nums[left]
            left <- left + 1
        else
            result[index] <- nums[right] * nums[right]
            right <- right - 1

        index <- index - 1

    result


// 189. https://leetcode.com/problems/rotate-array/
let rotate (nums: int[], k: int) =
    let countRotates = k % nums.Length
    
    let reverse (nums: int[], firstPosition: int, lastPosition: int) =
        let mutable left = firstPosition
        let mutable right = lastPosition
        while left < right do
            let temp = nums[left]
            nums[left] <- nums[right]
            nums[right] <- temp
            left <- left + 1
            right <- right - 1
        
    reverse (nums, 0, nums.Length - 1)
    reverse (nums, 0, countRotates - 1)
    reverse (nums, countRotates, nums.Length - 1)
    
    
// 283. https://leetcode.com/problems/move-zeroes/
let moveZeroes (nums: int[]) =
    let mutable lastNonZeroFoundAt = 0
    for current in 0 .. nums.Length - 1 do
        if(nums[current] <> 0) then
            let temp = nums[lastNonZeroFoundAt]
            nums[lastNonZeroFoundAt] <- nums[current]
            nums[current] <- temp
            lastNonZeroFoundAt <- lastNonZeroFoundAt + 1


// 167. https://leetcode.com/problems/two-sum-ii-input-array-is-sorted/
let twoSum (nums: int[], target: int) =
    let mutable left = 0
    let mutable right = nums.Length - 1
    let mutable breakWhile = false
    let resultArray = [|0;0|]
    while left < right && not breakWhile do
        let currentSum = nums[left] + nums[right]
        
        if(currentSum = target) then
            resultArray[0] <- left + 1
            resultArray[1] <- right + 1
            breakWhile <- true
        else if(currentSum > target) then
            right <- right - 1
        else if(currentSum < target) then
            left <- left + 1
        
    resultArray    


// 344. https://leetcode.com/problems/reverse-string/
let reverseString (str: char[]) =
    let mutable left = 0
    let mutable right = str.Length - 1
    
    while left < right do
        let temp = str[left]
        str[left] <- str[right]
        str[right] <- temp
        left <- left + 1
        right <- right + 1