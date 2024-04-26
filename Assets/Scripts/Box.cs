using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : Collidable
{
    protected override void OnCollide(Collider2D col)
    {
        if(col.tag == "Weapon")
            Destroy(gameObject);
            
    }
}
