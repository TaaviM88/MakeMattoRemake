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

    EventSystem eventSystem;
    // Start is called before the first frame update
    void Start()
    {
        if(instance != null )
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


    public void GenerateCharacterButtons()
    {
        if(characterPrefabs.Count > 0)
        {
            for (int i = 0; i < characterPrefabs.Count; i++)
            {
               
                GameObject itemb = Instantiate(characterPrefabButton);
                itemb.transform.parent = characterButtonPanel.transform;
                itemb.transform.localScale = new Vector3(1, 1, 1);
                itemb.GetComponent<CharacterButton>().AddCharacter(characterPrefabs[i]);
                if (i == 0)
                {
                    eventSystem.firstSelectedGameObject = itemb;
                }
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
