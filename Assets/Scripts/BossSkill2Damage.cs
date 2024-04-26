using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkill2Damage : MonoBehaviour
{
    public int damage;
    public float pushForce;
    private void OnParticleCollision(GameObject other) {
        if(other.tag == "Player"){
            Damage dmg = new Damage{
                damagedeal = damage,
                origin = transform.position,
                pushForce = pushForce
            };
            other.SendMessage("ReceiveDamage",dmg);
        }
    }
}
