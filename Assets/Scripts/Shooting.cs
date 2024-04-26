using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject fireballPrefbab;
    public Transform playerTrans;
    private float fireballSpeed = 1.5f;
    private float cooldown = 0.5f;
    private float lastSwing;
    private void Update()
    {   
        SetDirection();
        if(Input.GetMouseButtonDown (0) || Input.GetKeyDown(KeyCode.Space)){
            if(Time.time - lastSwing > cooldown){
                lastSwing = Time.time;
                Shoot();
            }
        }    
    }
    private void Shoot(){
        GameObject fireball = Instantiate(fireballPrefbab,firePoint.position,firePoint.rotation);
        Rigidbody2D fireballRig = fireball.GetComponent<Rigidbody2D>();
        fireballRig.AddForce(new Vector2(1,0) * fireballSpeed,ForceMode2D.Impulse);
        Destroy(fireball,0.5f);
    }
    private void SetDirection(){
        if(playerTrans.localScale.x == -1){
            fireballSpeed = -1.5f;
            fireballPrefbab.transform.localScale = new Vector3(-0.5f,0.5f,1);
        }
        else{
            fireballSpeed = 1.5f;
            fireballPrefbab.transform.localScale = new Vector3(0.5f,0.5f,1);
        }
    }
}
