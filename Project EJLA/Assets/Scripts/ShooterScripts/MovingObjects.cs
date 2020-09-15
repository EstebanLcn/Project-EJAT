using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObjects : MonoBehaviour
{
    public float movespeed;
   
       // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(movespeed * Time.deltaTime, 0f, 0f);
    }
    private void OnBecameInvisible() {
        Destroy(gameObject);
    }
}
