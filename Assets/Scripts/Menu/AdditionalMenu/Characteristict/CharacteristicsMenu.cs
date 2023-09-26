using UnityEngine;

public class CharacteristicsMenu : MoneyRenderer
{
    [SerializeField] private PlayerMaxHealthRenderer _maxHealthRenderer;
    [SerializeField] private PlayerDamageRenderer _damageRenderer;

    [SerializeField] private WareRenderer _healthPanel;
    [SerializeField] private WareRenderer _damagePanel;

    private Saver _saver;
    private Wallet _wallet;
    private Sound _sound;

    public bool HasHealthNextLevel => Settings.Characteristics.Health.HasNextLevel(_saver.PlayerHealth);
    public bool HasDamageNextLevel => Settings.Characteristics.Damage.HasNextLevel(_saver.PlayerDamage);
    public CharacteristicLevel NextHealthLevel => Settings.Characteristics.Health.GetNextLevel(_saver.PlayerHealth);
    public CharacteristicLevel NextDamageLevel => Settings.Characteristics.Damage.GetNextLevel(_saver.PlayerDamage);

    public void Init(Saver saver, Wallet wallet, Sound sound)
    {
        _saver = saver;
        _wallet = wallet;
        _sound = sound;
        base.Init(wallet);
        _maxHealthRenderer.Init(saver);
        _damageRenderer.Init(saver);
        _healthPanel.BuyButtonClicked += OnHealthButtonClick;
        _damagePanel.BuyButtonClicked += OnDamageButtonClick;
    }

    public override void Open()
    {
        base.Open();
        _maxHealthRenderer.Render();
        _damageRenderer.Render();
        RenderNextHealthLevel();
        RenderNextDamageLevel();
    }

    private void OnHealthButtonClick(WareRenderer _)
    {
        if (NextHealthLevel.Cost <= 0)
            Advertising.RewardForVideo(IncreaseHealth, _sound);
        else if (_wallet.TryGiveMoney(NextHealthLevel.Cost))
            IncreaseHealth();
    }

    private void OnDamageButtonClick(WareRenderer _)
    {
        if (NextDamageLevel.Cost <= 0)
            Advertising.RewardForVideo(IncreaseDamage, _sound);
        else if (_wallet.TryGiveMoney(NextDamageLevel.Cost))
            IncreaseDamage();
    }

    private void IncreaseHealth()
    {
        _saver.SetPlayerHealth(NextHealthLevel.Value);
        _maxHealthRenderer.Render();
        RenderNextHealthLevel();
        RenderNextDamageLevel();
    }

    private void IncreaseDamage()
    {
        _saver.SetPlayerDamage(NextDamageLevel.Value);
        _damageRenderer.Render();
        RenderNextDamageLevel();
        RenderNextHealthLevel();
    }

    private void RenderNextHealthLevel()
    {
        if (HasHealthNextLevel)
            _healthPanel.Render(NextHealthLevel.Cost, _wallet);
        else
            _healthPanel.DeactivateBuyButton();
    }

    private void RenderNextDamageLevel()
    {
        if (HasDamageNextLevel)
            _damagePanel.Render(NextDamageLevel.Cost, _wallet);
        else
            _damagePanel.DeactivateBuyButton();
    }
}
