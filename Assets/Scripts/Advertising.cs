using Agava.YandexGames;
using System;

public static class Advertising
{
    public static void RewardForAd(Action reward)
    {
#if UNITY_EDITOR
        reward();
        return;
#endif
        VideoAd.Show(onRewardedCallback: reward);
    }
}
