using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowEffect : MonoBehaviour
{
    private float timebtwSpawn;
    private SpriteRenderer shadowspriteRenderer;
    public float startTimebtwSpawn;
    public GameObject shadowPrefab;
    public Transform player;
    public Menu menu;
    private void Start() {
        player = GetComponent<Transform>();
        shadowspriteRenderer = shadowPrefab.GetComponent<SpriteRenderer>();
    }
    private void Update() {
        SetShadowDirection();
        SetSprite();
        if(timebtwSpawn <= 0){
            GameObject shadowEffect = (GameObject)Instantiate(shadowPrefab,transform.position,Quaternion.identity);
            timebtwSpawn = startTimebtwSpawn;
            Destroy(shadowEffect,1f);
        }
        else {
            timebtwSpawn -= Time.deltaTime;
        }
    }
    public void SetShadowDirection(){
        if(player.localScale.x == -1){
            shadowPrefab.transform.localScale = new Vector3(-1,1,1);
        }
        else if(player.localScale.x == 1){
            shadowPrefab.transform.localScale = new Vector3(1,1,1);
        }
    }
    public void SetSprite(){
        shadowspriteRenderer.sprite = GameManager.instance.playersprites[menu.currentChar];
    }
}
