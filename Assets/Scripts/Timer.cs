using System;
using System.Collections;
using UnityEngine;

public class Timer
{
    private float _time;
    private float _elapsedTime;
    private Coroutine _coroutine;

    public float ElapsedTime => _elapsedTime;

    public event Action WentOff;

    public void Start(float time)
    {
        Reset();
        _time = time;
        _coroutine = Settings.CoroutineObject.StartCoroutine(CountTime());
    }

    public void Reset()
    {
        if (_coroutine != null)
            Settings.CoroutineObject.StopCoroutine(_coroutine);

        _elapsedTime = 0;
    }

    private IEnumerator CountTime()
    {
        while (_elapsedTime < _time)
        {
            _elapsedTime += Time.deltaTime;
            yield return null;
        }
        _elapsedTime = _time;
        WentOff?.Invoke();
    }
}
