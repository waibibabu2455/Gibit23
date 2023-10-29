using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public PlayerController player;

    // Start is called before the first frame update

    // Update is called once per frame
    private void Start()
    {

    }
    void Update()
    {
        follow();
    }
    void follow() { 
        transform.position=new Vector3(player.transform.position.x,player.transform.position.y,-10);

    }
}
