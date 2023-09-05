public class ReadTask : StopTimeTask
{
    protected override void BeginTask()
    {
        TrainingPanel.SetContinueButtonActive(true);
        TrainingPanel.ContinueButtonClicked += Complete;
        InputController.SetMode(InputMode.UI);
        base.BeginTask();
    }

    protected override void OnComplete()
    {
        TrainingPanel.ContinueButtonClicked -= Complete;
        base.OnComplete();
    }
}
