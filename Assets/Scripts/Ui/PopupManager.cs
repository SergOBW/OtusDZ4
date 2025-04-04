using System;
using System.Collections.Generic;
using Lessons.Architecture.PM;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;
using CharacterInfo = Lessons.Architecture.PM.CharacterInfo;

public interface IChangable
{
    public event Action OnValueChanged;
}

public class PopupManager : MonoBehaviour
{
    [Inject]
    private CharacterPopup characterPopup;
    [Inject]
    private List<UserInfo> UserInfos { get; set; }
    [Inject]
    private List<PlayerLevel> PlayerLevels { get; set; }
    [Inject]
    private List<CharacterInfo> CharacterInfos { get; set; }

    private UserInfo _currentUserInfo => UserInfos[_currentUserIndex];
    private PlayerLevel _currentPlayerLevel => PlayerLevels[_currentUserIndex];
    private CharacterInfo _currentCharacterInfo => CharacterInfos[_currentUserIndex];
    
    private int _currentUserIndex;

    [Button]
    public void ShowPopup()
    {
        characterPopup.Show(_currentUserInfo,_currentPlayerLevel, _currentCharacterInfo);
    }
    
    [Button]
    private void HidePopup()
    {
        characterPopup.Hide();
    }
    
    [Button]
    public void NextUser()
    {
        if (_currentUserIndex + 1 >= UserInfos.Count)
        {
            _currentUserIndex = 0;
        }
        else _currentUserIndex++;
        

        ShowPopup();
    }
     
    [Button]
    public void PreviousUser()
    {
        if (_currentUserIndex - 1 < 0)
        {
            _currentUserIndex = UserInfos.Count - 1;
        }
        else _currentUserIndex--;

        ShowPopup();
    }
    
    [Button]
    private void ChangeUserDescription(string newValue)
    {
        _currentUserInfo.ChangeDescription(newValue);
    }
    
    [Button]
    private void ChangeUserName(string newValue)
    {
        _currentUserInfo.ChangeName(newValue);
    }
    
    [Button]
    private void ChangeUserAvatar(Sprite newValue)
    {
        _currentUserInfo.ChangeIcon(newValue);
    }
    
    [Button]
    private void PlayerAddExperience(int value)
    {
        _currentPlayerLevel.AddExperience(value);
    }
    
    [Button]
    private void PlayerLevelUp()
    {
        _currentPlayerLevel.LevelUp();
    }
    
    [Button]
    public void AddStat(string statName, int statValue)
    {
        _currentCharacterInfo.AddStat(new CharacterStat(statName, statValue));
    }

    [Button]
    public void RemoveStat(string statName)
    {
        _currentCharacterInfo.RemoveStat(statName);
    }
}
