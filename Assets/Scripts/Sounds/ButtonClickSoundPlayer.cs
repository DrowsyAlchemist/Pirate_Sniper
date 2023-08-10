using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonClickSoundPlayer : MonoBehaviour
{
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.AddListener(OnClick);
    }

    private void OnDestroy()
    {
        _button.RemoveListener(OnClick);
    }

    private void OnClick()
    {
        Sound.PlayClick();
    }
}
