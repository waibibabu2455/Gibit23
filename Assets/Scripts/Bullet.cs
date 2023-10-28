
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public int damage;

    public string colour;
    private Rigidbody2D rigidbody;
     
    
    // Start is called before the first frame update
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, 5f);
    }

    public void SetSpeed(Vector2 direction)
    {
        rigidbody.velocity = direction*speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Monster>() != null)
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    
}
