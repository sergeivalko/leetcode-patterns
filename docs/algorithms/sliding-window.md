# Sliding window

As its name suggests, this technique involves taking a subset of data from a given array or string, expanding or
shrinking that subset to satisfy certain conditions, hence the sliding effect.

![image](https://miro.medium.com/max/1400/0*80T-H4ETYfHvRH6o.gif)

## When can we use it?

Generally speaking, the sliding window technique is useful when you need to keep track of a **contiguous** sequence of
elements, such as summing up the values in a subarray.

> Given an array of positive integers and a positive integer,
> write a function that returns the minimal length of a contiguous subarray,
> where the sum is greater than or equal to the integer being passed in.
> If there isn’t one, return 0.

And here are some test cases:

```js
minSubArrayLen([2, 3, 1, 2, 4, 3], 7) // 2 -> [4, 3] is the smallest subarray
minSubArrayLen([3, 1, 7, 8, 62, 18, 9], 52) // 1 -> [62] is the smallest subarray
minSubArrayLen([1, 4, 16, 22, 5], 95) // 0
```

To implement the sliding window technique for this challenge, we need to first figure out the range of the window. In
this case, we “open up” the window from the left.

Then, we need to store the sum of the values in the enclosed subarray/window, and compare it against the target integer.

If the sum meets the condition (greater than or equal to the integer), we record the length of the current window range
and keep shrinking the window, as we need to find the minimal length.

If the sum does not meet the condition, then we keep expanding the right panel of the window (because we are iterating
from the left).

If the sum never meets the target, we break out of the loop and return 0 instead.

Putting it together:

```js
function minSubArrayLen(arr, target) {
    let minLength = Infinity
    let sum = 0
    let left = 0
    let right = 0
    while (left < arr.length) {
        if (sum >= target) {
            // store the current minimal length
            minLength = Math.min(minLength, (right - left))
            // shrink the window: 
            // (1) subtract the value at left idx
            // (2) move the left panel one index further to the right
            sum -= arr[left]
            left++
        } else if (sum < target && right < arr.length) {
            // expand the window:
            // (1) sum up the current value
            // (2) move the right panel one index further to the right
            sum += arr[right]
            right++
        } else {
            break
        }
    }
    return minLength === Infinity ? 0 : minLength
}
```

By using the sliding window technique, we are able to solve the problem above with O(n) time complexity, eliminating the
need for duplicate iterations. Hats off to the person/team who came up with this powerful tool!

### Classic problems: [List of problems](https://leetcode.com/tag/sliding-window/)