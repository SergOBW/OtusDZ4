using System;
using UnityEngine;

namespace Lessons.Architecture.PM
{
    public sealed class UserInfo : IChangable
    {
        public event Action OnValueChangedEvent;
        
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Sprite Icon { get; private set; }

        private UserInfoSo _lastUserInfoSo;

        public void LoadData(UserInfoSo userInfoSo)
        {
            _lastUserInfoSo = userInfoSo;
            
            Name = userInfoSo.Name;
            Description = userInfoSo.Description;
            Icon = userInfoSo.Icon;
        }
        
        private void SaveData()
        {
            _lastUserInfoSo.Name = Name;
            _lastUserInfoSo.Description = Description;
            _lastUserInfoSo.Icon = Icon;
        }
        
        public void ChangeName(string name)
        {
            Name = name;
            OnValueChangedEvent?.Invoke();
            SaveData();
        }
        
        public void ChangeDescription(string description)
        {
            Description = description;
            OnValueChangedEvent?.Invoke();
            SaveData();
        }
        
        public void ChangeIcon(Sprite icon)
        {
            Icon = icon;
            OnValueChangedEvent?.Invoke();
            SaveData();
        }
        
    }
}