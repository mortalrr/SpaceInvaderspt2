using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //public delegate void EnemyDestroyed(float pointValue);
   
    //public static event EnemyDestroyed OnEnemyAboutToBeDestroyed;

    public int score = 10;
    public System.Action<Enemy> killed;
    private Animator playerAnimator;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Bullet")) {
            Destroy(other.gameObject);
            //playerAnimator.SetTrigger("DieTrigger");
            killed?.Invoke(this);
        }
    }
    
}
