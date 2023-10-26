using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public BoxCollider2D collision2d;
    public int health;
    public float speed;
    public int Exp;
    private GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }


    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, 0);
        if (health <= 0) {
            Destroy(this.gameObject);
            Player.GetComponent<PlayerController>().ExpUp(Exp);

        }
        if (transform.position.x < -10) {
            Destroy(this.gameObject);

        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet")) {
            health -= collision.GetComponent<Bullet>().damage;
        }
    }
}
