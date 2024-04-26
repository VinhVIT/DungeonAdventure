using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BubbleChat : MonoBehaviour
{
    private SpriteRenderer Bubblechat;
    private TextMeshPro txt;
    public string message;
    private void Awake() {
        Bubblechat = transform.Find("Background").GetComponent<SpriteRenderer>();
        txt = transform.Find("Text").GetComponent<TextMeshPro>();
    }
    private void Start() {
        Setup(message);
    }
    private void Setup(string text){
        txt.SetText(text);
        txt.ForceMeshUpdate();
        Vector2 textSide = txt.GetRenderedValues(false);
        Vector2 padding = new Vector2(2f,1.2f);
        Bubblechat.size =  textSide + padding;
        //Bubblechat.transform.localPosition = new Vector3(Bubblechat.size.x/2f,0f);
    }
}
