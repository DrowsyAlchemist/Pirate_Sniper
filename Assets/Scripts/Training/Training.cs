using UnityEngine;

public class Training : MonoBehaviour
{
    [SerializeField] TrainingPanel _trainingPanel;
    [SerializeField] private Task[] _tasks;

    private int _currentTask;

    public bool IsTraining { get; private set; }

    private void Awake()
    {
        _trainingPanel.Deactivate();
    }

    public void Begin()
    {
        if (IsTraining == false)
        {
            _tasks[_currentTask].Completed += BeginNextTask;
            _tasks[_currentTask].Begin(_trainingPanel);
            IsTraining = true;
        }
    }

    private void BeginNextTask()
    {
        _tasks[_currentTask].Completed -= BeginNextTask;

        if (_currentTask + 1 < _tasks.Length)
        {
            _currentTask++;
            _tasks[_currentTask].Begin(_trainingPanel);
            _tasks[_currentTask].Completed += BeginNextTask;
        }
        else
        {
            IsTraining = false;
        }
    }
}
