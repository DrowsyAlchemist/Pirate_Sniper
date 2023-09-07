using UnityEngine;

public class Training : MonoBehaviour
{
    [SerializeField] private Level _level;
    [SerializeField] private TrainingOfferWindow _trainingOfferWindow;
    [SerializeField] TrainingPanel _trainingPanel;
    [SerializeField] private Task[] _tasks;

    private int _currentTask;

    public bool IsTraining { get; private set; }

    private void Awake()
    {
        _trainingPanel.Deactivate();
        _trainingOfferWindow.Close();
        _level.LevelLoaded += OnLevelLoaded;
        _trainingOfferWindow.AgreeButtonClicked += Begin;
    }

    private void OnDestroy()
    {
        _level.LevelLoaded -= OnLevelLoaded;
        _trainingOfferWindow.AgreeButtonClicked -= Begin;
    }

    private void OnLevelLoaded()
    {
        if (_level.CurrentLevel.Location.Index == 0 && _level.CurrentLevel.IndexInLocation == 0)
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
            _trainingPanel.Deactivate();
            IsTraining = false;
        }
    }

    private void ResetTrainingPanel()
    {
        _trainingPanel.SetContinueButtonActive(false);
        _trainingPanel.HideFadePanel();
        _trainingPanel.SetGameInteractable(true);
    }
}
