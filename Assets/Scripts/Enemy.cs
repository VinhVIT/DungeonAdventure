using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover
{
    //exp
    public int xpValue = 1; 
    //logic
    public float triggerLenght = 1f;
    public float chaseLengt = 5f;
    public bool chasing ;
    public ContactFilter2D filter;
    //spawn
    public float spawnTime = 60f;
    //health
    public EnemyHealthbar enemyHealthbar;
    public GameObject enemyHeath;
    //chasing
    protected bool collidewithPlayer;
    protected Transform playertransform;
    protected Vector3 startingPositon;
    //hitbox
    private BoxCollider2D hitbox;
    //
    public GameObject bloodEffect;
    
    private Collider2D[] hits = new Collider2D[10];
    protected override void Start()
    {
        base.Start();
        playertransform = GameManager.instance.player.transform;
        startingPositon = transform.position;
        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }
    protected virtual void FixedUpdate(){
        //check player in range
        if(Vector3.Distance(playertransform.position,startingPositon) < chaseLengt){      
            if(Vector3.Distance(playertransform.position,startingPositon) < triggerLenght){
                chasing = true;
            }
            if(chasing){
                if(!collidewithPlayer){
                    UpdateMotor((playertransform.position - transform.position).normalized);
                }
            }
            else{
                UpdateMotor(startingPositon - transform.position);
            }
        }
        else{
            UpdateMotor(startingPositon - transform.position);
            chasing = false;
        }
        collidewithPlayer = false;
        hitbox.OverlapCollider(filter,hits);
        for (int i = 0; i < hits.Length; i++)
        {
            if(hits[i] == null){
                continue;
            }
            if(hits[i].tag == "Player"){
                collidewithPlayer = true;
            }
            hits[i] = null;
        }
    }
    protected override void ReceiveDamage(Damage dmg){
        //time.time = rightnow
        if(Time.time - lastImmune > immuneTime){
            lastImmune = Time.time;
            hitpoint -= dmg.damagedeal;

            enemyHeath.SetActive(true);
            enemyHealthbar.OnHitPointChange(hitpoint,maxHitpoint);
            GetComponent<Animator>().SetTrigger("Hurt");
            
            pushDirection = (transform.position - dmg.origin).normalized *dmg.pushForce;
            if(gameObject.tag == "Player"){
                //reduce current Healthbar
                player.Healthbar.sizeDelta = new Vector2(player.Healthbar.sizeDelta.x -6.5f *dmg.damagedeal,player.Healthbar.sizeDelta.y);
            }
            //Add damage text
            DamagePopUp.Create(transform.position,dmg.damagedeal);
            //die condition
            if(hitpoint <= 0){
            hitpoint = 0;
            Death();
            }
        }
    }
    protected override void Death()
    {
        //Destroy(gameObject);
        gameObject.SetActive(false);
        Instantiate(bloodEffect,transform.position,Quaternion.identity);
        GameManager.instance.XPGain(xpValue);
        GameManager.instance.Showtext("Gain "+ xpValue + "exp", 25,Color.magenta,transform.position,Vector3.up *40,1f);
        Invoke("respawn",spawnTime);
    }
    private void respawn(){
        //reset a enemy ability
        hitpoint = maxHitpoint;
        enemyHeath.SetActive(false);
        gameObject.transform.position = startingPositon;
        gameObject.SetActive(true);
        //reset a enemy health
        enemyHealthbar.healthbar.localScale = new Vector3(1,1,1);
        enemyHealthbar.barSprite.color = Color.green;
    }
    IEnumerator DeathEffect(){
        
        yield return new WaitForSeconds(0.3f);
        Destroy(bloodEffect);
    }
}
