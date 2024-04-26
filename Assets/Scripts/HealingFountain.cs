using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingFountain : Collidable
{   public int healingAmount =1;
    protected override void OnCollide(Collider2D col){
        if(col.tag == "Player"){
            AudioManager.Instance.PlaySFX("Healing");
            GameManager.instance.player.Heal(healingAmount);
            this.enabled = false;
        }
    }
}
