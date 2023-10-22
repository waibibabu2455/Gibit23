using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turrent : MonoBehaviour
{
    // Start is called before the first frame update

    public Arrow arrow;
    public int interval;
    public int timer;
    public int damage;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void FixedUpdate()
    {

        TurntoCamera();
        if (timer >= interval && GameObject.FindGameObjectWithTag("Monster")!=null)
        {
            Attack();
            timer = 0;
        }
        timer += 1;

    }
    void Attack() {
        Arrow arrowgenerated=Instantiate(arrow);
        arrowgenerated.transform.position = this.transform.position;
        arrowgenerated.tag= "Arrow";
        arrowgenerated.damage = damage;
        GameObject monster = GameObject.FindGameObjectWithTag("Monster");
        arrowgenerated.SetMonster(monster);
        Debug.Log("shoot");
    }

    void TurntoCamera() { 
        this.transform.rotation = Camera.main.transform.rotation;
        Debug.Log("Turn to camera");
    }
}
