using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public GameObject Monster;
    public int damage;
    // Start is called before the first frame update
    void Start()
    {   
        if (Monster!=null && transform.position.x > Monster.transform.position.x) {
            transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        Destroy(this.gameObject, 5f);

    }

    // Update is called once per frames
    void Update()
    {

    }
    private void FixedUpdate()
    {   if (this.Monster != null)
        {
            shoot(this.Monster);
        }
        else {
            transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y + speed * Time.deltaTime,0);
        }
    }
    public void shoot(GameObject o) {
        transform.Rotate(new Vector3(0, 0, Mathf.Atan((o.transform.position.y-transform.position.y)/(o.transform.position.x - transform.position.x))), Space.Self);
        transform.position = Vector3.MoveTowards(transform.position, o.transform.position, speed * Time.deltaTime);
        Vector3 dis = o.transform.position - transform.position;
        if (dis.magnitude < 1) {
            SetDamage(this.Monster);
            Destroy(this.gameObject);

        }
    }
    public void SetMonster(GameObject o) {
        this.Monster = o;
    }
    public void SetDamage(GameObject o)
    {
        o.GetComponentInChildren<Monster>().health -= damage;
    }
}
