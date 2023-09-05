using System;
using TMPro;
using UnityEngine;

public class WareRenderer : MonoBehaviour
{
    [SerializeField] private UIButton _buyButton;
    [SerializeField] private RectTransform _adPanel;
    [SerializeField] private RectTransform _moneyPanel;
    [SerializeField] private TMP_Text _costText;

    public event Action<WareRenderer> BuyButtonClicked;

    private void Awake()
    {
        _buyButton.AddOnClickAction(OnBuyButtonClick);
    }

    public void Render(int cost)
    {
        _adPanel.gameObject.SetActive(cost <= 0);
        _moneyPanel.gameObject.SetActive(cost > 0);
        _costText.text = cost.ToString();
    }

    public void DeactivateBuyButton()
    {
        _buyButton.gameObject.SetActive(false);
    }

    private void OnBuyButtonClick()
    {
        BuyButtonClicked?.Invoke(this);
    }
}
