using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthbar : MonoBehaviour
{   
    public Transform parent;
    public Transform healthbar;
    public SpriteRenderer barSprite;
    private float ratio;
    public void OnHitPointChange(int hitpoint,int maxHitpoint){
        ratio = (float)hitpoint/(float)maxHitpoint;
        healthbar.localScale = new Vector3(ratio,1,1);
    }
    private void Update() {
        //fix the bug healthbar flip follow enemy
        if(parent.localScale.x == -1)
            transform.localScale = new Vector3(-1,1,1);
        if(parent.localScale.x == 1)
            transform.localScale = new Vector3(1,1,1);    
        //change healthbar color depend on hp left    
        if(healthbar.localScale.x <= 0.6 && healthbar.localScale.x >= 0.3)
            barSprite.color = Color.yellow;
        if(healthbar.localScale.x <= 0.3)
            barSprite.color = Color.red;
    }
}
