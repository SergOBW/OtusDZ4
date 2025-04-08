using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevelView : MonoBehaviour
{
    [Header("Player Level Info")]
    [SerializeField] private TMP_Text playerCurrentLevelText;
    [SerializeField] private TMP_Text playerExperienceText; 
    [SerializeField] private Slider playerExperienceSlider;
    
    [Header("Buttons")]
    [SerializeField] private Button levelUpButton;
    
    public event Action OnLevelUpButtonClickedEvent;

    #region OnEnable/OnDisable

    private void OnEnable()
    {
        levelUpButton.onClick.AddListener(OnMainButtonClicked);
    }

    private void OnDisable()
    {
        levelUpButton.onClick.RemoveListener(OnMainButtonClicked);
    }

    private void OnMainButtonClicked()
    {
        OnLevelUpButtonClickedEvent?.Invoke();
    }

    #endregion

    public void UpdateCurrentLevel(int currentLevel)
    {
        playerCurrentLevelText.text = currentLevel.ToString();
    }

    public void UpdateCurrentExperience(int currentExperience)
    {
        playerExperienceText.text = currentExperience.ToString();
    }

    public void UpdateSliderValues(int requiredExperience, int currentExperience)
    {
        playerExperienceSlider.minValue = 0;
        playerExperienceSlider.maxValue = requiredExperience;

        playerExperienceSlider.value = currentExperience;
    }
}