using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int currentLives = 3;

    public float respawnTime = 2f;

    public int currentScore;
    private int highScore = 500;

    public bool levelEnding;

    private int levelScore;

    public float waitForLevelEnd = 5f;

    public string nextLevel;

    private bool canPause;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentLives = PlayerPrefs.GetInt("CurrentLives");
        UiManager.instance.livesText.text = "x " + currentLives;

        

        highScore = PlayerPrefs.GetInt("HighScore");
        UiManager.instance.highScoreText.text = "Hi-Score: " + highScore;

        currentScore = PlayerPrefs.GetInt("CurrentScore");
        UiManager.instance.scoreText.text = "Score: " + currentScore;

        canPause = true;
    }

    void Update()
    {
        if(levelEnding)
        {
            SPlayerController.instance.transform.position += new Vector3(SPlayerController.instance.boostSpeed * Time.deltaTime, 0f, 0f);
        }

        if(Input.GetKeyDown(KeyCode.Escape) && canPause)
        {
            PauseUnpause();
        }
    }

    public void KillPlayer()
    {
        currentLives--;
        UiManager.instance.livesText.text = "x " + currentLives;

        if (currentLives > 0)
        {
            //respawn code
            StartCoroutine(RespawnCo());

        } else
        {
            //game over code
            UiManager.instance.gameOverScreen.SetActive(true);
            WaveManager.instance.canSpwanWaves = false;

            MusicController.instance.GameOverMusic();
            PlayerPrefs.SetInt("HighScore", highScore);

            canPause = false;
        }
    }

    public IEnumerator RespawnCo()
    {

        yield return new WaitForSeconds(respawnTime);
        HealthManager.instance.Respawn();

        WaveManager.instance.ContinueSpawning();
    }

    public void AddScore(int scoreToAdd)
    {
        currentScore += scoreToAdd;
        levelScore += scoreToAdd;
        UiManager.instance.scoreText.text = "Score: " + currentScore;

        if(currentScore > highScore)
        {
            highScore = currentScore;
            UiManager.instance.highScoreText.text = "Hi-Score: " + highScore;
            //PlayerPrefs.SetInt("HighScore", highScore);
        }
    }

    public IEnumerator EndLevelCo()
    {
        UiManager.instance.levelEndScreen.SetActive(true);
        SPlayerController.instance.stopMovement = true;
        levelEnding = true;
        MusicController.instance.VictoryMusic();

        canPause = false;

        yield return new WaitForSeconds(.5f);

        UiManager.instance.endLevelScore.text = "Level Score: " + levelScore;
        UiManager.instance.endLevelScore.gameObject.SetActive(true);

        yield return new WaitForSeconds(.5f);

        PlayerPrefs.SetInt("CurrentScore", currentScore);
        UiManager.instance.endCurrentScore.text = "Total Score: " + currentScore;
        UiManager.instance.endCurrentScore.gameObject.SetActive(true);

        if(currentScore == highScore)
        {
            yield return new WaitForSeconds(.5f);
            UiManager.instance.highScoreNotice.SetActive(true);
        }

        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.SetInt("CurrentLives", currentLives);

        yield return new WaitForSeconds(waitForLevelEnd);

        SceneManager.LoadScene(nextLevel);
    }

    public void PauseUnpause()
    {
        if(UiManager.instance.pauseScreen.activeInHierarchy)
        {
            UiManager.instance.pauseScreen.SetActive(false);
            Time.timeScale = 1f;
            SPlayerController.instance.stopMovement = false;
        } else
        {
            UiManager.instance.pauseScreen.SetActive(true);
            Time.timeScale = 0f;
            SPlayerController.instance.stopMovement = true;
        }
    }
}
