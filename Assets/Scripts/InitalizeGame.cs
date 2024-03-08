using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitalizeGame : MonoBehaviour
{

    [SerializeField] private PlayerScriptableObjectStats stats;
    [SerializeField] private GameRuleManager ruleManager;



    void Start()
    {

        stats.onHealthChanged?.Invoke(stats.CurrentHealth,stats.MaxHealth);
        ruleManager.onCoinCollected?.Invoke(ruleManager.CurrentCoins, ruleManager.MaxCoins);


        Destroy(gameObject);

    }

}
