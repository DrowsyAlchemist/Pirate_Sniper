using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelsMenu : Window
{
    [SerializeField] private RectTransform _container;
    [SerializeField] private LevelRenderer _levelRendererTemplate;

    public event Action<Level> LevelClicked;

    private List<LevelRenderer> _levelRenderers = new();

    private void OnDestroy()
    {
        foreach (var levelRenderer in _levelRenderers)
            levelRenderer.Clicked -= OnLevelClick;
    }

    public void RenderLocationLevels(Location location)
    {
        for (int i = 0; i < location.Levels.Count; i++)
        {
            if (i >= _levelRenderers.Count)
            {
                var levelRenderer = Instantiate(_levelRendererTemplate, _container);
                levelRenderer.Clicked += OnLevelClick;
                _levelRenderers.Add(levelRenderer);
            }
            _levelRenderers[i].Render(location.Levels[i]);
            _levelRenderers[i].Activate();
        }
        for (int i = location.Levels.Count; i < _levelRenderers.Count; i++)
            _levelRenderers[i].Deactivate();
    }

    private void OnLevelClick(Level level)
    {
        LevelClicked?.Invoke(level);
    }
}
