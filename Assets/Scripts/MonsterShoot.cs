
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterShoot : MonoBehaviour
{

    public 　List<GameObject>　monsterBulletPrefabLs;
    public float shootInterval;

    public float bulletSpeed;

    private GameObject Player;

    public GameObject currentMonster;

    private float timer = 0f;

    void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= shootInterval)
        {
            Shoot();
            timer = 0f;
        }
    }

    void Shoot()
    {
        string currentColor = currentMonster.GetComponent<Monster>().currentColor;
        GameObject bullet = null;
            switch(currentColor)
                {
                    case "YELLOW":
                        bullet = Instantiate(monsterBulletPrefabLs[0], transform.position, Quaternion.identity);
                        break;

                    case "CYAN":
                        bullet = Instantiate(monsterBulletPrefabLs[2], transform.position, Quaternion.identity);
                        break;
                    
                    case "PURPLE":
                        bullet = Instantiate(monsterBulletPrefabLs[1], transform.position, Quaternion.identity);
                        break;
                        
                    default:
                        break;
                }

            if(bullet != null)
            {
                Vector2 direction = (GetPlayerPosition() - (Vector2)transform.position).normalized;

                bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
                if (bullet.transform.position.y < Player.transform.position.y)
                {
                    bullet.transform.rotation = Quaternion.Euler(0, 0, -Mathf.Rad2Deg * Mathf.Atan((Player.transform.position.x - bullet.transform.position.x) / (Player.transform.position.y - bullet.transform.position.y)));
                }
                else {
                    bullet.transform.rotation = Quaternion.Euler(0, 0, 180-Mathf.Rad2Deg * Mathf.Atan((Player.transform.position.x - bullet.transform.position.x) / (Player.transform.position.y - bullet.transform.position.y)));
                }
            }
    }
            

    Vector2 GetPlayerPosition()
    {
        if (Player != null)
        {
            return new Vector2(Player.transform.position.x, Player.transform.position.y);
        }
        else
        {
            return Vector2.zero;
        }
    }
}
