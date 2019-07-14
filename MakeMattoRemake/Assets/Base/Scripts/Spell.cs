using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    [Header("Spell Settings")]
    public string Spellname = " ";
    private float sTimer = 0;
    public float fadeTime;

    public float force = 10f;
    public float damage = 10;
    private Rigidbody _rb;
    public ParticleSystem particleLauncher;
    public GameObject spellHitParticleEffect;
    public int playerID = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        sTimer += Time.deltaTime;
        if(sTimer >= fadeTime)
        {
            Disable();
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if(other.gameObject.GetComponent<Player>() && other.gameObject.GetComponent<PlayerMovement>().playerID != playerID)
        {
            other.gameObject.GetComponent<Player>().TakeDamageHP(damage);
        }
    }

    public void Disable()
    {
        //do stuff if needed
        Destroy(gameObject);
    }
}
