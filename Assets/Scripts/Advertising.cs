using Agava.YandexGames;
using System;
using UnityEngine;

public static class Advertising
{
    private const float SecondsBetweenInters = 65;
    private static Stopwatch _stopwatch;

    public static bool IsInterReady => _stopwatch.ElapsedTime > SecondsBetweenInters;

    static Advertising()
    {
        _stopwatch = new();
        _stopwatch.ReStart();
    }

    public static void ShowInter(AudioSource backgroundMusic)
    {
#if UNITY_EDITOR
        return;
#endif
        if (IsInterReady)
        {
            _stopwatch.ReStart();
            InterstitialAd.Show(
                onOpenCallback: () => backgroundMusic.Stop(),
                onCloseCallback: (_) => backgroundMusic.Play());
        }
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
