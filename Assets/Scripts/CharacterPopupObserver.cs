using System;
using Lessons.Architecture.PM;
using VContainer;

public class CharacterPopupObserver : IDisposable
{
    private CharacterPopupView _characterPopupView;
    
    private CharacterInfo _characterInfo;
    
    [Inject]
    public void Construct(CharacterPopupView characterPopupView)
    {
        _characterPopupView = characterPopupView;
        
        _characterPopupView.OnExitButtonClickedEvent += CharacterPopupViewOnExitButtonClicked;
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

    }

    private void CharacterPopupViewOnExitButtonClicked()
    {
        
    }
    
    public void Dispose()
    {
        _characterPopupView.OnExitButtonClickedEvent -= CharacterPopupViewOnExitButtonClicked;
    }
}