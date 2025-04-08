using System;
using System.Collections.Generic;
using Lessons.Architecture.PM;
using VContainer;

public class CharacterInfoObserver : IDisposable
{
    private CharacterInfoView _characterInfoView;
    private CharacterInfo _characterInfo;
    private CharacterStatFactory _characterStatFactory;
    
    [Inject]
    public void Construct(CharacterInfoView characterInfoView, CharacterStatFactory characterStatFactory)
    {
        _characterInfoView = characterInfoView;
        _characterStatFactory = characterStatFactory;
    }

    public void SetData(CharacterInfo characterInfo)
    {
        if (_characterInfo != null)
        {
            _characterInfo.OnValueChangedEvent -= CharacterInfoOnValueChanged;
        }
        
        _characterInfo = characterInfo;
        _characterInfo.OnValueChangedEvent += CharacterInfoOnValueChanged;
        CharacterInfoOnValueChanged();
    }
    
    void CharacterInfoOnValueChanged()
    {
        List<CharacterStatView> characterStatViews = new List<CharacterStatView>();
        foreach (var characterStat in _characterInfo.GetStats())
        {
            CharacterStatView characterStatView = _characterStatFactory.Create(characterStat);
            characterStatViews.Add(characterStatView);
        }
        _characterInfoView.SpawnCharacterStats(characterStatViews);
    }
    
    public void Dispose()
    {
        if (_characterInfo != null)
        {
            _characterInfo.OnValueChangedEvent -= CharacterInfoOnValueChanged;
        }
    }
}