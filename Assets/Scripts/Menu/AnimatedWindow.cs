using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatedWindow : MonoBehaviour
{
    private const string AppearAnimation = "Appear";
    private const string DisappearAnimation = "Disappear";

    private Animator _animator;

    public bool IsPlaying { get; private set; }

    public virtual void Appear()
    {
        if (_animator == null)
            Init();

        IsPlaying = true;
        _animator.Play(AppearAnimation);
    }

    public virtual void Disappear()
    {
        if (_animator == null)
            Init();

        IsPlaying = true;
        _animator.Play(DisappearAnimation);
    }

    private void OnAnimatorStopPlaying()
    {
        IsPlaying = false;
    }

    private void Init()
    {
        _animator = GetComponent<Animator>();
    }
}
