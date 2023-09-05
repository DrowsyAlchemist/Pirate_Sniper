public class ReadTask : StopTimeTask
{
    protected override void Begin()
    {
        TrainingPanel.SetContinueButtonActive(true);
        TrainingPanel.ContinueButtonClicked += Complete;
        base.Begin();
    }

    protected override void OnComplete()
    {
        TrainingPanel.ContinueButtonClicked -= Complete;
        base.OnComplete();
    }
}
