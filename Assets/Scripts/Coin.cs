using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Collectable
{   
    private int moneyAmount = 2;
    protected override void OnCollect()
    {
        AudioManager.Instance.PlaySFX("CollectCoin");
        GameManager.instance.Money += moneyAmount;
        GameManager.instance.Showtext("+" + moneyAmount + "$",25,Color.yellow,transform.position,Vector3.up * 25,0.5f);
        Destroy(gameObject);
    }
}
