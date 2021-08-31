using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text ScoreText;

    public Slider HealthBar;

    public Image GameOverScreen;

    public int TotalScore = 0;

    public int Health = 100;

    public static GameManager ManagerInstance;

    public GameObject Player;

    private void Awake()
    {
        ManagerInstance = this;
    }

    public void AddScore(int Count)
    {
        TotalScore += Count;
    }

    public void DamagePlayer(int Count)
    {
        if (Health > 0)
        {
            Health -= Count;
            HealthBar.value = Health;
        }
        else if(Health <= 0) GameOver();
    }

    public void GameOver()
    {
        GameOverScreen.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void Update()
    {
        ScoreText.text = "Score: " + TotalScore;
    }
}
