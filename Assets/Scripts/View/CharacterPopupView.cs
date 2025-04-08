using System;
using UnityEngine;
using UnityEngine.UI;
using VContainer.Unity;

public class CharacterPopupView : MonoBehaviour, IInitializable
{
    public event Action OnExitButtonClickedEvent;
    
    [Header("Buttons")]
    [SerializeField] private Button closeButton;
    
    public void Initialize()
    {
        Hide();
    }

    #region Show / Hide

    public void Show()
    {
        closeButton.onClick.AddListener(OnCloseButtonClicked);
        
        gameObject.SetActive(true);
    }
    
    public void Hide()
    {
        closeButton.onClick.RemoveListener(OnCloseButtonClicked);
        
        gameObject.SetActive(false);
    }

    #endregion
    
    
    private void OnCloseButtonClicked()
    {
        OnExitButtonClickedEvent?.Invoke();
    }
    
}
