using System.Collections;
using UnityEngine;

public class Stopwatch
{
    private float _elapsedTime;
    private Coroutine _coroutine;

    public float ElapsedTime => _elapsedTime;
    public bool IsRunning { get; private set; }

    public void Start()
    {
        if (_coroutine != null)
            Settings.CoroutineObject.StopCoroutine(_coroutine);

        _elapsedTime = 0;
        _coroutine = Settings.CoroutineObject.StartCoroutine(CountTime());
    }

    public void Stop()
    {
        IsRunning = false;
    }

    private IEnumerator CountTime()
    {
        IsRunning = true;

        while (IsRunning)
        {
            _elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
