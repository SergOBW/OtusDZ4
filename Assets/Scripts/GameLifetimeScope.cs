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
    
    [SerializeField] private CharacterPopupView characterPopupView;
    
    [SerializeField] private CharacterInfoView characterInfoView;
    [SerializeField] private PlayerLevelView playerLevelView;
    [SerializeField] private DescriptionView descriptionView;
    [SerializeField] private CharacterStatView characterStatView;
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
        
        // Views
        builder.RegisterInstance(characterStatView).AsSelf();
        builder.Register<CharacterStatFactory>(Lifetime.Scoped).AsSelf();
        
        builder.RegisterInstance(characterInfoView).AsImplementedInterfaces().AsSelf();
        builder.RegisterInstance(playerLevelView).AsImplementedInterfaces().AsSelf();
        builder.RegisterInstance(descriptionView).AsImplementedInterfaces().AsSelf();
        
        // At last
        builder.RegisterInstance(characterPopupView).AsImplementedInterfaces().AsSelf();

        // Observers
        builder.Register<DescriptionObserver>(Lifetime.Scoped).AsSelf().AsImplementedInterfaces();
        builder.Register<CharacterInfoObserver>(Lifetime.Scoped).AsSelf().AsImplementedInterfaces();
        builder.Register<PlayerLevelObserver>(Lifetime.Scoped).AsSelf().AsImplementedInterfaces();
        builder.Register<CharacterStatObserver>(Lifetime.Scoped).AsSelf().AsImplementedInterfaces();
        
        // At last
        builder.Register<CharacterPopupObserver>(Lifetime.Scoped).AsSelf().AsImplementedInterfaces();
    }
}
