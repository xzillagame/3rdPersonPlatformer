using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class DamagedStateEnd : MonoBehaviour
{

    [SerializeField] private UnityEvent onDamageAnimEnd;



    void DamageAnimEnded()
    {
        onDamageAnimEnd?.Invoke();
    }


}
