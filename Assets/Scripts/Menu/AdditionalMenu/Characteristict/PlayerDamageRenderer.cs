using TMPro;
using UnityEngine;

public class PlayerDamageRenderer : MonoBehaviour
{
    [SerializeField] private TMP_Text _damageText;

    private Saver _saver;

    public void Init(Saver saver)
    {
        _saver = saver;
    }

    public void Render()
    {
        _damageText.text = _saver.PlayerDamage.ToString();
    }
}
