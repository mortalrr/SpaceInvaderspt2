using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour
{
    //private Player player;
    private Enemy enemy;
    private Random random;
    //private Bunker[] bunkers;
    
    public int score { get; private set; }
    public int highScore = 0;
    
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    
    private void Awake()
    {
        //player = FindObjectOfType<Player>();
        enemy = FindObjectOfType<Enemy>();
        random = FindObjectOfType<Random>();
        //bunkers = FindObjectsOfType<Bunker>();
    }
    
    void Start()
    {
        enemy.killed += OnEnemyKilled;
        random.killed += OnRandomKilled;
        
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "HI-SCORE: \n" + highScore.ToString("D4");
    }
    
    void Update()
    {
        scoreText.text = "SCORE \n" + score.ToString("D4");
        
        
        if (score > highScore)
        {
            highScore = score;
            highScoreText.text = "HI-SCORE: \n" + highScore.ToString("D4"); 
            PlayerPrefs.SetInt("HighScore", highScore);
        }
    }
    
    private void SetScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString().PadLeft(4, '0');
    }
    
    private void OnEnemyKilled(Enemy enemy)
    {
        SetScore(score + enemy.score);
    }

    private void OnRandomKilled(Random random)
    {
        SetScore(score + random.score);
    }
    
}