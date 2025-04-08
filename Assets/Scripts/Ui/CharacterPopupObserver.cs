using System;
using Lessons.Architecture.PM;
using VContainer;

public class CharacterPopupObserver : IDisposable
{
    private CharacterPopupView _characterPopupView;
    
    private CharacterInfo _characterInfo;
    private PlayerLevel _playerLevel;
    private UserInfo _userInfo;
    
    [Inject]
    public void Construct(CharacterPopupView characterPopupView)
    {
        _characterPopupView = characterPopupView;
        
        _characterPopupView.OnMainButtonClickedEvent += CharacterPopupViewOnMainButtonClicked;
        _characterPopupView.OnExitButtonClickedEvent += CharacterPopupViewOnExitButtonClicked;
    }
    
    public void SetData(CharacterInfo newCharacterInfo, PlayerLevel newPlayerLevel, UserInfo newUserInfo)
    {
        // Отписываемся от старых, если нужно
        if (_characterInfo != null)
            _characterInfo.OnValueChanged -= CharacterInfoOnValueChanged;

        if (_playerLevel != null)
            _playerLevel.OnValueChanged -= PlayerLevelOnValueChanged;

        if (_userInfo != null)
            _userInfo.OnValueChanged -= UserInfoOnValueChanged;

        _characterInfo = newCharacterInfo;
        _playerLevel = newPlayerLevel;
        _userInfo = newUserInfo;

        _characterInfo.OnValueChanged += CharacterInfoOnValueChanged;
        _playerLevel.OnValueChanged += PlayerLevelOnValueChanged;
        _userInfo.OnValueChanged += UserInfoOnValueChanged;
        
        CharacterInfoOnValueChanged();
        PlayerLevelOnValueChanged();
        UserInfoOnValueChanged();
    }

    public void ShowPopup()
    {
        _characterPopupView.Show();
    }
    
    public void HidePopup()
    {
        _characterPopupView.Hide();
    }

    private void CharacterInfoOnValueChanged()
    {
        _characterPopupView.DrawCharacterStats(_characterInfo.GetStats());
    }
    
    private void PlayerLevelOnValueChanged()
    {
        _characterPopupView.DrawPlayerLevel(_playerLevel);
    }
    
    private void UserInfoOnValueChanged()
    {
        _characterPopupView.DrawUserInfo(_userInfo);
    }
    
    void CharacterPopupViewOnMainButtonClicked()
    {
        _playerLevel.LevelUp();
    }
    
    void CharacterPopupViewOnExitButtonClicked()
    {
        HidePopup();
    }
    
    public void Dispose()
    {
        if (_characterInfo != null)
            _characterInfo.OnValueChanged -= CharacterInfoOnValueChanged;
        if (_playerLevel != null)
            _playerLevel.OnValueChanged -= PlayerLevelOnValueChanged;
        if (_userInfo != null)
            _userInfo.OnValueChanged -= UserInfoOnValueChanged;
        
        _characterPopupView.OnMainButtonClickedEvent -= CharacterPopupViewOnMainButtonClicked;
        _characterPopupView.OnExitButtonClickedEvent -= CharacterPopupViewOnExitButtonClicked;
    }
}