using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ReloadingRenderer : MonoBehaviour
{
    [SerializeField] private ShootingPoint _shootingPoint;
    [SerializeField] private Slider _slider;
    [SerializeField] private Animator _animator;

    private const string HideAnimation = "Hide";
    private const string ShowAnimation = "Show";

    private void Awake()
    {
        _shootingPoint.Shooted += OnShooted;
    }

    private void OnDestroy()
    {
        _shootingPoint.Shooted -= OnShooted;
    }

    private void OnShooted()
    {
        _animator.Play(ShowAnimation);
        Settings.CoroutineObject.StartCoroutine(Render(_shootingPoint.CurrentWeapon));
    }

    private IEnumerator Render(Weapon weapon)
    {
        while (weapon.IsReady == false)
        {
            _slider.value = 1 - weapon.SecondsBeforeReadyLeft / weapon.Info.SecondsBetweenShots;
            yield return null;
        }
        _slider.value = 1;
        _animator.Play(HideAnimation);
    }
}
