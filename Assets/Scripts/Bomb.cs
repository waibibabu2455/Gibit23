using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bomb : Arrow
{
    public float radius;
    public LayerMask mask;
    public override void SetDamage(GameObject o)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(this.transform.position.x,transform.position.y), radius);
        foreach (Collider2D collider in colliders)
        {
            collider.GetComponent<Monster>().health -= damage;

        }
    }
    public override void shoot(GameObject o)
    {
        base.shoot(o);
    }
}
