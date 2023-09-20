using TMPro;
using UnityEngine;

public class PlayerMaxHealthRenderer : MonoBehaviour
{
    [SerializeField] private TMP_Text _healthText;

    private Saver _saver;

    public void Init(Saver saver)
    {
        _saver = saver;
    }

    public void Render()
    {
        _healthText.text = _saver.PlayerHealth.ToString();
    }
}
