using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBack : MonoBehaviour
{
    private BoxCollider2D box;
    private Rigidbody2D rigidbody;
    private float width;
    private float speed = -3f;

    // Start is called before the first frame update
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        rigidbody = GetComponent<Rigidbody2D>();
        width = box.size.x;
        rigidbody.velocity = new Vector2(speed, 0);
    }

    // Update is called once per frame
    void Update()
    {
         if(transform.position.x < -width)
        {
           Reposition();
        }
    
    }
     private void Reposition()
    {
        Vector2 vector = new Vector2(width* 2f, 0);
        transform.position = (Vector2)transform.position + vector;
    }
}
