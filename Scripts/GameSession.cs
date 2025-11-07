using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] int score = 0;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;

    void Awake() 
    {
        //Ne kadar varsa civarda buluyo:)
        int numGameSession = FindObjectsOfType<GameSession>().Length;
        if (numGameSession > 1){
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
        }
    }

    
    void Start()
    {
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();
    }

    public void ProcessPlayerDeath()
    {
        if (playerLives > 1){
            TakeLife();
        } else {
            ResetGameSession();
        }
    }

    public void AddToScore(int poinstToAdd)
    {
        score += poinstToAdd;
        scoreText.text = score.ToString();
    }

    void ResetGameSession() 
    {
        FindObjectOfType<ScenePersist>().ReserScenePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    void TakeLife()
    {
        //playerLives = playerLives - 1; Alttaki ile aynı
        playerLives--;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        livesText.text = playerLives.ToString();

    }


}