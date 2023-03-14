using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Random : MonoBehaviour
{
    [FormerlySerializedAs("Random")] public GameObject randomPrefab;
    public delegate void RandomDestroyed(float pointValue);

    public static event RandomDestroyed OnRandomAboutToBeDestroyed;

    public int score = 300;
    public float moveSpeed = 1f;
    public float moveDistance = 5f;
    public System.Action<Random> killed;

    private float startingX;
    //-----------------------------------------------------------------------------
    void OnCollisionEnter2D()
    {
        Debug.Log("Ouch!");

        OnRandomAboutToBeDestroyed.Invoke(score);
        Destroy(this.gameObject);
    }
    
    private void Start()
    {
        startingX = transform.position.x;
    }

    private void Update()
    {
        float x = startingX + Mathf.Sin(Time.time * moveSpeed) * moveDistance;
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Bullet")) {
            Destroy(other.gameObject);
            killed?.Invoke(this);
        }
    }
    
    
}