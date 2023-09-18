using System;

[Serializable]
public class SaveData
{
    public int PlayerMoney;
    public int PlayerMaxHealth;
    public int PlayerDamage;

    public string ZeroLocation;
    public string ShipLocation0;
    public string FirstLocation;
    public string ShipLocation1;
    public string SecondLocation;

    public string CurrentWeapon;
    public string Weapons;

    public float BaseSensitivity;
    public float ScopeSensitivity;

    public SaveData(string defaultString)
    {
        PlayerMaxHealth = Settings.Characteristics.Health.DefaultValue;
        PlayerDamage = Settings.Characteristics.Damage.DefaultValue;

        ZeroLocation = defaultString;
        ShipLocation0 = defaultString;
        FirstLocation = defaultString;
        ShipLocation1 = defaultString;
        SecondLocation = defaultString;
        CurrentWeapon = Settings.Shooting.DefaultWeapon.Id;
        Weapons = CurrentWeapon;
        BaseSensitivity = 0;
        ScopeSensitivity = 0;
    }
}
