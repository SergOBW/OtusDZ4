using System;
using System.Collections.Generic;
using System.Linq;
using Lessons.Architecture.PM;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using CharacterInfo = Lessons.Architecture.PM.CharacterInfo;

public class CharacterPopup : MonoBehaviour
{
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

    private CharacterInfo _characterInfo;
    private PlayerLevel _playerLevel;
    private UserInfo _userInfo;

    #region Show / Hide

    public void Show(UserInfo userInfo, PlayerLevel playerLevel, CharacterInfo characterInfo)
    {
        _userInfo = userInfo;
        _playerLevel = playerLevel;
        _characterInfo = characterInfo;
    
        _userInfo.OnValueChanged += HandleUserInfoChanged;
        _playerLevel.OnValueChanged += HandlePlayerLevelChanged;
        _characterInfo.OnValueChanged += HandleCharacterInfoChanged;
        
        mainButton.onClick.AddListener(OnMainButtonClicked);
        closeButton.onClick.AddListener(OnCloseButtonClicked);
        
        HandleUserInfoChanged();
        HandlePlayerLevelChanged();
        HandleCharacterInfoChanged();
        
        gameObject.SetActive(true);
    }

    private void HandleCharacterInfoChanged()
    {
        DrawCharacterStats(_characterInfo.GetStats());
    }

    private void HandlePlayerLevelChanged()
    {
        DrawPlayerLevel(_playerLevel);
    }

    private void HandleUserInfoChanged()
    {
        DrawUserInfo(_userInfo);
    }

    public void Hide()
    {
        if (_userInfo != null)
        {
            _userInfo.OnValueChanged -= HandleUserInfoChanged;
        }

        if (_playerLevel != null)
        {
            _playerLevel.OnValueChanged -= HandlePlayerLevelChanged;
        }

        if (_characterInfo != null)
        {
            _characterInfo.OnValueChanged -= HandleCharacterInfoChanged;
        }
        
        mainButton.onClick.RemoveListener(OnMainButtonClicked);
        closeButton.onClick.RemoveListener(OnCloseButtonClicked);
        
        gameObject.SetActive(false);
    }

    #endregion

    private void DrawPlayerLevel(PlayerLevel playerLevel)
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
    
    private void DrawCharacterStats(CharacterStat[] characterStats)
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

    private void DrawUserInfo(UserInfo userInfo)
    {
        userNameText.text = userInfo.Name;
        userDescriptionText.text = userInfo.Description;
        userAvatarImage.sprite = userInfo.Icon;
    }

    private void OnMainButtonClicked()
    {
        _playerLevel.LevelUp();
    }


    private void OnCloseButtonClicked()
    {
        Hide();
    }
}
