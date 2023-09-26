using Agava.YandexGames;
using System;
using UnityEngine;

public static class Advertising
{
    private const float SecondsBetweenInters = 65;
    private static Stopwatch _stopwatch;

    public static bool IsInterReady => _stopwatch.ElapsedTime > SecondsBetweenInters;
    public static bool IsRunning { get; private set; }

    static Advertising()
    {
        _stopwatch = new();
        _stopwatch.ReStart();
        IsRunning = false;
    }

    public static void ShowInter(Sound sound)
    {
#if UNITY_EDITOR
        return;
#endif
        if (IsInterReady && IsRunning == false)
        {
            _stopwatch.ReStart();
            InterstitialAd.Show(
                onOpenCallback: () =>
                {
                    sound.StopBackgroundMusic();
                    IsRunning = true;
                },
                onCloseCallback: (_) =>
                {
                    IsRunning = false;
                    sound.PlayBackgroundMusic();
                });
        }
    }

    public static void RewardForVideo(Action reward, Sound sound)
    {
#if UNITY_EDITOR
        reward();
        return;
#endif
        VideoAd.Show(
            onOpenCallback: () =>
            {
                sound.StopBackgroundMusic();
                IsRunning = true;
            },
            onCloseCallback: () =>
            {
                IsRunning = false;
                sound.PlayBackgroundMusic();
            },
            onRewardedCallback: reward,
            onErrorCallback: (_) => reward());
    }
}
