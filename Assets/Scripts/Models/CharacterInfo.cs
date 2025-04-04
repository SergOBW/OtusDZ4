using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lessons.Architecture.PM
{
    public sealed class CharacterInfo : IChangable
    {
        public event Action OnValueChanged;
        
        private HashSet<CharacterStat> _stats = new HashSet<CharacterStat>();

        private CharacterInfoSo _characterInfoSo;
        
        public void LoadData(CharacterInfoSo characterInfoSo)
        {
            _characterInfoSo = characterInfoSo;
            _stats.Clear();
        
            if (_characterInfoSo.Stats != null)
            {
                foreach (var stat in _characterInfoSo.Stats)
                {
                    if (stat != null)
                    {
                        _stats.Add(stat);
                    }
                }
            }
        }

        public void SaveData()
        {
            if (_characterInfoSo == null) return;
            
            if (_characterInfoSo.Stats == null)
            {
                _characterInfoSo.Stats = new List<CharacterStat>();
            }
            else
            {
                _characterInfoSo.Stats.Clear();
            }
            
            foreach (var stat in _stats)
            {
                _characterInfoSo.Stats.Add(stat);
            }
            
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(_characterInfoSo);
#endif
            
        }
        
        public void AddStat(CharacterStat stat)
        {
            if (_stats.Add(stat))
            {
                SaveData();
                OnValueChanged?.Invoke();
            }
        }
        
        public void RemoveStat(CharacterStat stat)
        {
            if (_stats.Remove(stat))
            {
                OnValueChanged?.Invoke();
            }
        }
        
        public void RemoveStat(string statName)
        {
            foreach (var characterStat in _stats)
            {
                if (characterStat.Name.Equals(statName) && _stats.Contains(characterStat))
                {
                    _stats.Remove(characterStat);
                    SaveData();
                    OnValueChanged?.Invoke();
                    break;
                }
            }
        }

        public CharacterStat GetStat(string name)
        {
            foreach (var stat in _stats)
            {
                if (stat.Name == name)
                {
                    return stat;
                }
            }

            throw new Exception($"Stat {name} is not found!");
        }

        public CharacterStat[] GetStats()
        {
            return _stats.ToArray();
        }
        
    }
}