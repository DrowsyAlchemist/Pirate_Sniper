using TMPro;
using UnityEngine;

public class PlayerMaxHealthRenderer : MonoBehaviour
{
    [SerializeField] private TMP_Text _healthText;

    private Player _player;

    public void Init(Player player)
    {
        _player = player;
    }

    public void Render()
    {
        _healthText.text = _player.MaxHealth.ToString();
    }
}
