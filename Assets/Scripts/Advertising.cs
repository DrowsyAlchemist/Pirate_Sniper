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

    public static void ShowInter(AudioSource backgroundMusic)
    {
#if UNITY_EDITOR
        return;
#endif
        if (IsInterReady)
        {
            _stopwatch.ReStart();
            InterstitialAd.Show(
                onOpenCallback: () =>
                {
                    backgroundMusic.Stop();
                    IsRunning = true;
                },
                onCloseCallback: (_) =>
                {
                    backgroundMusic.Play();
                    IsRunning = false;
                });
        }
    }

    public static void RewardForVideo(Action reward, AudioSource backgroundMusic)
    {
#if UNITY_EDITOR
        reward();
        return;
#endif
        VideoAd.Show(
            onOpenCallback: () =>
            {
                backgroundMusic.Stop();
                IsRunning = true;
            },
            onCloseCallback: () =>
            {
                backgroundMusic.Play();
                IsRunning = false;
            },
            onRewardedCallback: reward,
            onErrorCallback: (_) => reward());
    }
}
