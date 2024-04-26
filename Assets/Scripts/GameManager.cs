using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        if (GameManager.instance != null)
        {//if exist Object destroy duplicate Object
            Destroy(gameObject);
            Destroy(player.gameObject);
            Destroy(floatingTextManager.gameObject);
            Destroy(HUD);
            Destroy(menu);
            return;
        }
        instance = this;
        SceneManager.sceneLoaded += Load;
        SceneManager.sceneLoaded += OneScenceLoad;
        currentScene = SceneManager.GetActiveScene();
    }
    //Resources
    public List<Sprite> playersprites;
    public List<RuntimeAnimatorController> playerAnims;
    public List<Sprite> Weaponsprites;
    public List<int> WeaponPrice;
    public List<int> XPTable;
    //References
    public PlayerControl player;
    public Weapon weapon;
    public FloatingTextManager floatingTextManager;
    public GameObject HUD;
    public GameObject menu;
    public Animator DeathMenu;
    public Animator TutorialMenu;
    public Transform DmgTextPrefab;
    private Scene currentScene;
    //Logic
    public int Money;
    public int EXP;
    //Floating text in any Obj
    public void Showtext(string msg/*message*/, int fontsize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontsize, color, position, motion, duration);
    }
    //Update Weapon
    public bool UpdateMethod()
    {
        if (WeaponPrice.Count <= weapon.weaponLevel)
        {
            return false;
        }
        if (Money >= WeaponPrice[weapon.weaponLevel])
        {
            Money -= WeaponPrice[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }
        return false;
    }
    //EXP system
    public int GetLevel()
    {
        int r = 0;
        int maxXP = 0;
        while (EXP >= maxXP)
        {
            maxXP += XPTable[r];
            r++;
            if (r == XPTable.Count)//Max level
                return r;
        }
        return r;
    }
    public int GetEXP(int level)
    {
        int r = 0;
        int XP = 0;
        while (r < level)
        {
            XP += XPTable[r];
            r++;
        }
        return XP;
    }
    public void XPGain(int xp)
    {
        int curLevel = GetLevel();
        EXP += xp;
        if (curLevel < GetLevel())
        {
            LevelUp();
        }
        if (EXP > 3900)
            EXP = 3900;
    }
    public void LevelUp()
    {
        Showtext("Level Up", 60, Color.gray, transform.position, Vector3.up * 100f, 2);
        AudioManager.Instance.PlaySFX("LevelUp");
        AudioManager.Instance.PlayMusic("Theme");
        player.OnLevelUp();
    }
    //OnSceneLoad
    public void OneScenceLoad(Scene s, LoadSceneMode mode)
    {
        currentScene = SceneManager.GetActiveScene();
        if (currentScene.name != "MainMenu")
        {
            player.transform.position = GameObject.Find("SavePoint").transform.position;
        }
    }
    //death
    public void Respawn()
    {
        if (currentScene.name == "Tutorial")
        {
            SceneManager.LoadScene("Tutorial");
            AudioManager.Instance.PlayMusic("Theme");
            DeathMenu.SetTrigger("Hide");
            player.Respawn();
        }
        else
        {
            SceneManager.LoadScene("Base");
            AudioManager.Instance.PlayMusic("Theme");
            DeathMenu.SetTrigger("Hide");
            player.Respawn();
        }

    }
    public void StartGame()
    {   
        SceneManager.LoadScene("Base");
        AudioManager.Instance.PlayMusic("Theme");
        TutorialMenu.SetTrigger("Hide");
        player.Respawn();
    }
    public void ReturnMainMenu()
    {   
        SceneManager.LoadScene("MainMenu");
        Menu.Instance.Show();
        Time.timeScale = 1f;
    }
    //Save and Load function
    public void Save()
    {
        string s = "";
        s += "0" + "|";
        s += Money.ToString() + "|";
        s += EXP.ToString() + "|";//need tostring to avoid weird bug
        s += weapon.weaponLevel.ToString();
        PlayerPrefs.SetString("Save", s);
    }
    public void Load(Scene s, LoadSceneMode mode)
    {
        Debug.Log("Loading");
        SceneManager.sceneLoaded -= Load;
        if (!PlayerPrefs.HasKey("Save"))
        {
            return;
        }
        string[] data = PlayerPrefs.GetString("Save").Split('|');// put info into string[]
        Money = int.Parse(data[1]);//point on array string[1]
        EXP = int.Parse(data[2]);//exp on array string[2]
        if (GetLevel() != 1)
            player.SetLevel(GetLevel());
        weapon.SetWeapLevel(int.Parse(data[3]));//save weapon
        player.transform.position = GameObject.Find("SavePoint").transform.position;//savepoint
    }
}
