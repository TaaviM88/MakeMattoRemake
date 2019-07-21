using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Player stats")]
    public string charName = "";
    private float currentHealth = 10;
    [SerializeField]
    private float MaxHp = 100;

    private float currentShield = 0;

    [SerializeField]
    private float MaxShield = 100;
    bool shieldOn = false;

    [Header("Spell Settings")]
    public Transform spellStartPoint;


    public GameObject baseSpell;
    
    private GameObject currentSpell;

    [SerializeField]
    private GameObject specialSpell;

    public float fireRate = 1;
    private float nextFire;

    private GameObject shield;
    //private GameObject weaponSlotObj;
    private float currentWeaponRange = 1f;
    private float cWeaponReloadTime = 0;

    bool dead = false;

    PlayerMovement pMovement;
    public int playerID = 0;

    public Sprite characterAvatarImage;
    //Weapon wp;
    void Start()
    {
        pMovement = GetComponent<PlayerMovement>();
        pMovement.playerID = playerID;
        currentHealth = MaxHp;
        currentShield = MaxShield;

        currentSpell = baseSpell;
        switch(playerID)
        {
            case 1:
                gameObject.layer = 10;
                break;
            case 2:
                gameObject.layer = 11;
                break;
        }

        GameObject obj = GameObject.FindObjectOfType<Cinemachine.CinemachineTargetGroup>().gameObject;
        obj.GetComponent<Cinemachine.CinemachineTargetGroup>().AddMember(gameObject.transform, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale > 0)
        {
            switch(playerID)
            {
                #region Player1
                case 1:
                    if(Input.GetButton("Fire1") && Time.time > nextFire)
                    {
                        //use basic spell
                        CastSpell();
                        nextFire = Time.time + fireRate;
                    }
                    break;
                #endregion
                #region Player2
                case 2:
                    if (Input.GetButton("Fire4") && Time.time > nextFire)
                    {
                        //use basic spell
                        CastSpell();
                        nextFire = Time.time + fireRate;
                    }
                    break;
#endregion
                default:
                    Debug.Log($"{gameObject.name} doesn't have proper playerID. Let's use player 1 controls.  Player-script");
                    if (Input.GetButton("Fire1"))
                    {
                        //use basic spell
                    }
                    break;
            }
        }

        if(Input.GetButtonDown("kill"))
        {
            Debug.Log("I kill myself for science!PS: this is just a test");
            TakeDamageHP(MaxHp);
        }
    }

    public void CastSpell()
    {
        GameObject spellClone;
        if (transform.rotation.y == 0)
        {
            spellClone = Instantiate(currentSpell, spellStartPoint.position, Quaternion.Euler(0, 90, 0));
        }
        else
        {
            spellClone = Instantiate(currentSpell, spellStartPoint.position, Quaternion.Euler(0, 270, 0));
        }
        
        
        //test if we can give spell our gameobject rotation. If it doesn't work change to upper one.
        //GameObject spellClone = Instantiate(currentSpell, spellStartPoint.position, transform.rotation);
        Spell spell = spellClone.GetComponent<Spell>();
        spell.playerID = playerID;

    }

    public void CastSpecialSpell()
    {
        Debug.Log("Hahaaa! Special Attack! I just need an effect ;_;");
    }

    public void UpdateSpell(GameObject newSpell)
    {
        currentSpell = newSpell;
    }

    public void RollBackBasicSpell()
    {
        currentSpell = baseSpell;
    }

    public void TakeDamageHP(float damage)
    {
        //instantiate hit particle effect(if needed)
        currentHealth = Mathf.Max(currentHealth - damage, 0);
        Debug.Log($"{gameObject.name} Takes damage {damage} Healt left {currentHealth}");
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        //Dying stuff
        if (!dead)
        {
            dead = true;
            Debug.Log($"Player{playerID} dies");
            GameManager.instance.RemovePlayer(gameObject);
            gameObject.SetActive(false);
        }
        

    }
}
