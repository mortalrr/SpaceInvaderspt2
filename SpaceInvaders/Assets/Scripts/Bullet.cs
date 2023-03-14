using System;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Bullet : MonoBehaviour
{
    public System.Action<Bullet> destroyed;
    public float speed = 5;
    public Vector3 direction;
    public AudioClip fireSound;
    
    private AudioSource audioSource;
    
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        

        if (fireSound != null)
        {
            audioSource.PlayOneShot(fireSound);
        }
    }
    
    private void Update()
    {
        this.transform.position += this.direction * this.speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (destroyed != null) {
            destroyed.Invoke(this);
        }
            // if (other.gameObject.CompareTag("Enemy"))
        //{
        //    Destroy(other.gameObject);
         //   Destroy(gameObject);
          //  destroyed?.Invoke();
       // }
    }
}
