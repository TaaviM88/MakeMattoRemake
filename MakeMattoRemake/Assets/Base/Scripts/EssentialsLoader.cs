using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialsLoader : MonoBehaviour
{
    public GameObject UIScreen;
    public GameObject[] players;
    public GameObject gameManager;
    public GameObject audioManager;
    public GameObject battleManager;

    // Start is called before the first frame update
    void Start()
    {
        //Activate if wanted a fade effect
        /* if (UIFade.instance == null)
         {
             UIFade.instance = Instantiate(UIScreen).GetComponent<UIFade>();
         }*/

        if (GameManager.instance == null)
        {
            Instantiate(gameManager);
        }
        if (AudioManager.instance == null)
        {
            Instantiate(audioManager);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
