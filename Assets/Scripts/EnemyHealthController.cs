using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    public int totalhealth = 3;

    public GameObject deathEffect;

    public void DamageEnemy(int damageAmount)
    {
        totalhealth -= damageAmount;

        if(totalhealth < 0)
        {
            if(deathEffect != null)
            {
                Instantiate(deathEffect, transform.position, transform.rotation);
            }

            Destroy(gameObject);

        }
    }
}
