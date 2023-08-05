using UnityEngine;

[RequireComponent(typeof(Animator))]
public class WeaponAnimator : MonoBehaviour
{
    private const string ShotAnimation = "Shot";

    public bool IsPlaying { get; private set; }

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayShot()
    {
        IsPlaying = true;
        _animator.Play(ShotAnimation);
    }

    private void OnAnimatorStopPlaying()
    {
        IsPlaying = false;
    }
}
