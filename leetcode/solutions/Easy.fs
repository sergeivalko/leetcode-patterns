namespace leetcode

open Microsoft.FSharp.Collections
open Microsoft.FSharp.Core

[<AutoOpen>]
module Easy =

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
      

    // 70. https://leetcode.com/problems/climbing-stairs/
    let climbStairs n =
        if n = 1 then
            1
        else
            let mutable n1 = 1
            let mutable n2 = 2
            
            for _ in [3.. n] do // fibonacci
                let temp = n1
                n1 <- n2
                n2 <- n1 + temp
            
            n2
    

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
        

    // 338. https://leetcode.com/problems/counting-bits/
    let countBits n =
        let result = Array.zeroCreate (n + 1)
        
        for i in 0 .. n do
            result[i] <- result[i >>> 1] + i % 2
        
        result
        

    // 141. https://leetcode.com/problems/linked-list-cycle/
    type ListNode = {
        mutable next: ListNode option
        value: int
    }
    let hasCycle (head:ListNode) =
        let mutable fast  = head
        let mutable slow = head
        let mutable hasCycle = false
        while (fast.next.IsSome && fast.next.Value.next.IsSome) && (not hasCycle) do
            fast <- fast.next.Value.next.Value
            slow <- slow.next.Value
            if fast = slow then
                hasCycle <- true
        hasCycle
    
    
    // 876. https://leetcode.com/problems/middle-of-the-linked-list/
    let middleNode (head: ListNode option) =
        let mutable fast  = head
        let mutable slow = head
        
        while (fast.IsSome && fast.Value.next.IsSome) do
            fast <- fast.Value.next.Value.next
            slow <- slow.Value.next
        
        slow
        
    
    // 206. https://leetcode.com/problems/reverse-linked-list/
    let reverseList (head: ListNode option) =
        let mutable prev = Option.None
        let mutable current = head
        
        while current.IsSome do
            let nxt = current.Value.next
            current.Value.next <- prev
            prev <- current
            current <- nxt
        
        prev
        
    // 234. https://leetcode.com/problems/palindrome-linked-list/
    let isPalindrome head =
        let mutable slow = head
        let mutable middle = middleNode head
        middle <- reverseList middle
        let mutable result = true
        
        while middle.IsSome do
            if slow.Value.value <> middle.Value.value then
                result <- false
            slow <- slow.Value.next
            middle <- middle.Value.next 
        
        result
   
    
    // 203. https://leetcode.com/problems/remove-linked-list-elements/
    let removeElements (node: ListNode option, target: int) =
        let mutable result = {value = 0; next = node}
        let mutable current = node
        while current.IsSome && current.Value.next.IsSome do
            if(current.Value.next.Value.value = target) then
                current.Value.next <- current.Value.next.Value.next
            else
                current <- current.Value.next
            
        result.next
        
        
    // 83. https://leetcode.com/problems/remove-duplicates-from-sorted-list/
    let deleteDuplicates (head: ListNode option) =
        let mutable current = head
        while current.IsSome && current.Value.next.IsSome do
            
            if current.Value.value = current.Value.next.Value.value then
                current.Value.next <- current.Value.next.Value.next
            else
                current <- current.Value.next
        head


    // 21. https://leetcode.com/problems/merge-two-sorted-lists/
    let mergeTwoLists (list1: ListNode option, list2: ListNode option) =
        let mutable merged = Option.Some {value = 0; next = Option.None}
        let result = merged
        let mutable head1 = list1
        let mutable head2 = list2
        
        while head1.IsSome && head2.IsSome do
            if head1.Value.value < head2.Value.value then
                merged.Value.next <- Option.Some {value = head1.Value.value; next = Option.None}
                head1 <- head1.Value.next
            else
                merged.Value.next <- Option.Some {value = head2.Value.value; next = Option.None}
                head2 <- head2.Value.next
            merged <- merged.Value.next
        
        while head1.IsSome do
            merged.Value.next <- Option.Some {value = head1.Value.value; next = Option.None}
            head1 <- head1.Value.next
            merged <- merged.Value.next
        
        while head2.IsSome do
            merged.Value.next <- Option.Some {value = head2.Value.value; next = Option.None}
            head2 <- head2.Value.next
            merged <- merged.Value.next
        
        result.Value.next
    
    
    // 704. https://leetcode.com/problems/binary-search/
    let binarySearch (nums: int[], target: int) =
        let mutable left = 0
        let mutable right  = nums.Length - 1
        let mutable index = -1
        let mutable needBreak = false
        while(left < right) && (not needBreak) do
            let mid  = (left + right) / 2
            
            if nums[mid] = target then
                index <- mid
                needBreak <- true
                
            else if(nums[mid] > target) then
                right <- mid - 1
            else
                left <- mid + 1
        index
    
    // 744. https://leetcode.com/problems/find-smallest-letter-greater-than-target/    
    let nextGreatestLetter (letters: char[], target: char) =
        let mutable left = 0
        let mutable right = letters.Length - 1
        
        if(letters[right] <= target || target < letters[0]) then
            letters[0]
        else
            while left < right do
                let mid = (left + right) / 2
                
                if letters[mid] > target then
                    right <- mid
                else left <- mid + 1
            letters[right]
            

    // 1. https://leetcode.com/problems/two-sum/
    let twoSum (nums: int[], target: int) =
        let mutable map = Map []
        let mutable result = []
        for i in 0 .. (nums.Length - 1) do
            let num = nums[i]
            
            if map.ContainsKey (target - num) then
                result <- (map[target - num]) :: i :: result
            else
                map <- map.Add (num, i)
        result
