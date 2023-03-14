using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunker : MonoBehaviour
{
    public int health = 4;
    public GameObject destroyedBarricadePrefab;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Destroyable"))
        {
            health--;
            Destroy(collision.gameObject);
            if (health <= 0)
            {
                Destroy(gameObject);
                Instantiate(destroyedBarricadePrefab, transform.position, transform.rotation);
            }
        }
    }
    
    
    
}