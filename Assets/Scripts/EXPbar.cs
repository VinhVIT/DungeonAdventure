using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EXPbar : MonoBehaviour
{   
    public Text EXPText;
    public RectTransform EXPbarr;
    private void Update() {
        UpdateHealthbar();
    }
    public void UpdateHealthbar(){
        int currentLevel = GameManager.instance.GetLevel();
        if( currentLevel == GameManager.instance.XPTable.Count+1){//Max xp check
            EXPText.text = "Maxlevel";
            EXPbarr.localScale = Vector3.one;
        }
        else {
            int previousLevelXP = GameManager.instance.GetEXP(currentLevel-1);
            int currentLevelXP = GameManager.instance.GetEXP(currentLevel);
            int diff = currentLevelXP - previousLevelXP;
            int currentXP = GameManager.instance.EXP - previousLevelXP;
            float completionRatio = (float)currentXP / (float)diff;
            EXPbarr.localScale = new Vector3(completionRatio,1,1);
            EXPText.text = currentXP.ToString() + "/" + diff;
        }
    }
}
