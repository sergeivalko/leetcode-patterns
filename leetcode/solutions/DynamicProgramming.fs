namespace leetcode

[<AutoOpen>]
module DynamicProgramming =        
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