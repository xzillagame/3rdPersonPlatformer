using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollectable : MonoBehaviour
{

    [SerializeField] GameRuleManager coinGameRuleManager;


    private void Start()
    {
        coinGameRuleManager.AddCoinToCount();
    }



    public void CoinCollected()
    {
        coinGameRuleManager.RemoveCoinFromCount();
        Destroy(gameObject);
    }


}
