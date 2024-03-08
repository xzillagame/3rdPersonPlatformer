using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollectableComponent : MonoBehaviour
{


    public UnityEvent onCollected;



    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Reconngized");


        CollectablesCollector collector = other.gameObject.GetComponent<CollectablesCollector>();
        Debug.Log(collector);

        if (collector != null)
        {
            onCollected?.Invoke();
        }
    }


}
