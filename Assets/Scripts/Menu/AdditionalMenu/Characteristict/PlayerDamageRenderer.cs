using TMPro;
using UnityEngine;

public class PlayerDamageRenderer : MonoBehaviour
{
    [SerializeField] private TMP_Text _damageText;

    private Player _player;

    public void Init(Player player)
    {
        _player = player;
    }

    public void Render()
    {
        _damageText.text = _player.Damage.ToString();
    }
}
