namespace leetcode

[<AutoOpen>]
module Types =
    type ListNode =
        { mutable next: ListNode option
          value: int }

    type TreeNode =
        { mutable value: int
          mutable left: TreeNode option
          mutable right: TreeNode option }
