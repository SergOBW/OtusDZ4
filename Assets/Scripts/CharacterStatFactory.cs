using Lessons.Architecture.PM;
using UnityEngine;
using VContainer;

public class CharacterStatFactory
{
    private readonly IObjectResolver _resolver;
    private readonly CharacterStatView _prefab;

    public CharacterStatFactory(IObjectResolver resolver, CharacterStatView prefab)
    {
        _resolver = resolver;
        _prefab = prefab;
    }

    public CharacterStatView Create(CharacterStat model)
    {
        var view = Object.Instantiate(_prefab);
        view.gameObject.SetActive(true);
        
        var observer = _resolver.Resolve<CharacterStatObserver>();
        observer.Construct(view);
        observer.SetData(model);

        return view;
    }
}