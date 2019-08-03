using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    public static MainMenu instance;
    public GameObject characterPrefabButton;
    public GameObject characterButtonPanel;
    public GameObject playerPortrait1, playerPortrait2;
    public List<GameObject> characterPrefabs = new List<GameObject>();
    public List<GameObject> buttonList = new List<GameObject>();
    //Index for characterPrefabs list
    private int prefabIndexP1 = 0, prefabIndexP2 = 0;
    private bool readyP1 = false, readyP2 = false; 
    EventSystem eventSystem;
    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        eventSystem = EventSystem.current;

        foreach (GameObject character in Resources.LoadAll<GameObject>("CharactersPrefabs"))
        {
            characterPrefabs.Add(character);
        }

        GenerateCharacterButtons();


    }

    public void Update()
    {
        //Player 1
        if (Input.GetKeyDown(KeyCode.S) && !readyP1)
        {
            prefabIndexP1++;
            if (prefabIndexP1 < buttonList.Count)
            {
                //eventSystem.SetSelectedGameObject(buttonList[prefabIndexP1]);

                CharacterButton charB = buttonList[prefabIndexP1].GetComponent<CharacterButton>(); //eventSystem.currentSelectedGameObject.GetComponent<CharacterButton>();
                charB.UpdateAvatar(1);
                Debug.Log($"Current button is {buttonList[prefabIndexP1].name}");
            }
            else
            {
                prefabIndexP1 = 0;
                CharacterButton charB = buttonList[prefabIndexP1].GetComponent<CharacterButton>(); //eventSystem.currentSelectedGameObject.GetComponent<CharacterButton>();
                charB.UpdateAvatar(1);
                //eventSystem.SetSelectedGameObject(buttonList[prefabIndexP1]);
                Debug.Log($"Reset! Current button is {buttonList[prefabIndexP1].name}");
            }

        }

        if (Input.GetButtonUp("Fire1") && !readyP1)
        {
            CharacterButton charB = buttonList[prefabIndexP1].GetComponent<CharacterButton>(); //eventSystem.currentSelectedGameObject.GetComponent<CharacterButton>();
            charB.Press(1);
            readyP1 = true;
        }

        //player 2 
        if (Input.GetKeyDown(KeyCode.K) && !readyP2)
        {
            prefabIndexP2++;
            if (prefabIndexP2 < buttonList.Count)
            {
            //    eventSystem.SetSelectedGameObject(buttonList[prefabIndexP2]);

                CharacterButton charB = buttonList[prefabIndexP2].GetComponent<CharacterButton>(); //eventSystem.currentSelectedGameObject.GetComponent<CharacterButton>();
                charB.UpdateAvatar(2);

                Debug.Log($"Current button is {buttonList[prefabIndexP2].name}");
            }
            else
            {
                prefabIndexP2 = 0;
                CharacterButton charB = buttonList[prefabIndexP2].GetComponent<CharacterButton>(); //eventSystem.currentSelectedGameObject.GetComponent<CharacterButton>();
                charB.UpdateAvatar(2);
                Debug.Log($"Reset! Current button is {buttonList[prefabIndexP2].name}");
            }

        }

        if (Input.GetButtonUp("Fire2") && !readyP2)
        {
            CharacterButton charB = buttonList[prefabIndexP2].GetComponent<CharacterButton>(); //eventSystem.currentSelectedGameObject.GetComponent<CharacterButton>();
            charB.Press(2); 
            readyP2 = true;
        }
    }

    public void GenerateCharacterButtons()
    {
        if (characterPrefabs.Count > 0)
        {
            for (int i = 0; i < characterPrefabs.Count; i++)
            {

                GameObject itemb = Instantiate(characterPrefabButton);
                itemb.transform.parent = characterButtonPanel.transform;
                itemb.transform.localScale = new Vector3(1, 1, 1);
                buttonList.Add(itemb);
                itemb.GetComponent<CharacterButton>().AddCharacter(characterPrefabs[i]);
                /* if (i == 0)
                 {
                     eventSystem.firstSelectedGameObject = itemb;
                     prefabIndex = 0;
                 }*/
            }
        }
        else
        {
            Debug.Log("There ain't any characters to load");
        }
    }

    public void PressStartLevel()
    {
        SceneManager.LoadScene("Mutti");
    }
}
