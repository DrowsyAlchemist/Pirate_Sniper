using UnityEngine;

public class HitEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem _hitEffect;
    [SerializeField] private AudioSource _hitSound;

    public void Init()
    {
        _hitEffect.Play();
    }

    public void Play()
    {
        _hitEffect.Play();
        _hitSound.Play();
    }
}
