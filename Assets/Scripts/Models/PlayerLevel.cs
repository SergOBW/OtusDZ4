using System;

namespace Lessons.Architecture.PM
{
    public sealed class PlayerLevel : IChangable
    {
        public event Action OnValueChanged;
        
        public int CurrentLevel { get; private set; } = 1;
        public int CurrentExperience { get; private set; }
        public int RequiredExperience => 100 * (CurrentLevel + 1);

        private PlayerLevelSo _playerLevelSo;
        
        public void LoadData(PlayerLevelSo playerLevelSo)
        {
            _playerLevelSo = playerLevelSo;

            CurrentLevel = _playerLevelSo.CurrentLevel;
            CurrentExperience = _playerLevelSo.CurrentExperience;
        }

        private void SaveData()
        {
            _playerLevelSo.CurrentLevel = CurrentLevel;
            _playerLevelSo.CurrentExperience = _playerLevelSo.CurrentExperience;
        }
        
        public void AddExperience(int range)
        {
            var xp = Math.Min(CurrentExperience + range, RequiredExperience);
            CurrentExperience = xp;
            SaveData();
            OnValueChanged?.Invoke();
        }
        
        public void LevelUp()
        {
            if (CanLevelUp())
            {
                CurrentExperience = 0;
                CurrentLevel++;
                SaveData();
                OnValueChanged?.Invoke();
            }
        }

        public bool CanLevelUp()
        {
            return CurrentExperience == RequiredExperience;
        }
        
    }
}