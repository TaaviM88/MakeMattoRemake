using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerPortrait : MonoBehaviour
{
    public Image playerPortrait;
    public TMP_Text charName;


    public void UpdateInfo(GameObject character)
    {
        playerPortrait.sprite = character.GetComponent<Player>().characterAvatarImage;
        charName.text = $"Player {character.GetComponent<Player>().playerID}: {character.GetComponent<Player>().charName}";
    }
}
