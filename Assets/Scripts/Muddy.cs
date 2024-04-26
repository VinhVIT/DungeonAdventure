using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muddy : MonoBehaviour
{
    private Animator anim;
    public Fighter fighter;
    public EnemyHitbox enemy;
    private void Start() {
        anim = GetComponent<Animator>();
    }
    private void Update() {
        if(fighter.hitpoint <= 5){
            anim.SetTrigger("Engage");
            enemy.damage = 2;
        }
    }
}
