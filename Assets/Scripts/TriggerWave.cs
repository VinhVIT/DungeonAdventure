using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerWave : MonoBehaviour
{
    public Animator lockedDoorAnim;
    public GameObject Wave;
    private void OnTriggerEnter2D(Collider2D trig) {
        if(trig.gameObject.tag == "Player"){
            StartCoroutine("StartWave");  
        }
    }
    IEnumerator StartWave(){
        lockedDoorAnim.SetBool("IsOpen",false);
        yield return new WaitForSeconds(2f);
        Wave.SetActive(true);
    }
}
