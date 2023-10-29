using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCreate : MonoBehaviour
{
    public int interval;
    public int i;
    public List<GameObject> monsters;
    public bool canset;
    public PlayerController playerController;
    public CameraFollow camera;
    private void Start()
    {
        canset = false;
        i = 0;
        setinterval();
        PlayerController player= Instantiate(playerController, new Vector3(0, 0, 0),Quaternion.Euler(0,0,0));
        player.gameObject.tag = ("Player");
        camera.GetComponent<CameraFollow>().player = player;
        player.camera = camera;
    }
    // Update is called once per frame
    void Update()
    {   if (canset) {
            setinterval();
            canset = false;
        }
        if (i >= interval)
        {
            spawnmonster();
            i = 0;
            canset= true;
        }
        else {
            i += 1;
        }
    }
    void setinterval() { 
        interval=Random.Range(0, 3000);
    }
    void spawnmonster()
    {
        GameObject Monster = Instantiate(monsters[Random.Range(0, monsters.Count)]);
        Monster.transform.position = new Vector3(10, Random.Range(-5,3));
    }
}
