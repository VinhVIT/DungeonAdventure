using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour
{
    public int hitpoint = 99;
    public int maxHitpoint = 99;
    private float immuneTime = 0.5f;
    private float lastImmune;
    public EnemyHealthbar enemyHealthbar;
    public GameObject enemyHeath;
    private BoxCollider2D hitbox;
    public ContactFilter2D filter;
    private bool collidewithPlayer;
    private Collider2D[] hits = new Collider2D[10];
    private void Start()
    {   
        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }
    private void FixedUpdate() {
        hitbox.OverlapCollider(filter,hits);
        for (int i = 0; i < hits.Length; i++)
        {
            if(hits[i] == null){
                continue;
            }
            if(hits[i].tag == "Player"){
            }
            hits[i] = null;
        }
    }
    private void ReceiveDamage(Damage dmg){
        //time.time = rightnow
        if(Time.time - lastImmune > immuneTime){
            lastImmune = Time.time;
            hitpoint -= dmg.damagedeal;
            enemyHeath.SetActive(true);
            enemyHealthbar.OnHitPointChange(hitpoint,maxHitpoint);
            GetComponent<Animator>().SetTrigger("Hurt");
            //Add damage text
            DamagePopUp.Create(transform.position,dmg.damagedeal);
            //die condition
            if(hitpoint <= 0){
            hitpoint = 0;
            Death();
            }
        }
    }
    private void Death(){
        Destroy(gameObject);
    }
}
