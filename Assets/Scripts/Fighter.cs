using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    //public field
    public int hitpoint = 6;
    public int maxHitpoint = 6;
    public float pushRecoverySpeed = 0.2f;
    //immune
    protected float immuneTime = 0.5f;
    protected float lastImmune;
    protected Vector3 pushDirection;
    public PlayerControl player;
    protected virtual void ReceiveDamage(Damage dmg){
        //time.time = rightnow
        if(Time.time - lastImmune > immuneTime){
            lastImmune = Time.time;
            hitpoint -= dmg.damagedeal;
            GetComponent<Animator>().SetTrigger("Hurt");
            pushDirection = (transform.position - dmg.origin).normalized *dmg.pushForce;
            if(gameObject.tag == "Player"){
                AudioManager.Instance.PlaySFX("Hurt");
                //reduce current Healthbar
                player.Healthbar.sizeDelta = new Vector2(player.Healthbar.sizeDelta.x -6.5f *dmg.damagedeal,player.Healthbar.sizeDelta.y);
            }
            //Add damage text
            GameManager.instance.Showtext(dmg.damagedeal.ToString(),35,Color.red,transform.position,Vector3.up *25,0.5f);
            //die condition
            if(hitpoint <= 0){
            hitpoint = 0;
            Death();
            }
        }
    }
    protected virtual void Death(){
        Debug.Log("You're dead");
    }
}
