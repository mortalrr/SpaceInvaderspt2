using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public delegate void Enemy2Destroyed(float pointValue);

    public static event Enemy2Destroyed OnEnemy2AboutToBeDestroyed;

    public int enemy2Points = 20;
    
    
    //-----------------------------------------------------------------------------
    void OnCollisionEnter2D()
    {
        // todo - trigger death animation
        Debug.Log("Ouch!");

        OnEnemy2AboutToBeDestroyed.Invoke(enemy2Points);
        Destroy(this.gameObject);
    }
}