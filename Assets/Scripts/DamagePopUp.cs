using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopUp : MonoBehaviour
{   
    //Create DmgText
    public static DamagePopUp Create(Vector3 pos,int dmg){
        Transform dmgPopupTrans = Instantiate(GameManager.instance.DmgTextPrefab,pos,Quaternion.identity);
        DamagePopUp damagePopUp = dmgPopupTrans.GetComponent<DamagePopUp>();
        damagePopUp.Setup(dmg);
        return damagePopUp;
    }
    private static int sortingOrder;
    private const float DISAPPEAR_TIMER_MAX = 1f;
    private TextMeshPro textMesh;
    private float duration;
    private Color textColor;
    private Vector3 moveVector;
    private void Awake(){
        textMesh = GetComponent<TextMeshPro>();
    }
    public void Setup(int dmg){
        textMesh.SetText(dmg.ToString());
        textColor = textMesh.color;
        duration = DISAPPEAR_TIMER_MAX;
        sortingOrder++;
        textMesh.sortingOrder = sortingOrder;
        moveVector = new Vector3(0.2f,1) * 2f;
    }
    private void Update(){
        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * 8f * Time.deltaTime;
        //animation text 
        if(duration > DISAPPEAR_TIMER_MAX *.5f){
            float scaleIncreaseAmount = 0.8f;
            transform.localScale += Vector3.one * scaleIncreaseAmount *Time.deltaTime;
        }
        else{
            float scaleDecreaseAmount = 1f;
            transform.localScale -= Vector3.one * scaleDecreaseAmount *Time.deltaTime;
        }
        //make text disappear
        duration -= Time.deltaTime;
        float disappearSpeed = 2f;
        if(duration < 0){
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if(textColor.a < 0)
                Destroy(gameObject);
        }
    }
}
