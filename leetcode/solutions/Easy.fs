namespace leetcode

open System.Collections.Generic
open Microsoft.FSharp.Collections
open Microsoft.FSharp.Core

[<AutoOpen>]
module Easy =

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
        while(left <= right) && (not needBreak) do
            let mid  = left +  (right - left) / 2
            
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
            while left <= right do
                let mid = left +  (right - left) / 2
                
                if letters[mid] > target then
                    right <- mid
                else left <- mid + 1
            letters[right]
     
    
    // 852. https://leetcode.com/problems/peak-index-in-a-mountain-array/  
    let peakIndexInMountainArray (arr: int[]) =
        let mutable left = 0
        let mutable right = arr.Length - 1
        while left <= right do
            let mid = left +  (right - left) / 2
            if arr[mid] < arr[mid + 1] then
                left <- mid + 1
            else
                right <- mid - 1
        left

    
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

        
    // 637. https://leetcode.com/problems/average-of-levels-in-binary-tree/
    let averageOfLevels (root: TreeNode option) =
        if root.IsNone then
            []
        else 
            let mutable result = []
            let mutable queue = Queue<TreeNode>()
            queue.Enqueue(root.Value)
            let empty () = queue.Count = 0

            while not (empty()) do
                let n = queue.Count
                let mutable levelSum = 0                
                
                for i in 1 .. n do
                    let node = queue.Dequeue()
                    levelSum <- levelSum + node.value
                    
                    if node.left.IsSome then
                        queue.Enqueue(node.left.Value)
                
                    if node.right.IsSome then
                        queue.Enqueue(node.right.Value)
                
                let avg = levelSum / n
                result <- result @ [avg]
                
            result
            
    
    // 111. https://leetcode.com/problems/minimum-depth-of-binary-tree/
    let minDepth (root: TreeNode option) =
        if root.IsNone then
            0
        else 
            let mutable queue = Queue<TreeNode>()
            queue.Enqueue(root.Value)
            let empty () = queue.Count = 0
            let mutable depth = 1
            let mutable needClear = false
            while not (empty()) && not needClear do
                let n = queue.Count
                for i in 1 .. n do
                    let node = queue.Dequeue()
                    
                    if node.left.IsNone && node.right.IsNone then
                        needClear <- true
                    
                    if node.left.IsSome then
                        queue.Enqueue(node.left.Value)
                
                    if node.right.IsSome then
                        queue.Enqueue(node.right.Value)
                        
                if needClear then
                    queue.Clear()
                    
                if not needClear then
                    depth <- depth + 1
                
            depth
            
        
    // 104. https://leetcode.com/problems/maximum-depth-of-binary-tree/    
    let rec maxDepth (root: TreeNode option) =
        if root.IsNone then
            0
        else
            let leftDepth = maxDepth root.Value.left
            let rightDepth = maxDepth root.Value.right
            (max leftDepth rightDepth) + 1


    // 112. https://leetcode.com/problems/path-sum/
    let rec hasPathSum (root: TreeNode option, targetSum: int) =
        if root.IsNone then
            false
        else 
            let newTarget = targetSum - root.Value.value    
            
            if root.Value.left.IsNone && root.Value.right.IsNone then
                newTarget = 0
            else
                hasPathSum (root.Value.left, newTarget) || hasPathSum(root.Value.right, newTarget)
                
    
    // 100. https://leetcode.com/problems/same-tree/
    let rec isSameTree (leftTree: TreeNode option, rightTree: TreeNode option) =
        if leftTree.IsNone && rightTree.IsNone then
            true
        
        else if leftTree.IsNone || rightTree.IsNone then
            false
        
        else if (leftTree.Value.value <> rightTree.Value.value) then
            false
        else 
            let left = isSameTree(leftTree.Value.left, rightTree.Value.left)
            let right = isSameTree(leftTree.Value.right, rightTree.Value.right)
            left && right
            
            
    // 543. Diameter of Binary Tree
    let rec diameterOfBinaryTree (root: TreeNode option) =
        if root.IsNone then
            0
        else
            let leftDepth = maxDepth root.Value.left
            let rightDepth = maxDepth root.Value.right
            leftDepth + rightDepth
            
            
    // 617. Merge Two Binary Trees
    let rec mergeTrees (leftTree: TreeNode option, rightTree: TreeNode option) =
        if leftTree.IsNone then
            rightTree
        else if rightTree.IsNone then
            leftTree
        else
            leftTree.Value.value <- leftTree.Value.value + rightTree.Value.value
            leftTree.Value.left <- mergeTrees(leftTree.Value.left, rightTree.Value.left)
            leftTree.Value.right <- mergeTrees(leftTree.Value.right, rightTree.Value.right)
            leftTree


    // 235. Lowest Common Ancestor of a Binary Search Tree
    let lowestCommonAncestor (root: TreeNode option, p: TreeNode, q: TreeNode) =
        let mutable node = root
        let mutable isNeedBreak = false
        let mutable resultNode = Option.None
        while node.IsSome && not isNeedBreak do
            
            if p.value > node.Value.value && q.value > node.Value.value then
                node <- node.Value.right
            else if p.value < node.Value.value && q.value < node.Value.value then
                node <- node.Value.left
            else
                isNeedBreak <- true
                resultNode <- node
        resultNode