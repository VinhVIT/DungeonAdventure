using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedButton : MonoBehaviour
{
    private Animator anim;
    public Animator lockedDoorAnim;
    public GameObject cam;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag =="Player"){
            StartCoroutine("ChangeCam");
        }
    }
    IEnumerator ChangeCam(){
        cam.SetActive(true);
        anim.SetTrigger("Press");
        lockedDoorAnim.SetBool("IsOpen",true);
        StartCoroutine(CameraShake.instance.Shake(2f,1));
        yield return new WaitForSeconds(2f);
        cam.SetActive(false);
    }
}
