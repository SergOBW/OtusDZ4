using System;
using System.Collections.Generic;
using Lessons.Architecture.PM;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer.Unity;

public class CharacterPopupView : MonoBehaviour, IInitializable
{
    public event Action OnMainButtonClickedEvent;
    public event Action OnExitButtonClickedEvent;
    
    [Header("User Info")]
    [SerializeField] private TMP_Text userNameText; 
    [SerializeField] private TMP_Text userDescriptionText; 
    [SerializeField] private Image userAvatarImage;
    
    [Header("Player Info")]
    [SerializeField] private TMP_Text playerCurrentLevelText;
    [SerializeField] private TMP_Text playerExperienceText; 
    [SerializeField] private Slider playerExperienceSlider; 
    
    [Header("Character Info")]
    [SerializeField] private GameObject characterStatUiItemPrefab;
    [SerializeField] private Transform characterStatsGrid;
    
    [Header("Buttons")]
    [SerializeField] private Button mainButton;
    [SerializeField] private Button closeButton;

    private List<GameObject> _spawnedStats = new List<GameObject>();
    
    public void Initialize()
    {
        Hide();
    }

    #region Show / Hide

    public void Show()
    {
        mainButton.onClick.AddListener(OnMainButtonClicked);
        closeButton.onClick.AddListener(OnCloseButtonClicked);
        
        gameObject.SetActive(true);
    }
    
    public void Hide()
    {
        mainButton.onClick.RemoveListener(OnMainButtonClicked);
        closeButton.onClick.RemoveListener(OnCloseButtonClicked);
        
        gameObject.SetActive(false);
    }

    #endregion

    public void DrawPlayerLevel(PlayerLevel playerLevel)
    {
        playerCurrentLevelText.text = $"Level : {playerLevel.CurrentLevel}";
        playerExperienceText.text = $"XP : {playerLevel.CurrentExperience} / {playerLevel.RequiredExperience}";
        
        playerExperienceSlider.minValue = 0;
        playerExperienceSlider.maxValue = playerLevel.RequiredExperience;

        playerExperienceSlider.value = playerLevel.CurrentExperience;

        if (playerLevel.CanLevelUp())
        {
            mainButton.interactable = true;
        } 
        else  mainButton.interactable = false;
    }
    
    public void DrawCharacterStats(CharacterStat[] characterStats)
    {
        foreach (var oldStat in _spawnedStats)
        {
            DestroyImmediate(oldStat);
        }

        if (characterStats is null || characterStats.Length <= 0)
        {
            return;
        }

        _spawnedStats = new List<GameObject>();
        
        foreach (var characterStat in characterStats)
        {
            GameObject characterStatUiItem = Instantiate(characterStatUiItemPrefab, characterStatsGrid);
            TMP_Text characterStatText =  characterStatUiItem.GetComponentInChildren<TMP_Text>();
            characterStatText.text = $"{characterStat.Name} : {characterStat.Value}";
            characterStatUiItem.gameObject.SetActive(true);
            _spawnedStats.Add(characterStatUiItem);
        }
    }

    public void DrawUserInfo(UserInfo userInfo)
    {
        userNameText.text = userInfo.Name;
        userDescriptionText.text = userInfo.Description;
        userAvatarImage.sprite = userInfo.Icon;
    }

    private void OnMainButtonClicked()
    {
        OnMainButtonClickedEvent?.Invoke();
    }
    
    private void OnCloseButtonClicked()
    {
        OnExitButtonClickedEvent?.Invoke();
    }
    
}
