using System;

public interface IPointerUpArea
{
    public event Action PointerUp;

    public void CheckForMouseUp();
}
