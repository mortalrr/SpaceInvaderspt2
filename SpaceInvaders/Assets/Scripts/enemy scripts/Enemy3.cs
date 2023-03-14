using UnityEngine;

public class Enemy3 : MonoBehaviour
{
    public delegate void Enemy3Destroyed(float pointValue);

    public static event Enemy3Destroyed OnEnemy3AboutToBeDestroyed;

    public int enemy3Points = 10;
    
    
    //-----------------------------------------------------------------------------
    void OnCollisionEnter2D()
    {
        // todo - trigger death animation
        Debug.Log("Ouch!");

        OnEnemy3AboutToBeDestroyed.Invoke(enemy3Points);
        Destroy(this.gameObject);
    }
}