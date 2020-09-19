using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
         public static GameManager instance;
         public float respawnTime = 2f;
           public int currentScore;
               private int highScore;

    public int currentLives =3;
    private void Awake() {
        instance = this;
    }
      void Start()
    {
   
        UiManager.instance.livesText.text = "x " + currentLives;
           UiManager.instance.scoreText.text = "Score: " + currentScore;
                   highScore = PlayerPrefs.GetInt("HighScore");

              UiManager.instance.highScoreText.text = "Hi-Score: " + highScore;

    }
  public void KillPlayer()
  {
    currentLives --;
    UiManager.instance.livesText.text = "x " + currentLives;

    if(currentLives >0)
    {
      StartCoroutine(RespawnCo());
    }else
    {
      UiManager.instance.gameOverScreen.SetActive(true);
      WaveManager.instance.canSpwanWaves = false;
    }
  }
    public void AddScore(int scoreToAdd)
    {
        currentScore += scoreToAdd;
        
        UiManager.instance.scoreText.text = "Score: " + currentScore;
  if(currentScore > highScore)
        {
            highScore = currentScore;
            UiManager.instance.highScoreText.text = "Hi-Score: " + highScore;
                        PlayerPrefs.SetInt("HighScore", highScore);
        }
      
    }
  public IEnumerator RespawnCo(){
    
    yield return new WaitForSeconds(respawnTime);
    HealthManager.instance.Respawn();
    WaveManager.instance.ContinueSpawning();
  }
}
