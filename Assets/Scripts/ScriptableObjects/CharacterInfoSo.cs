using System.Collections.Generic;
using Lessons.Architecture.PM;
using UnityEngine;

[CreateAssetMenu()]
public class CharacterInfoSo : ScriptableObject
{
    public List<CharacterStat> Stats;
}
