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

    public static void ShowInter(Sound sound, Action onClose = null)
    {
#if UNITY_EDITOR
        onClose?.Invoke();
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
                    onClose?.Invoke();
                });
        }
        else
        {
            onClose?.Invoke();
        }
    }

    public static void RewardForVideo(Action reward, Sound sound, Action onClose = null)
    {
#if UNITY_EDITOR
        reward();
        onClose?.Invoke();
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
                onClose.Invoke();
            },
            onRewardedCallback: reward,
            onErrorCallback: (_) =>
            {
                reward();
                onClose.Invoke();
            });
    }
}
