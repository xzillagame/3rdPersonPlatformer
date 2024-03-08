using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;




[CreateAssetMenu(fileName = "LevelCoins", menuName = "ScriptableObjects/GameManager")]

public class GameRuleManager : ScriptableObject
{
    private int maxCoins = 0;
    private int currentCoins = 0;

    public int MaxCoins { get { return maxCoins; } }
    public int CurrentCoins { get { return currentCoins; } }


    public UnityEvent<int,int> onCoinCollected;

    private void OnEnable()
    {
        maxCoins = 0;
        currentCoins = 0;
    }


    public void AddCoinToCount()
    {
        maxCoins++;
    }

    public void RemoveCoinFromCount()
    {
        currentCoins++;

        onCoinCollected?.Invoke(currentCoins, maxCoins);

        if (currentCoins == maxCoins)
        {
            Debug.Log("win");
        }
    }



}
