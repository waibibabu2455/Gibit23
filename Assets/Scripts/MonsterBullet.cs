using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBullet : MonoBehaviour
{
    public float lifetime = 2f;
    public string bulletColor;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

}
