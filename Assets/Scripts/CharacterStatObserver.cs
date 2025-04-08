using System;
using Lessons.Architecture.PM;
using VContainer;

public class CharacterStatObserver : IDisposable
{
    private CharacterStatView _characterStatView;
    private CharacterStat _characterStat;

    [Inject]
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
    }

    private void CharacterStatOnValueChanged()
    {
        _characterStatView.UpdateName(_characterStat.Name);
        _characterStatView.UpdateValue(_characterStat.Value);
    }

    public void Dispose()
    {
        if (_characterStat != null)
        {
            _characterStat.OnValueChangedEvent -= CharacterStatOnValueChanged;
        }
    }
}