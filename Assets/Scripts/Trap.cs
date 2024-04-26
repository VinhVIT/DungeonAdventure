using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : Collidable
{   
    private Animator anim;
    private float cooldown = 4f;
    private float lastActiveTrap;
    public int damage = 1;
    private int pushForce = 5;
    protected override void Start() {
        base.Start();
        anim = GetComponent<Animator>();
    }
    protected override void Update()
    {       
        base.Update();
        if(Time.time - lastActiveTrap > cooldown){
            lastActiveTrap = Time.time;
            ActiveTrap();
        }
    }
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
    public void ActiveTrap(){    
        anim.SetTrigger("Active");
    }
}
