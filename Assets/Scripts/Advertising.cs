using Agava.YandexGames;
using System;

public static class Advertising
{
    public static void ShowInter()
    {
#if UNITY_EDITOR
        return;
#endif
        InterstitialAd.Show(
            onOpenCallback: () => Sound.BackgroundMusic.Stop(),
            onCloseCallback: (_) => Sound.BackgroundMusic.Play());
    }

    public static void RewardForVideo(Action reward)
    {
#if UNITY_EDITOR
        reward();
        return;
#endif
        VideoAd.Show(
            onOpenCallback: () => Sound.BackgroundMusic.Stop(),
            onCloseCallback: () => Sound.BackgroundMusic.Play(),
            onRewardedCallback: reward,
            onErrorCallback: (_) => reward());
    }
}
