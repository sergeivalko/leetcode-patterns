namespace leetcode

[<AutoOpen>]
module sliding_window =
    // 3. https://leetcode.com/problems/longest-substring-without-repeating-characters/submissions/
    let lengthOfLongestSubstring (str: string) =
        if System.String.IsNullOrWhiteSpace str then
            0
        else
            let window = Array.zeroCreate 128
            let mutable start = 0
            let mutable right = 0
            let mutable low = 0
            let mutable high = 0
            
            while high < str.Length do
                if window[int str[high]] then
                    while str[low] <> str[high] do
                        window[int str[low]] <- false
                        low <- low + 1
                    
                    low <- low + 1
                else
                    window[int str[high]] <- true
                    if right - start < high - low then
                        start <- low
                        right <- high
                
                high <- high + 1
            
            right - start + 1
