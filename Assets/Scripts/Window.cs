using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Window : MonoBehaviour
{
    private const string AppearAnimation = "Appear";
    private const string DisappearAnimation = "Disappear";
    private Animator _animator;

    public void Appear()
    {
        if (_animator == null)
            Init();

        _animator.Play(AppearAnimation);
    }

    public void Disappear()
    {
        if (_animator == null)
            Init();

        _animator.Play(DisappearAnimation);
    }

    private void Init()
    {
        _animator = GetComponent<Animator>();
    }
}
