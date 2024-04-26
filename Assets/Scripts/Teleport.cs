using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    public string sceneName;
    private Animator anim;
    private bool standing = false;
    private void Start() {
        anim = GetComponent<Animator>();
    }
    private void Update() {
        if(standing)
            Tele();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){    
            anim.SetBool("TurnOn",true);
            standing = true;
        }   
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Player"){
            anim.SetBool("TurnOn",false);
        }
    }
    private void Tele(){
        if(Input.GetKeyDown(KeyCode.E)){
                GameManager.instance.Save();
                SceneManager.LoadScene(sceneName);
            }
    }
}
