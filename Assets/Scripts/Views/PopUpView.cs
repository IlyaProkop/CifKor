using System;
using UnityEngine;
using UnityEngine.UI;

public class PopUpView : MonoBehaviour
{
    [SerializeField] private Text popupTitle;
    [SerializeField] private Text popupDescription;
    [SerializeField] private Button okButton;

    public void Initialize(Action onClose)
    {
        okButton.onClick.AddListener(() => onClose?.Invoke());
    }

    public void UpdateView(string title, string description)
    {
        popupTitle.text = title;
        popupDescription.text = description;
    }

    public void Show()
    {        
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
