using System.Collections.Generic;
using UnityEngine;

public class CharacterInfoView : MonoBehaviour
{
    [Header("Character Info")]
    [SerializeField] private Transform characterStatsGrid;
    [SerializeField] private CharacterStatView characterStatView;
    
    private List<CharacterStatView> _spawnedStats = new List<CharacterStatView>();
    
    public void UpdateCharacterInfo(int stastAmount)
    {
        foreach (var oldStat in _spawnedStats)
        {
            DestroyImmediate(oldStat);
        }
        
        _spawnedStats = new List<CharacterStatView>();

        for (int i = 0; i < stastAmount; i++)
        {
            CharacterStatView characterStatUiItem = Instantiate(characterStatView, characterStatsGrid);
            characterStatUiItem.gameObject.SetActive(true);
            _spawnedStats.Add(characterStatUiItem);
        }
    }
}
