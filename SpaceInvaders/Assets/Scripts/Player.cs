using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    public Bullet bulletPrefab;
    public GameObject explosionPrefab;
    public AudioClip explosionSound;
    public AudioClip fireSound;
    public System.Action killed;

    private bool bulletActive;
    private bool isAlive = true;
    private AudioSource audioSource;
    private Animator playerAnimator;
    
    //[FormerlySerializedAs("bullet")] public GameObject bulletPrefab;
    //[FormerlySerializedAs("shootingOffset")] public Transform shootOffsetTransform;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {

        if (!isAlive) return;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.position += Vector3.right * this.speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.position += Vector3.left * this.speed * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
            playerAnimator.SetTrigger("ShootTrigger");
        }
    }

    private void Fire()
    {
        if (!bulletActive)
        {
            Bullet bullet = Instantiate(this.bulletPrefab, this.transform.position, Quaternion.identity);
            bullet.destroyed += BulletDestroyed;
            bulletActive = true;
            Debug.Log("Bang!");
        }
    }

    private void BulletDestroyed(Bullet bullet)
    {
        bulletActive = false;
    }

    private void Die()
    {
        isAlive = false;
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        audioSource.PlayOneShot(explosionSound);
        Destroy(gameObject);
        SceneManager.LoadScene("Main Menu");
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Die();
        }
        
        if (other.gameObject.layer == LayerMask.NameToLayer("Missile") ||
            other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            //Die();
            if (killed != null) {
                killed.Invoke();
            }
        }
    }
    
}