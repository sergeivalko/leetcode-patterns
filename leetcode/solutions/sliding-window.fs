namespace leetcode

[<AutoOpen>]
module sliding_window =
    // 3. https://leetcode.com/problems/longest-substring-without-repeating-characters/submissions/
    let lengthOfLongestSubstring str =
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
            

    // 567. https://leetcode.com/problems/permutation-in-string/        
    let checkInclusion (str1:string,  str2: string) =
        if str1.Length > str2.Length then
            false
        else
            let map1 = Array.zeroCreate 26
            let map2 = Array.zeroCreate 26
            let aValue = int 'a'
            let matches (map1: int[], map2: int[]) =
                let mutable needBreak = false
                let mutable index = 0
                
                while index < 26 && not needBreak do
                    if(map1[index] <> map2[index]) then
                        needBreak <- true
                    index <- index + 1
                not needBreak && true
                
            for i in 0 .. str1.Length - 1 do
                let index1 = int str1[i] - aValue
                let index2 = int str2[i] - aValue
                map1[index1]<- map1[index1] + 1
                map2[index2]<- map2[index2] + 1
            
            let mutable index = 0
            let mutable needBreak = false
            
            while index < (str2.Length - str1.Length) && not needBreak do
                    if matches (map1, map2) then
                        needBreak <- true
                    else
                        let index1 = int str2[index + str1.Length] - aValue
                        let index2 = int str2[index] - aValue
                        map2[index1] <- map2[index1] + 1
                        map2[index2] <- map2[index2] - 1
                    index <- index + 1
            
            matches (map1, map2)
