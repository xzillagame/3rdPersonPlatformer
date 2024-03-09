using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "LevelCoins", menuName = "ScriptableObjects/GameManager")]

public class GameRuleManager : ScriptableObject
{

    [SerializeField] private SceneLoader SceneLoader;


    private int maxCoins = 0;
    private int currentCoins = 0;

    public int MaxCoins { get { return maxCoins; } }
    public int CurrentCoins { get { return currentCoins; } }


    [HideInInspector]
    public UnityEvent<int,int> onCoinCollected;

    private void OnEnable()
    {
        ResetCoins();
    }


    public void ResetCoins()
    {
        maxCoins = 0;
        currentCoins = 0;
    }


    public void AddCoinToCount()
    {
        maxCoins++;
    }

    public void CoinCollected()
    {
        currentCoins++;

        onCoinCollected?.Invoke(currentCoins, maxCoins);

        if ( CheckWinCondition() )
        {
            Debug.Log("win");
            SceneLoader.LoadWinScene();
        }
    }


    private bool CheckWinCondition()
    {
        return currentCoins == maxCoins;
    }



}
