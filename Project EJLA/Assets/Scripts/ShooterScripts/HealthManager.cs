using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
         public static HealthManager instance;

    public int currentHealth, maxHealth;

    public GameObject deathEffect;
    // Start is called before the first frame update
 private void Awake()
  {
        instance = this;
    }
        void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void HurtPlayer()
    {
        currentHealth--;
        if(currentHealth <=0)
        {
            Instantiate(deathEffect, transform.position, transform.rotation);
            gameObject.SetActive(false);
        }
    }
}
