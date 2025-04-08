using TMPro;
using UnityEngine;

public class CharacterStatView : MonoBehaviour
{
    [SerializeField] private TMP_Text statNameText;
    [SerializeField] private TMP_Text statValueText;

    public void UpdateName(string newName)
    {
        statNameText.text = newName;
    }

    public void UpdateValue(int newValue)
    {
        statNameText.text = newValue.ToString();
    }
}