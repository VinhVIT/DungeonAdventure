using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{   
    public GameObject menuButton;
    public GameObject PressButton;
    public void StartMenu(){
        SceneManager.LoadScene("Base");
    }
    public void Tutorial(){
        SceneManager.LoadScene("Tutorial");
    }
    private void Update() {
        Press();
    }
    public void Press(){
        if(Input.anyKey){
            menuButton.SetActive(true);
            PressButton.SetActive(false);
        }
    }
    public void Quit(){
        Application.Quit();
    }
}
