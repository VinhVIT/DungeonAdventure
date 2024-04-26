using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public static Menu Instance;
    //Text
    public Text levelText, hpText, moneyText, upgradeCostText, EXPText, CharacterName;
    //sprite
    public int currentChar = 0;
    public Image CharSprite;
    public Image WepSprite;
    public RectTransform EXPbar;
    public Animator charMenu;
    public Button updateButton;
    public bool isShowing = false;
    public Slider musicSlider, sfxSlider;
    private void Awake()
    {
        Instance = this;
    }
    //Menu Showing
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UpdateMenu();
            Show();
        }
    }
    public void Show()
    {
        if (isShowing == false)
        {
            isShowing = true;
            charMenu.SetTrigger("Show");
            Time.timeScale = 0f;
        }
        else if (isShowing == true)
        {
            isShowing = false;
            charMenu.SetTrigger("Hide");
            Time.timeScale = 1f;
        }
    }
    //CharSelection
    public void OnArrowClick(bool right)
    {
        if (right)
        {
            currentChar++;
            if (currentChar == GameManager.instance.playersprites.Count)
            {
                currentChar = 0;
            }
            OnSelectionChanged();
        }
        else
        {
            currentChar--;
            if (currentChar < 0)
            {
                currentChar = GameManager.instance.playersprites.Count - 1;
            }
            OnSelectionChanged();
        }
    }
    private void OnSelectionChanged()
    {
        CharSprite.sprite = GameManager.instance.playersprites[currentChar];
        CharacterName.text = GameManager.instance.playersprites[currentChar].name;
        GameManager.instance.player.SwapChar(currentChar);
    }
    //WepUpgrade    
    public void OnUpgradeClick()
    {
        if (GameManager.instance.UpdateMethod())
        {
            UpdateMenu();
        }
    }
    //CharInfo
    public void UpdateMenu()
    {
        // Wepon
        WepSprite.sprite = GameManager.instance.Weaponsprites[GameManager.instance.weapon.weaponLevel];
        AudioManager.Instance.PlaySFX("Update");
        if (GameManager.instance.weapon.weaponLevel == GameManager.instance.WeaponPrice.Count - 1)
        {
            upgradeCostText.text = "MAX";
            updateButton.interactable = false;
        }
        else
        {
            upgradeCostText.text = GameManager.instance.WeaponPrice[GameManager.instance.weapon.weaponLevel].ToString();
        }

        if (GameManager.instance.Money >= GameManager.instance.WeaponPrice[GameManager.instance.weapon.weaponLevel])
        {
            upgradeCostText.color = Color.white;
        }
        if (GameManager.instance.Money < GameManager.instance.WeaponPrice[GameManager.instance.weapon.weaponLevel])
        {
            upgradeCostText.color = Color.red;
        }
        //CharInfo
        hpText.text = GameManager.instance.player.hitpoint.ToString();
        moneyText.text = GameManager.instance.Money.ToString();
        levelText.text = GameManager.instance.GetLevel().ToString();

        //EXPbar
        int currentLevel = GameManager.instance.GetLevel();
        if (currentLevel == GameManager.instance.XPTable.Count + 1)
        {//Max xp check
            EXPText.text = "Maxlevel";
            EXPbar.localScale = Vector3.one;
        }
        else
        {
            int previousLevelXP = GameManager.instance.GetEXP(currentLevel - 1);
            int currentLevelXP = GameManager.instance.GetEXP(currentLevel);
            int diff = currentLevelXP - previousLevelXP;
            int currentXP = GameManager.instance.EXP - previousLevelXP;
            float completionRatio = (float)currentXP / (float)diff;
            EXPbar.localScale = new Vector3(completionRatio, 1, 1);
            EXPText.text = currentXP.ToString() + "/" + diff;
        }
    }
    public void ToggleMusic()
    {
        AudioManager.Instance.ToggleMusic();
    }
    public void ToggleSFX()
    {
        AudioManager.Instance.ToggleSFX();
    }
    public void MusicVolume()
    {
        AudioManager.Instance.MusicVolume(musicSlider.value);
    }
    public void SFXVolume()
    {
        AudioManager.Instance.SFXVolume(sfxSlider.value);
    }
}
