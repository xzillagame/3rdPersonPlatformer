using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollectable : MonoBehaviour
{

    [SerializeField] GameRuleManager coinGameRuleManager;


    private void Awake()
    {
        coinGameRuleManager.AddCoinToCount();
    }



    public void CoinCollected()
    {
        coinGameRuleManager.CoinCollected();
        Destroy(gameObject);
    }


}
