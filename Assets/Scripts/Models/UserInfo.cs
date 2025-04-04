using System;
using UnityEngine;

namespace Lessons.Architecture.PM
{
    public sealed class UserInfo : IChangable
    {
        public event Action OnValueChanged;
        
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
            OnValueChanged?.Invoke();
            SaveData();
        }
        
        public void ChangeDescription(string description)
        {
            Description = description;
            OnValueChanged?.Invoke();
            SaveData();
        }
        
        public void ChangeIcon(Sprite icon)
        {
            Icon = icon;
            OnValueChanged?.Invoke();
            SaveData();
        }
        
    }
}