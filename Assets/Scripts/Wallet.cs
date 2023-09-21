using System;
using UnityEngine;

public class Wallet
{
    private readonly Saver _saver;
    private readonly AudioSource _buySound;

    public int Money { get; private set; }

    public event Action<int> MoneyValueChanged;

    public Wallet(Saver saver, AudioSource buySound)
    {
        _saver = saver;
        _buySound = buySound;
        Money = _saver.PlayerMoney;
        MoneyValueChanged?.Invoke(Money);
    }

    public bool TryGiveMoney(int money)
    {
        if (CanGive(money))
        {
            Give(money);
            return true;
        }
        return false;
    }

    public bool CanGive(int money)
    {
        if (money < 0)
            throw new ArgumentOutOfRangeException();

        return Money >= money;
    }

    public void Give(int money)
    {
        if (CanGive(money) == false)
            throw new InvalidOperationException();

        SetMoney(Money - money);
        _buySound.Play();
    }

    public void Add(int money)
    {
        if (money < 0)
            throw new ArgumentOutOfRangeException();

        SetMoney(Money + money);
    }

    private void SetMoney(int value)
    {
        Money = value;
        _saver.SetPlayerMoney(Money);
        MoneyValueChanged?.Invoke(Money);
    }
}
