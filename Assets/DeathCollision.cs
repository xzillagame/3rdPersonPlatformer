using UnityEngine;

public class DeathCollision : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {

        Damageable damageable = other.GetComponent<Damageable>();

        if(damageable != null)
        {
            damageable.TakeDamage(999);
        }


    }



}
