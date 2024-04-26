using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chess : Collectable
{   
    
    Vector2 Position;
    public int moneyAmount = 5;
    protected override void OnCollect()
    {
        if(!collected){
            collected = true;
            AudioManager.Instance.PlaySFX("Chess");
            GetComponent<Animator>().SetBool("IsOpen",true);
            GameManager.instance.Money += moneyAmount;
            //Call the showtext:message,color,position,position to display(motion),duration
            GameManager.instance.Showtext("+" + moneyAmount + "$",25,Color.yellow,transform.position,Vector3.up * 25,0.7f);
        }
    }
    protected override void Update()
    {   
        base.Update();
        Position = new Vector2(transform.localPosition.x,transform.localPosition.y);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            GetComponent<Animator>().SetBool("IsOpen",true);
            StartCoroutine("Emtychess");
            
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        GetComponent<Animator>().SetBool("IsOpen",false);
    }
    IEnumerator Emtychess(){
        yield return  new WaitForSeconds(0.4f);
        Destroy(gameObject);
        GameObject DieObject = (GameObject) Instantiate(Resources.Load("EmtyChess_0"));
        DieObject.transform.localPosition = Position;
        
    }
}
