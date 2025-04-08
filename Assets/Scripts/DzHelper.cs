using System;
using System.Collections.Generic;
using Lessons.Architecture.PM;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;
using CharacterInfo = Lessons.Architecture.PM.CharacterInfo;

public interface IChangable
{
    public event Action OnValueChangedEvent;
}

public class DzHelper : MonoBehaviour
{
    private CharacterPopupObserver _characterPopupObserver;
    
    private List<UserInfo> _userInfos;
    private List<PlayerLevel> _playerLevels;
    private List<CharacterInfo> _characterInfos;

    private UserInfo _currentUserInfo => _userInfos[_currentUserIndex];
    private PlayerLevel _currentPlayerLevel => _playerLevels[_currentUserIndex];
    private CharacterInfo _currentCharacterInfo => _characterInfos[_currentUserIndex];
    
    private int _currentUserIndex;

    [Inject]
    public void Construct(CharacterPopupObserver characterPopupObserver, List<UserInfo> userInfos, List<PlayerLevel> playerLevels, List<CharacterInfo> characterInfos)
    {
        _characterPopupObserver = characterPopupObserver;
        _userInfos = userInfos;
        _playerLevels = playerLevels;
        _characterInfos = characterInfos;
    }

    [Button]
    public void ShowPopup()
    {
        _characterPopupObserver.SetData(_currentCharacterInfo, _currentPlayerLevel, _currentUserInfo);
        _characterPopupObserver.ShowPopup();
    }
    
    [Button]
    private void HidePopup()
    {
        _characterPopupObserver.HidePopup();
    }
    
    [Button]
    public void NextUser()
    {
        if (_currentUserIndex + 1 >= _userInfos.Count)
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
            _currentUserIndex = _userInfos.Count - 1;
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
