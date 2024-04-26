using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Collidable
{
    public GameObject hiteffect;
    public int damage = 1;
    protected override void Start() {

        base.Start();
    }
    protected override void OnCollide(Collider2D col)
    {
        if(col.tag == "Enemy"){
            
            Damage dmg = new Damage{
                damagedeal = damage,
                origin = transform.position,
                //pushForce = pushForce //maybe dont need
            };
            col.SendMessage("ReceiveDamage",dmg);
        }
        GameObject effect = Instantiate(hiteffect,transform.position,Quaternion.identity);
        Destroy(effect,0.6f);
        Destroy(gameObject);
        
    }
}
