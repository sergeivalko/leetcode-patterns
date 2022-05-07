# Two pointers

These kind of problems usually involve two pointers:
* One slow-runner and the other fast-runner.
* One pointer starts from the beginning while the other pointer starts from the end.

> The idea here is to iterate two different parts of the array simultaneously to get the answer faster.

## Implementation

There are primarily two ways of implementing the two-pointer technique:

1. One pointer at each end

One pointer starts from beginning and other from the end and they proceed towards each other

![image](https://s3.ap-south-1.amazonaws.com/afteracademy-server-uploads/what-is-the-two-pointer-technique-type1-0f96379aee2ce0dc.png)

> Example : In a sorted array, find if a pair exists with a given sum S

* **Brute Force Approach**: We could implement a nested loop finding all possible pairs of elements and adding them.

```c#
bool pairExists(int arr[], int n, int S)
{
    for(i = 0 to n-2)
        for(j = i+1 to n-1)
            if(arr[i] + arr[j] == S)
                return true
    return false
}
```

**Time complexity: O(n²)**

* **Efficient Approach**

```c#
bool pairExists(int arr[], int n, int S)
{
    i = 0
    j = n-1
    while(i < j)
    {
        curr_sum = arr[i] + arr[j]
        if (curr_sum == S)
            return true
        else if (curr_sum < X)
            i = i + 1
        else if (curr_sum > X)
            j = j - 1
    }
    return false
}
```

**Time Complexity: O(n)**

2. Different Paces

Both pointers start from the beginning but one pointer moves at a faster pace than the other one.

![image](https://s3.ap-south-1.amazonaws.com/afteracademy-server-uploads/what-is-the-two-pointer-technique-type2-0ff52ece0ef1829c.png)

> Example: Find the middle of a linked list

* **Brute Force Approach**: We can find the length of the entire linked list in one complete iteration and then iterate
  till half-length again.

```c#
ListNode getMiddle(ListNode head)
{
    len = 0
    ListNode curr = head
    while (curr != NULL)
    {
        curr = curr.next
        len = len + 1
    }
    
    curr = head
    i = 0
    while(i != len / 2)
    {
        curr = curr.next
        i = i + 1
    }
    return curr
}
```

* **Efficient Approach:** Using a two-pointer technique allows us to get the result in one complete iteration

```c#
ListNode getMiddle(ListNode head)
{
    ListNode slow = head
    ListNode fast = head
    while(fast && fast.next)
    {
        slow = slow.next
        fast = fast.next.next
    }
   return slow
}
```

## How does this technique save space?

There are several situations when a naive implementation of a problem requires additional space thereby increasing the
space complexity of the solution. Two-pointer technique often helps to decrease the required space or remove the need
for it altogether

> Example: Reverse an array

* **Naive Solution**: Using a temporary array and fillings elements in it from the end

```c#
int[] reverseArray(int arr[], int n)
{
    int reverse[n]
    for (i = 0 to n-1 )
        reverse[n-i-1] = arr[i]
    
    return reverse
}
```

**Space Complexity: O(n)**

* **Efficient Solution:** Moving pointers towards each other from both ends and swapping elements at their positions

```c#
int[] reverseArray(int arr[], int n)
{
    i = 0
    j = n-1
    while ( i < j )
    {
        swap(arr[i], arr[j])
        i = i + 1
        j = j - 1
    }
   return arr
}
```

**Space Complexity: O(1)**


### Classic problems:

1. [Remove Duplicates from Sorted Array](https://leetcode.com/problems/remove-duplicates-from-sorted-array/)
2. [Two Sum II - Input array is sorted](https://leetcode.com/problems/two-sum-ii-input-array-is-sorted/)
3. [Reverse Words in a String II](https://leetcode.com/problems/reverse-words-in-a-string-ii/)
4. [Rotate Array](https://leetcode.com/problems/rotate-array/)
5. [Valid Palindrome](https://leetcode.com/problems/valid-palindrome/)
6. [Container With Most Water](https://leetcode.com/problems/container-with-most-water/)
7. [Product of Array Except Self](https://leetcode.com/problems/product-of-array-except-self/)