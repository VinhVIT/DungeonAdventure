using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treeghost : Enemy
{
    public float stoppingDistance;
    public float retreatDistance;
    private float cooldown;
    public float lastShoot;
    public GameObject bullet;
    protected override void Start() {
        base.Start();
        xSpeed *= 0.5f;//set treeghost speed
        cooldown = lastShoot;
    }
    private void Update() {
        //nothing happen when player is so far
        if(Vector2.Distance(transform.position,playertransform.position) < stoppingDistance && Vector2.Distance(transform.position,playertransform.position) > retreatDistance){
            transform.position = this.transform.position;
        }
        //fall back when player go near
        else if(Vector2.Distance(transform.position,playertransform.position) < retreatDistance){
            transform.position = Vector2.MoveTowards(transform.position,playertransform.position,-xSpeed * Time.deltaTime);
        }

        
    }
    protected override void FixedUpdate()
    {   
        //check if player in range -> start shooting
        if(Vector3.Distance(playertransform.position,startingPositon) < chaseLengt){      
            if(Vector3.Distance(playertransform.position,startingPositon) < triggerLenght){
                chasing = true;
            }
            if(chasing){
                if(cooldown <=0){
                    Instantiate(bullet,transform.position,Quaternion.identity);
                    cooldown = lastShoot;
            }
                else {
                    cooldown -= Time.deltaTime;
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
    }
}
