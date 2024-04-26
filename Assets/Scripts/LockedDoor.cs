using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    private Animator anim;
    public bool isOpen;
    void Start()
    {
        anim = GetComponent<Animator>();
        if(isOpen){
            anim.SetBool("IsOpen",true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
