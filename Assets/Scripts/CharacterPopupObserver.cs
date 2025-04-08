using System;
using Lessons.Architecture.PM;
using VContainer;

public class CharacterPopupObserver : IDisposable
{
    [Inject]
    private CharacterPopupView _characterPopupView;
    [Inject]
    private CharacterInfoView _characterInfoView;
    [Inject]
    private PlayerLevelView _playerLevelView;
    [Inject]
    private DescriptionView _descriptionView;
    
    [Inject]
    private CharacterInfoObserver _characterInfoObserver;
    [Inject]
    private PlayerLevelObserver _playerLevelObserver;
    [Inject]
    private DescriptionObserver _descriptionObserver;
    
    [Inject]
    public void Construct(CharacterPopupView characterPopupView)
    {
        _characterPopupView = characterPopupView;
        
        _characterPopupView.OnExitButtonClickedEvent += OnExitButtonClicked;
    }
    
    public void ShowPopup()
    {
        _characterPopupView.Show();
    }
    
    public void HidePopup()
    {
        _characterPopupView.Hide();
    }
    
    public void SetData(CharacterInfo newCharacterInfo, PlayerLevel newPlayerLevel, UserInfo newUserInfo)
    {
        _characterInfoObserver.SetData(newCharacterInfo);
        _playerLevelObserver.SetData(newPlayerLevel);
        _descriptionObserver.SetData(newUserInfo);
    }

    private void OnExitButtonClicked()
    {
        HidePopup();
    }
    
    public void Dispose()
    {
        _characterPopupView.OnExitButtonClickedEvent -= OnExitButtonClicked;
    }
}