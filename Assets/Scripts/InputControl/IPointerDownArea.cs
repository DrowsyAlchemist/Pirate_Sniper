using System;

public interface IPointerDownArea
{
    public event Action PointerDown;

    public void SetActive(bool value);
}
