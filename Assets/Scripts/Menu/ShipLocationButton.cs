public class ShipLocationButton : LocationButton
{
    protected override bool IsLocked()
    {
        return base.IsPreviousLocationCompleted() == false;
    }
}
