using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyController : MonoBehaviour
{
        public float movespeed;
        public Vector2 startDirection;
        public bool shouldChangeDirection;
        public float changeDirectionXPoint;
        public Vector2 ChangedDirection;
        public GameObject shot;
        public Transform firePoint;
         public float timeBetweenShots = .1f;
         public bool canShot;
         public bool allowShoot;
    private float ShotsCounter; 

    // Start is called before the first frame update
    void Start()
    {
        ShotsCounter = timeBetweenShots;
    }

    // Update is called once per frame
    void Update()
    {
       // transform.position -= new Vector3(movespeed * Time.deltaTime, 0f, 0f);
       if(!shouldChangeDirection)
       {
                  transform.position += new Vector3(startDirection.x * movespeed * Time.deltaTime, startDirection.y * movespeed * Time.deltaTime, 0f);

       }else
       if(transform.position.x > changeDirectionXPoint)
       {
                             transform.position += new Vector3(startDirection.x * movespeed * Time.deltaTime, startDirection.y * movespeed * Time.deltaTime, 0f);

       }else {
                             transform.position += new Vector3(ChangedDirection.x * movespeed * Time.deltaTime, ChangedDirection.y * movespeed * Time.deltaTime, 0f);

       }
       if(allowShoot)
       {
       ShotsCounter -= Time.deltaTime;
                if(ShotsCounter <=0)
                {
                     ShotsCounter = timeBetweenShots;
                  Instantiate(shot, firePoint.position, firePoint.rotation);
                 
                }
       }


    }
    private void OnBecameInvisible() {
        Destroy(gameObject);
    }
    private void OnBecameVisible() {
        if(canShot){
            allowShoot = true;
        }
    }
}
