using System;
using Lessons.Architecture.PM;
using VContainer;

public class DescriptionObserver : IDisposable
{
    private UserInfo _userInfo;
    private DescriptionView _descriptionView;
    [Inject]
    public void Construct(DescriptionView descriptionView)
    {
        _descriptionView = descriptionView;
    }

    public void SetData(UserInfo newUserInfo)
    {
        if (_userInfo != null)
        {
            _userInfo.OnValueChangedEvent -= UserInfoOnValueChanged;
        }
        
        _userInfo = newUserInfo;
        _userInfo.OnValueChangedEvent += UserInfoOnValueChanged;
        UserInfoOnValueChanged();
    }
    
    public void Dispose()
    {
        if (_userInfo != null)
        {
            _userInfo.OnValueChangedEvent -= UserInfoOnValueChanged;
        }
    }
    
    private void UserInfoOnValueChanged()
    {
        _descriptionView.UpdateUserName(_userInfo.Name);
        _descriptionView.UpdateDescription(_userInfo.Description);
        _descriptionView.UpdateAvatarIcon(_userInfo.Icon);
    }
    
}