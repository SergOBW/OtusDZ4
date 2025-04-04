using System.Collections.Generic;
using Lessons.Architecture.PM;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using CharacterInfo = Lessons.Architecture.PM.CharacterInfo;

public class GameLifetimeScope : LifetimeScope
{
    private List<UserInfo> _userInfos = new List<UserInfo>();
    private List<PlayerLevel> _playerLevels = new List<PlayerLevel>();
    private List<CharacterInfo> _characterInfos = new List<CharacterInfo>();
    
    [SerializeField] private CharacterPopup characterPopup;
    protected override void Configure(IContainerBuilder builder)
    {
        foreach (UserInfoSo userInfoSo in Resources.LoadAll<UserInfoSo>("UserInfos"))
        {
            UserInfo userInfo = new UserInfo();
            userInfo.LoadData(userInfoSo);
            _userInfos.Add(userInfo);
        }
        
        foreach (PlayerLevelSo playerLevelSo in Resources.LoadAll<PlayerLevelSo>("PlayerLevels"))
        {
            PlayerLevel playerLevel = new PlayerLevel();
            playerLevel.LoadData(playerLevelSo);
            _playerLevels.Add(playerLevel);
        }
        
        foreach (CharacterInfoSo characterInfoSo in Resources.LoadAll<CharacterInfoSo>("ChacterInfos"))
        {
            CharacterInfo characterInfo = new CharacterInfo();
            characterInfo.LoadData(characterInfoSo);
            _characterInfos.Add(characterInfo);
        }

        builder.RegisterInstance(_userInfos).As<IReadOnlyList<UserInfo>>();
        builder.RegisterInstance(_playerLevels).As<IReadOnlyList<PlayerLevel>>();
        builder.RegisterInstance(_characterInfos).As<IReadOnlyList<CharacterInfo>>();

        characterPopup.Hide();
        builder.RegisterInstance(characterPopup).AsSelf();
    }
}
