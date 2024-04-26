using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPC : Collidable
{   
    [SerializeField] private TextWriter textWriter;
    public string message;
    private float cooldown =4;
    private float lastCool =4;
    public TextMeshPro txt;
    

    protected override void Start()
    {
        base.Start();
        lastCool = -lastCool;
        txt = transform.Find("Text").GetComponent<TextMeshPro>();
        
    }
    protected override void OnCollide(Collider2D col)
    {   
        if(col.tag == "Player"){
            if(Time.time - lastCool > cooldown){
                lastCool = Time.time;
                textWriter.AddWriter(txt,message,0.1f);
            } 
        }        
    }
    private void Setup(string text){
        txt.SetText(text);
        txt.ForceMeshUpdate();
        Vector2 textSide = txt.GetRenderedValues(false);
        Vector2 padding = new Vector2(2f,1.2f);
    }
    
}
