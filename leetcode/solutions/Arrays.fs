namespace leetcode

open Microsoft.FSharp.Collections

[<AutoOpen>]
module Arrays =

    open System.Collections.Generic

    // 217. https://leetcode.com/problems/contains-duplicate/
    let containsDuplicate (nums: int []) =
        let map = Dictionary<int, int>()
        let mutable result = false

        for num in nums do
            if (map.ContainsKey num) then
                result <- true
            else
                map.Add(num, num)

        result
        
        
    // 268. https://leetcode.com/problems/missing-number/    
    let missingNumber nums =
        let length = nums |> Array.length
        length * (length + 1) / 2 - (nums |> Array.sum)
        
    
    // 468. https://leetcode.com/problems/find-all-numbers-disappeared-in-an-array/    
    let findDisappearedNumbers (nums : int []) =
        let useAdditionalMemory =
            let array = [|1..(nums |> Array.length)|]
            for num in nums do
                if (array[num - 1] = num) then
                    array[num - 1] <- 0;
            array |> Array.filter (fun x-> x <> 0)
            
        let cyclicSort =
             let mutable i = 0
             let length = nums.Length
             while (i < length) do
                 let pos = nums[i] - 1
                 
                 if(nums[i] <> nums[pos]) then
                    let temp  = nums[i]
                    nums[i] <- nums[pos]
                    nums[pos] <- temp
                 else
                    i <- i + 1
             let mutable miss = []
             
             nums |>
             Array.iteri (fun i _ -> if(nums[i] <> i + 1) then miss <- List.append miss [i + 1]) 
             
             miss
        cyclicSort
        
        
    // 136. https://leetcode.com/problems/single-number/
    let singleNumber nums =
        let mutable mask = 0
        
        for num in nums do
            mask <- mask ^^^ num // xor
                
        mask
          
    
    // 121. https://leetcode.com/problems/best-time-to-buy-and-sell-stock/
    let maxProfit (prices: int []) =
        let mutable maxProfit = 0
        let mutable currentMin = prices[0]
        
        for price in prices do
            maxProfit <- if maxProfit > (price - currentMin) then maxProfit else price - currentMin
            currentMin <- if currentMin < price then currentMin else price
        
        maxProfit
        
        
    // 53. https://leetcode.com/problems/maximum-subarray/
    let maxSubArray (nums: int[]) =
        let mutable maxSum = nums[0]
        let mutable currentSum = nums[0]
        
        for i in 1 .. nums.Length - 1 do
            let num = nums[i]
            currentSum <- (if (currentSum + num) > num then currentSum + num else num)
            maxSum <- if maxSum > currentSum then maxSum else currentSum
            
        maxSum
        
        
    // 303. https://leetcode.com/problems/range-sum-query-immutable/
    // [|-2;0;3;-5;2;-1|]
    // -2 -2 1 -4 -2 -3    
    let subArrayRange (nums: int[]) =
        let mutable sum = 0
        let mutable currentSum = 0
        let mutable sums = []
        
        for num in nums do
            currentSum <- currentSum + num
            sums <-  sums @ [currentSum]
        
        let sumRange left right =
            if left = 0 then
                sums[right]
            else
                sums[right] - sums[left - 1]
                
        sum <- sum + sumRange 0 2
        sum <- sum + sumRange 2 5
        sum <- sum + sumRange 0 5
        
        sum
        
        
    // 198. https://leetcode.com/problems/house-robber/
    let rob (nums: int[]) =
        let mutable maxValueForRob = 0
        let maxAvailableHouseForRob = if nums.Length % 2 = 0 then nums.Length / 2 else nums.Length / 2 + 1
        let lenght = nums.Length - 1
        let mutable attempts = Array.zeroCreate nums.Length |> Array.map (fun _ -> Array.zeroCreate maxAvailableHouseForRob)
        
        for house in 0 .. lenght do
            let mutable currentSum = 0
            let maxAttemptForCurrentHome = house / 2;
            for robAttempt in 1 .. maxAvailableHouseForRob do
                if robAttempt = 1 then
                    currentSum <- nums[house]
                    attempts[house][robAttempt - 1] <- nums[house]
                
                else if robAttempt - 2 >= 0 then
                    currentSum <- attempts[house][robAttempt - 2] + attempts[house - 2][robAttempt - 2]
                    attempts[house][robAttempt - 1] <- currentSum

            maxValueForRob <- if currentSum > maxValueForRob then currentSum else maxValueForRob
        maxValueForRob