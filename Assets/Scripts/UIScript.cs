using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    // Start is called before the first frame update


    [Header("Game Rules Stat Refernce")]
    [SerializeField] GameRuleManager gameRuleManager;


    [Header("Player Stat Reference")]
    [SerializeField] PlayerScriptableObjectStats stats;


    [Header("UI References")]
    [SerializeField] TMP_Text healthText;
    [SerializeField] TMP_Text coinCountText;




    private void OnEnable()
    {
        stats.onHealthChanged.AddListener(UpdateHealth);
        gameRuleManager.onCoinCollected.AddListener(UpdateCoin);
    }

    private void OnDisable()
    {
        stats.onHealthChanged.RemoveListener(UpdateHealth);
        gameRuleManager.onCoinCollected.RemoveListener(UpdateCoin);
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.H))
        {
            stats.DamagePlayer(5);
        }

    }



    void UpdateHealth(int newCurrHealth, int newMaxHealth)
    {
        healthText.text = "Health: " + newCurrHealth + " / " + newMaxHealth ;
    }

    void UpdateCoin(int newCurrCoin, int newMaxCoin)
    {
       coinCountText.text = "Coins: " + newCurrCoin + " / " + newMaxCoin;
    }

}
