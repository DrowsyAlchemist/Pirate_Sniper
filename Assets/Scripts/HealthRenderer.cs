using UnityEngine;
using UnityEngine.UI;

public class HealthRenderer : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    private IReadonlyHealth _health;

    public void Init(IReadonlyHealth health)
    {
        _health = health;
        Render(_health.CurrentHealth);
        _health.HealthChanged += Render;
    }

    private void OnDestroy()
    {
        _health.HealthChanged -= Render;
    }

    private void Render(int health)
    {
        _slider.value = (float)health / _health.MaxHealth;
    }
}
