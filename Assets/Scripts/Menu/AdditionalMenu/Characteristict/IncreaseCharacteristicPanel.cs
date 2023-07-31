using System;
using TMPro;
using UnityEngine;

public class IncreaseCharacteristicPanel : MonoBehaviour
{
    [SerializeField] private UIButton _buyButton;
    [SerializeField] private RectTransform _adPanel;
    [SerializeField] private RectTransform _moneyPanel;
    [SerializeField] private TMP_Text _costText;

    public event Action BuyButtonClicked;

    private void Awake()
    {
        _buyButton.SetOnClickAction(OnBuyButtonClick);
    }

    public void Render(int cost)
    {
        _adPanel.gameObject.SetActive(cost <= 0);
        _moneyPanel.gameObject.SetActive(cost > 0);
        _costText.text = cost.ToString();
    }

    public void Deactivate()
    {
        _buyButton.gameObject.SetActive(false);
    }

    private void OnBuyButtonClick()
    {
        BuyButtonClicked?.Invoke();
    }
}
