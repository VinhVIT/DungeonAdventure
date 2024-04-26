using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Collidable
{
    private GameObject target;
    public float speed;
    private Rigidbody2D rb;
    private int damage = 1;
    private float pushForce = 1f;
    protected override void Start() {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        Vector2 moveDir = (target.transform.position - transform.position).normalized * speed;
        rb.velocity = new Vector2(moveDir.x,moveDir.y);
    }
    protected override void OnCollide(Collider2D col)
    {
        if(col.CompareTag("Player")){
            
            Damage dmg = new Damage{
                damagedeal = damage,
                origin = transform.position,
                pushForce = pushForce
            };
            col.SendMessage("ReceiveDamage",dmg);
            Destroy(gameObject);
        }
        if(col.CompareTag("Blocking")){
            Destroy(gameObject);
        }
    }
}
