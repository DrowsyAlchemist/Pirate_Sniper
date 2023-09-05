using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrainingPanel : MonoBehaviour
{
    [SerializeField] private Image _fadePanel;
    [SerializeField] private RectTransform _notePanel;
    [SerializeField] private TMP_Text _noteText;
    [SerializeField] private UIButton _continueButton;

    public event Action ContinueButtonClicked;

    private void Awake()
    {
        _continueButton.AddOnClickAction(() => ContinueButtonClicked?.Invoke());
    }

    public void SetNote(string note)
    {
        _noteText.text = note;
    }

    public void HideFadePanel()
    {
        _fadePanel.Deactivate();
    }

    public void SetHighlightedObject(RectTransform highlightedObject)
    {
        transform.SetParent(highlightedObject.parent);
        highlightedObject.SetSiblingIndex(highlightedObject.parent.childCount - 1);
        _fadePanel.Activate();
    }

    public void SetInRightCorner()
    {

    }

    public void SetInLeftCorner()
    {

    }

    public void SetContinueButtonActive(bool value)
    {
        _continueButton.SetActive(value);
    }
}
