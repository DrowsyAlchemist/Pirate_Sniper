using UnityEngine;

public class Training : MonoBehaviour
{
    [SerializeField] private LocationsStorage _locationsStorage;
    [SerializeField] private Level _level;
    [SerializeField] private TrainingOfferWindow _trainingOfferWindow;
    [SerializeField] TrainingPanel _trainingPanel;
    [SerializeField] private Task[] _tasks;

    private int _currentTask;

    public bool IsTraining { get; private set; }

    private void Awake()
    {
        _level.LevelLoaded += OnLevelLoaded;
        _trainingOfferWindow.AgreeButtonClicked += Begin;
        _trainingPanel.Deactivate();
        _trainingPanel.CancelButtonClicked += ForceStopTraining;
        _trainingOfferWindow.Deactivate();
    }

    private void OnDestroy()
    {
        _level.LevelLoaded -= OnLevelLoaded;
        _trainingOfferWindow.AgreeButtonClicked -= Begin;
        _trainingOfferWindow.CancelButtonClicked -= ForceStopTraining;
    }

    private void OnLevelLoaded()
    {
        if (_locationsStorage.GetLocationIndex(_level.CurrentLevel) == 0 && _locationsStorage.GetIndexInLocation(_level.CurrentLevel) == 0)
            if (IsTraining == false)
                _trainingOfferWindow.Open();
    }

    private void Begin()
    {
        if (IsTraining)
            throw new System.InvalidOperationException();

        _currentTask = -1;
        BeginNextTask();
        IsTraining = true;
    }

    private void BeginNextTask()
    {
        if (_currentTask >= 0)
            _tasks[_currentTask].Completed -= BeginNextTask;

        if (_currentTask + 1 < _tasks.Length)
        {
            ResetTrainingPanel();
            _currentTask++;
            _tasks[_currentTask].Completed += BeginNextTask;
            _tasks[_currentTask].Begin(_trainingPanel);
        }
        else
        {
            StopTraining();
        }
    }

    private void ResetTrainingPanel()
    {
        _trainingPanel.SetContinueButtonActive(false);
        _trainingPanel.SetCancelButtonActive(false);
        _trainingPanel.HideFadePanel();
        _trainingPanel.SetGameInteractable(true);
    }

    private void StopTraining()
    {
        if (InputController.InputMode == InputMode.Game)
            _level.PauseGame();

        _trainingPanel.Deactivate();
        IsTraining = false;
    }

    private void ForceStopTraining()
    {
        _tasks[_currentTask].Completed -= BeginNextTask;
        _tasks[_currentTask].ForceComplete();
        StopTraining();
    }
}
