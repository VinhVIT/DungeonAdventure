using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextWriter : MonoBehaviour
{
    private TextMeshPro txt;
	private string textToWrite;
	private int characterIndex;
	private float timePerCharacter;
	private float timer;
	public NPC NPCText;
	public GameObject bbchat;
	public void AddWriter(TextMeshPro txt,string textToWrite,float timePerCharacter){
		this.txt = txt;
		this.textToWrite = textToWrite;
		this.timePerCharacter = timePerCharacter;
		characterIndex = 0;
	}
	private void Update() {
		if(txt != null){
			timer -= Time.deltaTime;
			if(timer <= 0f){
				bbchat.SetActive(true);
				timer += timePerCharacter;
				characterIndex ++;
				txt.text = textToWrite.Substring(0,characterIndex);
			}
			if(characterIndex >=textToWrite.Length){
				txt = null;
				StartCoroutine("HideText");
				return;
			}
		}
	}
	IEnumerator HideText(){
        yield return new WaitForSeconds(4);
        NPCText.txt.SetText("");
		bbchat.SetActive(false);
    }
}
