using System;

namespace Lessons.Architecture.PM
{
    [Serializable]
    public sealed class CharacterStat : IChangable
    {
        public CharacterStat(string statName, int statValue)
        {
            Name = statName;
            Value = statValue;
        }
        
        public event Action OnValueChangedEvent;
        
        public string Name { get; private set; }
        
        public int Value { get; private set; }
        
        public void ChangeValue(int value)
        {
            Value = value;
            OnValueChangedEvent?.Invoke();
        }
    }
}