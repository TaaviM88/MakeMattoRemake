using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BattleUIManager : MonoBehaviour
{
    public static BattleUIManager instance;
    public float[] PlayersHP;
    TMP_Text timerText;
    public float roundTimer = 10f;
    private float currentRoundTime;

    //Canvas
    //HP:t pelaajille
    //Elämät pelaajille?
    //Timer
    //Erä laskuri? vai GM:n puolelle?
    //
    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        currentRoundTime = roundTimer;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdatePlayerHealth(GameObject player)
    {
       //Update here players HP
    }


}
