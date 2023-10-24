using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Arrow : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public GameObject Monster;
    public Vector3 MonsterPosition;
    public Vector3 FinalPosition;
    public int damage;
    public bool cancal;
    // Start is called before the first frame update
    void Start()
    {
        MonsterPosition = this.Monster.transform.position;
        cancal = true;
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
    {   
        if (this.Monster != null)
        {   
            MonsterPosition = this.Monster.transform.position;
            shoot(this.Monster);
        }
        else {
            if (cancal)
            {
                FinalPosition = 20000 * MonsterPosition - 19999 * transform.position;
            }
            cancal = false;
            transform.position = Vector3.MoveTowards(transform.position, FinalPosition, speed * Time.deltaTime);
            transform.Rotate(new Vector3(0, 0, Mathf.Atan((FinalPosition.y-transform.position.y) / (FinalPosition.x-transform.position.x))));

        }
    }
    public virtual void shoot(GameObject o) {
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
    public virtual void SetDamage(GameObject o)
    {
        o.GetComponentInChildren<Monster>().health -= damage;
    }
}
