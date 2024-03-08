using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.Events;



[CreateAssetMenu(fileName = "Player Stats", menuName = "ScriptableObjects/Player Stats")]
public class PlayerScriptableObjectStats : ScriptableObject
{
    [SerializeField] int maxHealth;
    [SerializeField] int currrentHealth;

    public int CurrentHealth
    {
        get { return currrentHealth; }

        set
        {
            currrentHealth = value;
            currrentHealth = Mathf.Clamp(currrentHealth, 0, maxHealth);

            if(currrentHealth == 0)
            {
                Debug.Log("Player Should be Dead");
            }

        }

    }


    [SerializeField] int currentCoins;

    [HideInInspector]
    public UnityEvent<int,int> onHealthChanged;


    private void OnEnable()
    {
        currrentHealth = maxHealth;
    }



    public void DamagePlayer(int damage)
    {
        Debug.Log(CurrentHealth);
        CurrentHealth -= damage;
        onHealthChanged?.Invoke(currrentHealth, maxHealth);
    }

    public void HealPlayer(int healing)
    {
        CurrentHealth += healing;
        onHealthChanged?.Invoke(currrentHealth, maxHealth);
    }

    



}
