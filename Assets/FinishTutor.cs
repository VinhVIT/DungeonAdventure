using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishTutor : MonoBehaviour
{
    public Animator tutorialAnim;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag =="Player"){
            tutorialAnim.SetTrigger("Show");
        }
    }
}
