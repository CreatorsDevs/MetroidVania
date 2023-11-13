using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    [SerializeField] private float timeToExplode = 0.5f;
    [SerializeField] private GameObject explosion;

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
        }
        
    }
}
