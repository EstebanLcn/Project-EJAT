using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
         public static HealthManager instance;

    public int currentHealth, maxHealth;
    public float invicibleTime = 2f;
    private float invicibleCounter;
    public SpriteRenderer theSR;

    public GameObject deathEffect;
    // Start is called before the first frame update
 private void Awake()
  {
        instance = this;
    }
        void Start()
    {
        currentHealth = maxHealth;
            UiManager.instance.healthBar.maxValue = maxHealth;
            UiManager.instance.healthBar.value = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(invicibleCounter >=0)
        {
            invicibleCounter -= Time.deltaTime;
            if(invicibleCounter <=0)
            {
                        theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, 1f); 

            }
        }
    }
    public void HurtPlayer()
    {
        if(invicibleCounter <=0)
        {

        currentHealth--;
                    UiManager.instance.healthBar.value = currentHealth;

        if(currentHealth <=0)
        {
            Instantiate(deathEffect, transform.position, transform.rotation);
            gameObject.SetActive(false);
            
            GameManager.instance.KillPlayer();
            WaveManager.instance.canSpwanWaves = false;
        }
    }
    }
    public void Respawn()
    {
        gameObject.SetActive(true);
        currentHealth = maxHealth;
        UiManager.instance.healthBar.value = currentHealth;

        invicibleCounter = invicibleTime;
        theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, .5f); 
    }
}
