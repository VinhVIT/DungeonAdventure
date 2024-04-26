using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlip : MonoBehaviour
{   
    private GameObject target;
    private void Start() {
        target = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update() {
        Vector3 scale = transform.localScale;
        if(target.transform.position.x < transform.position.x){
            scale.x = Mathf.Abs(scale.x)* -1;
        }
        else {
            scale.x = Mathf.Abs(scale.x);
        }
        transform.localScale = scale;
    }
}
