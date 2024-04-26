using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : Collidable
{
    public int damage;
    public float pushForce;
    protected override void OnCollide(Collider2D col)
    {
        if(col.tag == "Player"){
            Damage dmg = new Damage{
                damagedeal = damage,
                origin = transform.position,
                pushForce = pushForce
            };
            col.SendMessage("ReceiveDamage",dmg);
        } 
    }
}
