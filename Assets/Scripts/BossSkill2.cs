using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkill2 : MonoBehaviour
{
    public GameObject skill2;
    [SerializeField]private float  cooldown = 5f;
    private float lastCooldown;
    // Update is called once per frame
    void Update()
    {
        if(Time.time - lastCooldown > cooldown){
            lastCooldown = Time.time;
            Skill2();
        }
    }
    private void Skill2(){
       GameObject skill2Obj = Instantiate(skill2,transform.position,Quaternion.identity);
       Destroy(skill2Obj,3f);
    }
}
