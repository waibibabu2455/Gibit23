
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public int damage;
    private Rigidbody2D rigidbody;
     
    
    // Start is called before the first frame update
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void SetSpeed(Vector2 direction)
    {
        rigidbody.velocity = direction*speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    
}