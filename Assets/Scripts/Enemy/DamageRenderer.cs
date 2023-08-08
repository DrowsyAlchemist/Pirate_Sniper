using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class DamageRenderer : MonoBehaviour
{
    [SerializeField] private TMP_Text _damageText;
    [SerializeField] private Image _headshotImage;
    [SerializeField] private RectTransform _panel;

    private const string ShowAnimation = "Show";
    private const string IdleAnimation = "Idle";
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _panel.Deactivate();
    }

    public void Show(int damage, bool isHeadshot)
    {
        _damageText.text = "-" + damage.ToString();
        _headshotImage.gameObject.SetActive(isHeadshot);
        Settings.CoroutineObject.StartCoroutine(PlayShowAnimation());
    }

    private IEnumerator PlayShowAnimation()
    {
        _animator.Play(IdleAnimation);
        yield return new WaitForEndOfFrame();
        _animator.Play(ShowAnimation);
    }
}
