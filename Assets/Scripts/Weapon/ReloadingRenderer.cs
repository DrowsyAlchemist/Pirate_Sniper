using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ReloadingRenderer : MonoBehaviour
{
    [SerializeField] private ShootingPoint _shootingPoint;
    [SerializeField] private Image _filledImage;
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
            _filledImage.fillAmount = weapon.SecondsBeforeReadyLeft / weapon.SecondsBetweenShots;
            yield return null;
        }
        _filledImage.fillAmount = 0;
        _animator.Play(HideAnimation);
    }
}
