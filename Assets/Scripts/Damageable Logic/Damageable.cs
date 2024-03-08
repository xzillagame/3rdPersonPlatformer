using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{

    [SerializeField]
    private UnityEvent<int> onDamaged;


    public void TakeDamage(int damageToTake)
    {
        onDamaged?.Invoke(damageToTake);
    }



}
