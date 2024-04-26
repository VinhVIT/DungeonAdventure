using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator anim;
    public void LoadSceneLevel(string levelName){
        StartCoroutine(LoadScene(levelName));
    }
    IEnumerator LoadScene(string sceneName){
        anim.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneName);  
    }
}
