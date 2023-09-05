using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Training : MonoBehaviour
{
    [SerializeField] TrainingPanel _trainingPanelTemplate;
    [SerializeField] private Task[] _tasks;

    public TrainingPanel _trainingPanel;
    private int _currentTask;

    public void Begin()
    {
        _trainingPanel = Instantiate(_trainingPanelTemplate);
        _tasks[_currentTask].Completed += BeginNextTask;
        _tasks[_currentTask].Init(_trainingPanel);
    }

    private void BeginNextTask()
    {
        _tasks[_currentTask].Completed -= BeginNextTask;

        if (_currentTask + 1 < _tasks.Length)
        {
            _currentTask++;
            _tasks[_currentTask].Completed += BeginNextTask;
        }
    }
}
