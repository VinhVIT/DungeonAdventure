using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collidable : MonoBehaviour
{
    public ContactFilter2D filter;
    private BoxCollider2D boxcollider;
    private Collider2D[] hit = new Collider2D[10];
    protected virtual void Start() {
        boxcollider = GetComponent<BoxCollider2D>();
    }
    protected virtual void Update() {
        boxcollider.OverlapCollider(filter,hit);
        for (int i = 0; i < hit.Length; i++)
        {
            if(hit[i] == null){
                continue;
            }
            OnCollide(hit[i]);
            hit[i] = null;
        }
    }
    protected virtual void OnCollide(Collider2D col){
        Debug.Log(col.name);
    }
}

