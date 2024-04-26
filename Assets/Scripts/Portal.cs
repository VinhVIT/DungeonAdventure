using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : Collidable
{
    //public string[] sceneNames;//no need
    public string sceneName;
    public LevelLoader levelLoader;
    protected override void OnCollide(Collider2D col)
    {
        if(col.tag == "Player"){
            GameManager.instance.Save();
            //string sceneName = sceneNames[Random.Range(0,sceneNames.Length)];
            levelLoader.LoadSceneLevel(sceneName);
        }
    }
}
