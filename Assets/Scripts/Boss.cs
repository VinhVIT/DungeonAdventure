using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : Mover
{   
//exp
    public int xpValue = 1;
    public Slider healthBar;
    //logic
    public float triggerLenght = 1f;
    public float chaseLengt = 5f;
    public bool chasing;
    public ContactFilter2D filter;
    public Animator bossanim;
    private bool isEngage = false;
    private Vector3 startingPositon;
    //hitbox
    private BoxCollider2D hitbox;
    private BoxCollider2D bosscol;
    private Collider2D[] hits = new Collider2D[10];
    //reference scripts
    public BossAttack bossAttack;
    public BossSkill1 bossSkill1;
    public BossSkill2 bossSkill2;
    //drop weapon when engage
    public GameObject weapon;
    public Animator weapAnim;
    protected override void Start()
    {
        base.Start();
        startingPositon = transform.position;
        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
        bosscol = GetComponent<BoxCollider2D>();
        healthBar = GameObject.FindGameObjectWithTag("Healthbar").GetComponent<Slider>();
    }
    private void Update() {
        //set boss healthbar
        healthBar.value = hitpoint;
        //boss engage
        if(hitpoint <= maxHitpoint/2 && !isEngage){
            StartCoroutine(Engage());
        }
    }
    protected virtual void FixedUpdate(){
        
        hitbox.OverlapCollider(filter,hits);
        for (int i = 0; i < hits.Length; i++)
        {
            if(hits[i] == null){
                continue;
            }
            if(hits[i].tag == "Player"){
                //no condition
            }
            hits[i] = null;
        }
    }
    protected override void ReceiveDamage(Damage dmg){
        //time.time = timerightnow
        if(Time.time - lastImmune > immuneTime){
            lastImmune = Time.time;
            hitpoint -= dmg.damagedeal;
            bossanim.SetTrigger("Hurt");
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
        healthBar.value = 0;
        Destroy(gameObject);
        GameObject DieObject = (GameObject) Instantiate(Resources.Load("BossDie"));
        DieObject.transform.position = transform.position;
        //gameObject.SetActive(false);
        transform.position = this.transform.position;
        GameManager.instance.XPGain(xpValue);
        GameManager.instance.Showtext("Gain "+ xpValue + "exp", 25,Color.magenta,transform.position,Vector3.up *40,1f);
    }
    IEnumerator Engage(){
        //drop weapon
        bossanim.SetTrigger("Engage");
        isEngage = true;
        bossAttack.enabled = false;//cant attack anymore
        bossSkill2.enabled = false;
        yield return new WaitForSeconds(2f);
        weapon.transform.parent = null;
        bossSkill1.enabled = true;//time to castskill1
        xSpeed *= 1.5f;
        ySpeed *= 1.5f;
    }
}
