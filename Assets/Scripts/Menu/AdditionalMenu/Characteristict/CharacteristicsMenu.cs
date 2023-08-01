using Agava.YandexGames;
using System;
using UnityEngine;

public class CharacteristicsMenu : MoneyRenderer
{
    [SerializeField] private PlayerMaxHealthRenderer _healthRenderer;
    [SerializeField] private PlayerDamageRenderer _damageRenderer;

    [SerializeField] private IncreaseCharacteristicPanel _healthPanel;
    [SerializeField] private IncreaseCharacteristicPanel _damagePanel;

    private Player _player;

    public bool HasHealthNextLevel => Settings.Characteristics.Health.HasNextLevel(_player.MaxHealth);
    public bool HasDamageNextLevel => Settings.Characteristics.Damage.HasNextLevel(_player.Damage);
    public Characteristic.Level NextHealthLevel => Settings.Characteristics.Health.GetNextLevel(_player.MaxHealth);
    public Characteristic.Level NextDamageLevel => Settings.Characteristics.Damage.GetNextLevel(_player.Damage);

    public void Init(Player player)
    {
        _player = player;
        base.Init(player.Wallet);
        _healthRenderer.Init(player);
        _damageRenderer.Init(player);
        _healthPanel.BuyButtonClicked += OnHealthButtonClick;
        _damagePanel.BuyButtonClicked += OnDamageButtonClick;
    }

    public override void Open()
    {
        base.Open();
        _healthRenderer.Render();
        _damageRenderer.Render();
        RenderNextHealthLevel();
        RenderNextDamageLevel();
    }

    private void OnHealthButtonClick()
    {
        if (NextHealthLevel.Cost <= 0)
            RewardForAd(IncreaseHealth);
        else if (TryGetMoney(NextHealthLevel.Cost))
            IncreaseHealth();
    }

    private void OnDamageButtonClick()
    {
        if (NextDamageLevel.Cost <= 0)
            RewardForAd(IncreaseDamage);
        else if (TryGetMoney(NextDamageLevel.Cost))
            IncreaseDamage();
    }

    private void IncreaseHealth()
    {
        _player.SetMaxHealth(NextHealthLevel.Value);
        _healthRenderer.Render();
        RenderNextHealthLevel();
    }

    private void IncreaseDamage()
    {
        _player.SetDamage(NextDamageLevel.Value);
        _damageRenderer.Render();
        RenderNextDamageLevel();
    }

    private void RewardForAd(Action reward)
    {
#if UNITY_EDITOR
        reward();
        return;
#endif
        VideoAd.Show(onRewardedCallback: reward);
    }

    private bool TryGetMoney(int money)
    {
        if (_player.Wallet.CanGive(money))
        {
            _player.Wallet.Give(money);
            return true;
        }
        return false;
    }

    private void RenderNextHealthLevel()
    {
        if (HasHealthNextLevel)
            _healthPanel.Render(NextHealthLevel.Cost);
        else
            _healthPanel.Deactivate();
    }

    private void RenderNextDamageLevel()
    {
        if (HasDamageNextLevel)
            _damagePanel.Render(NextDamageLevel.Cost);
        else
            _damagePanel.Deactivate();
    }
}
