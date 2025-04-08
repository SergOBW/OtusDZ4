using System;
using Lessons.Architecture.PM;
using UnityEngine;
using VContainer;
using Object = System.Object;

public class CharacterStatObserver : IDisposable
{
    private CharacterStatView _characterStatView;
    private CharacterStat _characterStat;
    
    public void Construct(CharacterStatView characterStatView)
    {
        _characterStatView = characterStatView;
    }

    public void SetData(CharacterStat characterStat)
    {
        if (_characterStat != null)
        {
            _characterStat.OnValueChangedEvent -= CharacterStatOnValueChanged;
        }
        
        _characterStat = characterStat;
        
        _characterStat.OnValueChangedEvent += CharacterStatOnValueChanged;
        CharacterStatOnValueChanged();
    }

    private void CharacterStatOnValueChanged()
    {
        _characterStatView.UpdateText($"{_characterStat.Name} : {_characterStat.Value}");
    }

    public void Dispose()
    {
        if (_characterStat != null)
        {
            _characterStat.OnValueChangedEvent -= CharacterStatOnValueChanged;
        }
    }
}