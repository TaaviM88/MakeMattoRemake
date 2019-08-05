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

    public float scrollRate = 1;
    private float nextScroll1, nextScroll2;

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

        //let's check that our counter is not bigger than our list if it's for somereason let's set it to last item of the list. 
        if (prefabIndexP1 > buttonList.Count)
        {
            prefabIndexP1 = buttonList.Count;

        }

        if (prefabIndexP2 > buttonList.Count)
        {
            prefabIndexP2 = buttonList.Count;
        }

        //Change button color to doublecolor because both players are in  same button;
        if (prefabIndexP1 == prefabIndexP2)
        {
            buttonList[prefabIndexP1].GetComponent<CharacterButton>().ChangeColor(3);
        }

        //Update avatars for start
        buttonList[prefabIndexP1].GetComponent<CharacterButton>().UpdateAvatar(1);
        buttonList[prefabIndexP2].GetComponent<CharacterButton>().UpdateAvatar(2);
    }

    public void Update()
    {
        //Player 1
        //if (Input.GetKeyDown(KeyCode.S) && !readyP1)
        if (!readyP1)
        {

            if (Input.GetAxis("Vertical") > 0 && Time.time > nextScroll1)
            {
                buttonList[prefabIndexP1].GetComponent<CharacterButton>().ChangeColor(0);
                prefabIndexP1++;
                if (prefabIndexP1 < buttonList.Count)
                {
                    CharacterButton charB = buttonList[prefabIndexP1].GetComponent<CharacterButton>(); //eventSystem.currentSelectedGameObject.GetComponent<CharacterButton>();
                    charB.UpdateAvatar(1);
                }
                else
                {
                    prefabIndexP1 = 0;
                    CharacterButton charB = buttonList[prefabIndexP1].GetComponent<CharacterButton>(); //eventSystem.currentSelectedGameObject.GetComponent<CharacterButton>();
                    charB.UpdateAvatar(1);
                }

                buttonList[prefabIndexP1].GetComponent<CharacterButton>().ChangeColor(1);
                nextScroll1 = Time.time + scrollRate;
            }
            else if (Input.GetAxis("Vertical") < 0 && Time.time > nextScroll1)
            {
                buttonList[prefabIndexP1].GetComponent<CharacterButton>().ChangeColor(0);

                prefabIndexP1--;
                if (prefabIndexP1 < 0)
                {
                    prefabIndexP1 = buttonList.Count - 1;
                    CharacterButton charB = buttonList[prefabIndexP1].GetComponent<CharacterButton>(); //eventSystem.currentSelectedGameObject.GetComponent<CharacterButton>();
                    charB.UpdateAvatar(1);
                }
                else
                {
                    CharacterButton charB = buttonList[prefabIndexP1].GetComponent<CharacterButton>(); //eventSystem.currentSelectedGameObject.GetComponent<CharacterButton>();
                    charB.UpdateAvatar(1);
                }

                buttonList[prefabIndexP1].GetComponent<CharacterButton>().ChangeColor(1);
                nextScroll1 = Time.time + scrollRate;
            }

            if (Input.GetButtonUp("Fire1"))
            {
                CharacterButton charB = buttonList[prefabIndexP1].GetComponent<CharacterButton>(); //eventSystem.currentSelectedGameObject.GetComponent<CharacterButton>();
                charB.Press(1);
                readyP1 = true;
            }
        }
        //player 2 
        if (!readyP2)
        {
            if (Input.GetKeyDown(KeyCode.K) && Time.deltaTime > nextScroll2)
            {
                buttonList[prefabIndexP2].GetComponent<CharacterButton>().ChangeColor(0);
                prefabIndexP2++;
                if (prefabIndexP2 < buttonList.Count)
                {
                    //    eventSystem.SetSelectedGameObject(buttonList[prefabIndexP2]);

                    CharacterButton charB = buttonList[prefabIndexP2].GetComponent<CharacterButton>(); //eventSystem.currentSelectedGameObject.GetComponent<CharacterButton>();
                    charB.UpdateAvatar(2);


                }
                else
                {
                    prefabIndexP2 = 0;
                    CharacterButton charB = buttonList[prefabIndexP2].GetComponent<CharacterButton>(); //eventSystem.currentSelectedGameObject.GetComponent<CharacterButton>();
                    charB.UpdateAvatar(2);

                }

                buttonList[prefabIndexP2].GetComponent<CharacterButton>().ChangeColor(2);
                nextScroll2 = Time.time + scrollRate;
            }

            if (Input.GetButtonUp("Fire2") && !readyP2)
            {
                CharacterButton charB = buttonList[prefabIndexP2].GetComponent<CharacterButton>(); //eventSystem.currentSelectedGameObject.GetComponent<CharacterButton>();
                charB.Press(2);
                readyP2 = true;
            }
        }


        if (prefabIndexP1 == prefabIndexP2)
        {
            buttonList[prefabIndexP1].GetComponent<CharacterButton>().ChangeColor(3);
        }
        else
        {
            buttonList[prefabIndexP1].GetComponent<CharacterButton>().ChangeColor(1);
            buttonList[prefabIndexP2].GetComponent<CharacterButton>().ChangeColor(2);
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
