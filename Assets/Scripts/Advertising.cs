using Agava.YandexGames;
using System;
using UnityEngine;

public static class Advertising
{
    public static void ShowInter(AudioSource backgroundMusic)
    {
#if UNITY_EDITOR
        return;
#endif
        InterstitialAd.Show(
            onOpenCallback: () => backgroundMusic.Stop(),
            onCloseCallback: (_) => backgroundMusic.Play());
    }

    public static void RewardForVideo(Action reward, AudioSource backgroundMusic)
    {
#if UNITY_EDITOR
        reward();
        return;
#endif
        VideoAd.Show(
            onOpenCallback: () => backgroundMusic.Stop(),
            onCloseCallback: () => backgroundMusic.Play(),
            onRewardedCallback: reward,
            onErrorCallback: (_) => reward());
    }
}
