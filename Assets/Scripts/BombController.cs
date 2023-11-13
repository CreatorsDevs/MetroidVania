using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    [SerializeField] private float timeToExplode = 0.5f;
    [SerializeField] private GameObject explosion;
    [SerializeField] private float blastRange;

    public LayerMask whatIsDestructible;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeToExplode -= Time.deltaTime;
        if (timeToExplode <= 0 )
        {
            if( explosion != null )
            {
                Instantiate(explosion, transform.position, transform.rotation);
            }

            Destroy( gameObject );

            Collider2D[] objectsToRemove = Physics2D.OverlapCircleAll(transform.position, blastRange, whatIsDestructible);

            if(objectsToRemove.Length > 0)
            {
                foreach(Collider2D col in objectsToRemove)
                {
                    Destroy(col.gameObject);
                }
            }
        }
        
    }
}
