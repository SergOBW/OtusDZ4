using TMPro;
using UnityEngine;

public class CharacterStatView : MonoBehaviour
{
    [SerializeField] private TMP_Text statText;
    
    public void UpdateText(string newName)
    {
        statText.text = newName;
    }
    
}