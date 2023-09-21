using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonClickSoundPlayer : MonoBehaviour
{
    private Button _button;
    private Sound _sound;

    private void Awake()
    {
        _sound = FindObjectOfType<Sound>() ?? throw new System.ApplicationException("Sound is not found");
        _button = GetComponent<Button>();
        _button.AddListener(OnClick);
    }

    private void OnDestroy()
    {
        _button.RemoveListener(OnClick);
    }

    private void OnClick()
    {
        _sound.PlayClick();
    }
}
