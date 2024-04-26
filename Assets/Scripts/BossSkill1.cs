using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkill1 : MonoBehaviour
{   [SerializeField]private GameObject target;
    private float speed = 0.5f;
    [SerializeField]private Animator anim;
    private float  cooldown = 5f;
    private float lastCooldown;
    [SerializeField]private Rigidbody2D rb;
    public EnemyFlip flip;
    private void Start() {
        target = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void Update() {
        if(Time.time - lastCooldown > cooldown){
            lastCooldown = Time.time;
            Skill1();
        }
    }
    public void Skill1(){
        flip.enabled = false;
        anim.SetBool("IsRunning",true);
        speed = 2f;
        Vector2 moveDir = (target.transform.position - transform.position).normalized * speed;
        rb.velocity = new Vector2(moveDir.x,moveDir.y);
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Blocking"){
            flip.enabled = true;
            anim.SetBool("IsRunning",false);
            speed = 0.5f;
            transform.position = this.transform.position;
        }
        if(other.gameObject.tag == "Player"){
            flip.enabled = true;
        }
    }
}
