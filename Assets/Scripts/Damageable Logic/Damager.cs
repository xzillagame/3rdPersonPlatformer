using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Events;
using UnityEngine.Events;

public class Damager : MonoBehaviour
{
    [SerializeField]
    private int DamageToDeal = 0;

    [SerializeField]
    private UnityEvent onDamaging;

    private void OnTriggerEnter(Collider other)
    {
        Damageable damageable = other.GetComponent<Damageable>();


        if(damageable != null)
        {
            damageable.TakeDamage(DamageToDeal);
            onDamaging?.Invoke();
        }

    }


    public void DestorySelf()
    {
        Destroy(gameObject);
    }





}
