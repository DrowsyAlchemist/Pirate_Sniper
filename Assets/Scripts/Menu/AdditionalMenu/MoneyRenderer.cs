using TMPro;
using UnityEngine;

public class MoneyRenderer : Window
{
    [SerializeField] private TMP_Text _moneyText;

    protected Wallet Wallet { get; private set; }

    protected void Init(Wallet wallet)
    {
        Wallet = wallet;
        Wallet.MoneyValueChanged += OnMoneyChanged;
        OnMoneyChanged(Wallet.Money);
    }

    private void OnDestroy()
    {
        Wallet.MoneyValueChanged -= OnMoneyChanged;
    }

    private void OnMoneyChanged(int money)
    {
        _moneyText.text = money.ToString();
    }
}
