using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionView : MonoBehaviour
{
    [Header("User Info")]
    [SerializeField] private TMP_Text userNameText; 
    [SerializeField] private TMP_Text userDescriptionText; 
    [SerializeField] private Image userAvatarImage;
    
    public void UpdateUserName(string userName)
    {
        userNameText.text = userName;
    }
    
    public void UpdateDescription(string description)
    {
        userDescriptionText.text = description;
    }
    
    public void UpdateAvatarIcon(Sprite avatarSprite)
    {
        userAvatarImage.sprite = avatarSprite;
    }
}
