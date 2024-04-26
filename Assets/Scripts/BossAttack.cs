using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : Mover
{   
    //chasing
    private Transform playertransform;
    //attack
    public Animator attackAnim;
    private float attackRange = 0.3f;
    private float atkCooldown = 1.5f;
    private float lastAttack;
    protected override void Start()
    {
        base.Start();
        playertransform = GameManager.instance.player.transform;
    }
    private void Update() {
        Attack();
    }
    public void Attack(){
        //check player in range
        if(Vector2.Distance(transform.position,playertransform.position) > attackRange){
            UpdateMotor((playertransform.position - transform.position).normalized);
        }
        if(Vector2.Distance(transform.position,playertransform.position) < attackRange){
            if(Time.time - lastAttack > atkCooldown){
                lastAttack = Time.time;
                attackAnim.SetTrigger("Attack");
            }
        }
    }
}
