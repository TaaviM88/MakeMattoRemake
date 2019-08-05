using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class CharacterButton : MonoBehaviour
{
    public GameObject characterPrefab;

    public TMP_Text nameSlot, spellNameSlot;
    public Image portrait;
    private Color originalColorl, p1Color,p2Color, doubleColor;
    private Image image;
    
    //EventSystem _eventS;
   //  EventSystem _eventS1;
   // EventSystem _eventS2;
   // new List<GameObject> events = new List<GameObject>();
    // Start is called before the first frame update
    void Awake()
    {
        //   _eventS = EventSystem.current;
        /*  foreach (EventSystem e in GameObject.FindObjectsOfType<EventSystem>())
          {
             if(e.name == "cursor1")
              {
                  Debug.Log("Löyty cursor1");
                  _eventS1 = e;
              }

              if (e.name == "cursor2")
              {
                  Debug.Log("Löyty cursor2");
                  _eventS2 = e;
              }
          }
         */
        image = GetComponent<Image>();

        originalColorl = image.color;
        p1Color = Color.blue;
        p2Color = Color.red;
        doubleColor = new Color(1, 0, 1);
    }


    public void Press(int id)
    {
        if (characterPrefab != null)
        {
           switch(id)
            {
                case 1:
                    //Debug.Log("Player 1 pressed");
                    GameObject clone1 = characterPrefab;
                    clone1.GetComponent<Player>().playerID = id;
                    GameManager.instance.AddPlayerToList(clone1,id);
                    MainMenu.instance.playerPortrait1.GetComponent<PlayerPortrait>().UpdateInfo(clone1);
                    MainMenu.instance.playerPortrait1.GetComponent<PlayerPortrait>().PlayerIsReady(true);
                    break;
                case 2:
                    GameObject clone2 = characterPrefab;
                    clone2.GetComponent<Player>().playerID = id;
                    GameManager.instance.AddPlayerToList(clone2,id);
                    MainMenu.instance.playerPortrait2.GetComponent<PlayerPortrait>().UpdateInfo(clone2);
                    MainMenu.instance.playerPortrait2.GetComponent<PlayerPortrait>().PlayerIsReady(true);
                    break;
                default:
                    Debug.Log($"There aren't correct player ID");
                    break;
            }

            /* _eventS1.GetComponent<StandaloneInputModule>().submitButton = "Fire1";
             if (_eventS1.currentSelectedGameObject == this && _eventS1.GetComponent<StandaloneInputModule>().submitButton == "Fire1")
             {
                 Debug.Log("Player 1 pressed");
                 characterPrefab.GetComponent<Player>().playerID = 1;
                 GameManager.instance.AddPlayerToList(characterPrefab);
                 MainMenu.instance.playerPortrait1.GetComponent<PlayerPortrait>().UpdateInfo(characterPrefab);
             }

             if (_eventS2.currentSelectedGameObject == this && _eventS1.GetComponent<StandaloneInputModule>().submitButton == "Player2Accept")
             {
                 Debug.Log("Player 2 pressed");
                 characterPrefab.GetComponent<Player>().playerID = 2;
                 Debug.Log(characterPrefab.GetComponent<Player>().playerID);
                 GameManager.instance.AddPlayerToList(characterPrefab);
                 MainMenu.instance.playerPortrait2.GetComponent<PlayerPortrait>().UpdateInfo(characterPrefab);
             }

             */
        }
        else
        {
            Debug.Log($"There is no fucking character you fucking  idiot!");
        }

    }
    public void AddCharacter(GameObject newObject)
    {
        characterPrefab = newObject;
        nameSlot.text = newObject.GetComponent<Player>().charName;
        spellNameSlot.text = $"Spell:\n{newObject.GetComponent<Player>().baseSpell.GetComponent<Spell>().Spellname}";
        portrait.sprite = newObject.GetComponent<Player>().characterAvatarImage;

    }

    public void UpdateAvatar(int id)
    {
        if(id == 1)
        {
            MainMenu.instance.playerPortrait1.GetComponent<PlayerPortrait>().UpdateInfo(characterPrefab);
        }

        if(id == 2)
        {
            MainMenu.instance.playerPortrait2.GetComponent<PlayerPortrait>().UpdateInfo(characterPrefab);
        }
    }

    public void ChangeColor(int id)
    {
        switch(id)
        {
            case 0:
                image.color = originalColorl;
                break;

            case 1:
                image.color = p1Color;
                break;
            case 2:
                image.color = p2Color;
                break;
            case 3:
                if(image.color != doubleColor)
                {
                    image.color = doubleColor;
                }

                break;

            default:
                image.color = originalColorl;
                break;
        }
    }
}
