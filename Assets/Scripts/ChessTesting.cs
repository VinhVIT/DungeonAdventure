using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessTesting : MonoBehaviour
{
    Animator anim;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            GetComponent<Animator>().SetBool("IsOpen",true);
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        GetComponent<Animator>().SetBool("IsOpen",false);
        
    }
}
