using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    private Animator bossHealth;
    public Animator anim;
    public BoxCollider2D boxCol;
    public BossAttack bossATK;
    public BossSkill2 bossSkill2;
    private void Start() {
        bossHealth = GameObject.Find("BossHealth").GetComponent<Animator>();
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag =="Player"){
            bossHealth.SetBool("Active",true);
            anim.SetBool("IsOpened",false);
            boxCol.isTrigger = false;
            bossATK.enabled = true;
            bossSkill2.enabled = true;
        }
    }
}
