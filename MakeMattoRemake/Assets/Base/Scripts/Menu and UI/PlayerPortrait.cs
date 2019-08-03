using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerPortrait : MonoBehaviour
{
    public Image playerPortrait;
    public TMP_Text charName, playerReadyTmp;

    bool ready = false;
    public void UpdateInfo(GameObject character)
    {
        if(!ready)
        {
            playerPortrait.sprite = character.GetComponent<Player>().characterAvatarImage;
            charName.text = $"Player {character.GetComponent<Player>().playerID}: {character.GetComponent<Player>().charName}";
        }
    }

    public void PlayerIsReady(bool b)
    {
        if (b)
        {
            playerReadyTmp.text = "Ready";
        }else
        {
            playerReadyTmp.text = "";
        }
        
        ready = b;
    }
}
