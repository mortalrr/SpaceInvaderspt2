using UnityEngine;
using System;


public class EnemyContainer : MonoBehaviour
{
   public System.Action<Enemy> killed;
   public Enemy[] prefabs;
   public int totalKilled {get; private set;}
   public int totalEnemy => rows * columns;
   public float percentKilled => (float)totalKilled/(float)totalEnemy;
   public int AmountAlive => totalEnemy - totalKilled;
   
   public int columns = 11;
   public int rows = 5;
   public AnimationCurve speed;
   
   public float missileAttackRate = 1.0f;
   public Bullet missilePrefab;
   
   private Vector3 direction = Vector2.right;


   private void Awake()
   {
      for (int row = 0; row < this.rows; row++)
      {
         float width = 2.0f * (this.columns - 1);
         float height = 2.0f * (this.rows - 1);
         Vector2 center = new Vector2(-width / 2, -height / 2);
         Vector3 rowPosition = new Vector3(center.x, center.y + (row * 2.0f), 0.0f);

         for (int col = 0; col < this.columns; col++)
         {
            Enemy enemy = Instantiate(prefabs[row], transform);
            enemy.killed += OnEnemyKilled;
            
            Vector3 position = rowPosition;
            position.x += col * 2.0f;
            enemy.transform.localPosition = position;
         }
      }
   }


   private void Start()
   {
      InvokeRepeating(nameof(MissileAttack),missileAttackRate, missileAttackRate);
   }

   private void Update()
   {
      this.transform.position += direction * this.speed.Evaluate(this.percentKilled) * Time.deltaTime;

      Vector3 leftBorder = Camera.main.ViewportToWorldPoint(Vector3.zero);
      Vector3 rightBorder = Camera.main.ViewportToWorldPoint(Vector3.right);

      foreach (Transform invader in this.transform)
      {
         if (!invader.gameObject.activeInHierarchy)
         {
            continue;
         }

         if (direction == Vector3.right && invader.position.x >= rightBorder.x - 3.0f)
         {
            AdvanceRow();
         }
         else if (direction == Vector3.left && invader.position.x <= leftBorder.x + 3.0f)
         {
            AdvanceRow();
         }
      }
   }

   private void AdvanceRow()
   {
      direction.x *= -1.0f;
      Vector3 position = this.transform.position;
      position.y -= 1.0f;
      this.transform.position = position;
   }
   
   private void OnEnemyKilled(Enemy enemy)
   {
      enemy.gameObject.SetActive(false);
      totalKilled++;
      killed(enemy);
   }

   private void MissileAttack()
   {
      int amountAlive = AmountAlive;
      if (amountAlive == 0) {
         return;
      }
      foreach (Transform invader in transform)
      {
         if (!invader.gameObject.activeInHierarchy)
         {
            continue;
         }
         if (UnityEngine.Random.value < (1f / (float)amountAlive))
         {
            Instantiate(missilePrefab, invader.position, Quaternion.identity);
            break;
         }
      }
   }
   
   
}




