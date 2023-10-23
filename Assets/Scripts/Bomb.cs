using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bomb : Arrow
{
    public float radius;
    public LayerMask mask;
    public Collider2D[] colliders;
    public override void SetDamage(GameObject o)
    {

        colliders = Physics2D.OverlapCircleAll(this.transform.position, radius,mask.value,0,2);

        foreach (Collider2D collider in colliders)
        {
            collider.GetComponent<Monster>().health -= damage;

        }
    }
    public override void shoot(GameObject o)
    {
        base.shoot(o);
    }
    private void Update()
    {
        Debug.Log(colliders);
    }

}
