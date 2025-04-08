using System;
using Lessons.Architecture.PM;
using VContainer;

public class PlayerLevelObserver : IDisposable
{
    private PlayerLevelView _playerLevelView;
    private PlayerLevel _playerLevel;

    [Inject]
    public void Construct(PlayerLevelView playerLevelView)
    {
        _playerLevelView = playerLevelView;
        
        _playerLevelView.OnLevelUpButtonClickedEvent += PlayerLevelViewOnLevelUpButtonClicked;
    }

    public void SetData(PlayerLevel playerLevel)
    {
        if (_playerLevel != null)
        {
            _playerLevel.OnValueChangedEvent -= PlayerLevelOnValueChanged;
        }
        
        _playerLevel = playerLevel;
        
        _playerLevel.OnValueChangedEvent += PlayerLevelOnValueChanged;
    }
    
    private void PlayerLevelViewOnLevelUpButtonClicked()
    {
        _playerLevel.LevelUp();
    }
    
    private void PlayerLevelOnValueChanged()
    {
        _playerLevelView.UpdateCurrentExperience(_playerLevel.CurrentExperience);
        _playerLevelView.UpdateCurrentLevel(_playerLevel.CurrentLevel);
        _playerLevelView.UpdateSliderValues(_playerLevel.RequiredExperience, _playerLevel.CurrentExperience);
    }
    
    public void Dispose()
    {
        if (_playerLevel != null)
        {
            _playerLevel.OnValueChangedEvent -= PlayerLevelOnValueChanged;
        }
        
        _playerLevelView.OnLevelUpButtonClickedEvent -= PlayerLevelViewOnLevelUpButtonClicked;
    }
}