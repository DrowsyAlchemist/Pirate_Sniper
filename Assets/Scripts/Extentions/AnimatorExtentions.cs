using UnityEngine;

public static class AnimatorExtentions
{
    public static float GetLength(this Animator animator)
    {
        return animator.GetCurrentAnimatorStateInfo(0).length;
    }
}
