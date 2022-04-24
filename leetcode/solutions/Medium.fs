namespace leetcode

open Microsoft.FSharp.Collections

[<AutoOpen>]
module Medium =

    // 198. https://leetcode.com/problems/house-robber/
    let rob (nums: int[]) =
        let n = nums.Length
        let dp = Array.zeroCreate (nums.Length + 2)
        
        for i in 2 .. (n + 1) do
            dp[i] <- max dp[i - 1] (dp[i - 2] + nums[i - 2])
        
        dp[dp.Length - 1]


    // 162. https://leetcode.com/problems/find-peak-element/
    let findPeakElement (nums: int[]) =
        let mutable left = 0
        let mutable right  = nums.Length - 1
        
        while(left <= right) do
            let mid  = left +  (right - left) / 2
            
            if nums[mid] < nums[mid + 1] then
                left <- mid + 1
            else right <- mid
        left