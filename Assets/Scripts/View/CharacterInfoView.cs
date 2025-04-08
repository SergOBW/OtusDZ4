using System.Collections.Generic;
using UnityEngine;

public class CharacterInfoView : MonoBehaviour
{
    [Header("Character Info")]
    [SerializeField] private Transform characterStatsGrid;
    [SerializeField] private CharacterStatView characterStatView;
    
    private List<CharacterStatView> _spawnedStats = new List<CharacterStatView>();
    
    public void SpawnCharacterStats(List<CharacterStatView> characterStatViews)
    {
        foreach (var oldStat in _spawnedStats)
        {
            DestroyImmediate(oldStat.gameObject);
        }
        
        _spawnedStats = new List<CharacterStatView>(characterStatViews);

        foreach (var characterStatView in _spawnedStats)
        {
            characterStatView.transform.SetParent(characterStatsGrid, false);
            characterStatView.gameObject.SetActive(true);
        }
    }
}
