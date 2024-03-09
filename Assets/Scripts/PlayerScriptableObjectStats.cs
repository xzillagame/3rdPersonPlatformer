using UnityEngine;
using UnityEngine.Events;



[CreateAssetMenu(fileName = "Player Stats", menuName = "ScriptableObjects/Player Stats")]
public class PlayerScriptableObjectStats : ScriptableObject
{
    [SerializeField] int maxHealth;
    [SerializeField] int currrentHealth;

    [HideInInspector]
    public UnityEvent<int, int> onHealthChanged;

    public UnityEvent onAllHealthLost;


    public int MaxHealth { get { return maxHealth; } }
    public int CurrentHealth
    {
        get { return currrentHealth; }

        set
        {
            currrentHealth = value;
            currrentHealth = Mathf.Clamp(currrentHealth, 0, maxHealth);

            if (currrentHealth == 0)
            {
                Debug.Log("Player Should be Dead");
                onAllHealthLost?.Invoke();
            }

        }

    }


    private void OnEnable()
    {
        ResetHealth();
    }


    public void DamagePlayer(int damage)
    {
        Debug.Log(CurrentHealth);
        CurrentHealth -= damage;
        onHealthChanged?.Invoke(currrentHealth, maxHealth);
    }

    public void ResetHealth()
    {
        currrentHealth = maxHealth;
    }



}
