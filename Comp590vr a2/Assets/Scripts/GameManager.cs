using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int highscore = 0;
    public int playerHealth = 3;
    public int score = 0;
    public bool playing = false;

    [SerializeField] Text startHighScoreLabel;
    [SerializeField] Text deathHighScoreLabel;
    [SerializeField] Text deathScoreLabel;

    [SerializeField] Text statusLabel;

    [SerializeField] Canvas hud;
    [SerializeField] Canvas startScreen;
    [SerializeField] Canvas deathScreen;

    [SerializeField] public SphericEnemy enemyPrefab;
    private float timeLeft;
    private float waitTime = 7f;
    private float waitTimeReduce = .1f;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("highscore"))
        {
            highscore = PlayerPrefs.GetInt("highscore");
            startHighScoreLabel.text = "High Score: " + highscore.ToString();
        }
        UpdateStatus();
        EnhancedTouchSupport.Enable();
    }

    void UpdateStatus() {
        statusLabel.text = "Score: " + score.ToString() + "\nHealth: " + playerHealth.ToString();
    }


    void StartGame()
    {
        Debug.Log("Starting game!");
        playerHealth = 3;
        score = 0;
        playing = true;
        foreach (SphericEnemy e in transform.parent.GetComponentsInChildren<SphericEnemy>())
        {
            e.Kill();
        }
        UpdateStatus();
        hud.gameObject.SetActive(true);
        startScreen.gameObject.SetActive(false);
        deathScreen.gameObject.SetActive(false);
    }

    public void DamagePlayer()
    {
        playerHealth -= 1;
        UpdateStatus();
        Debug.Log("Health is now " + playerHealth);
        if (playerHealth <= 0) {
            GameOver();
        }
    }

    public void AddPoints(int points)
    {
        score += points;
        UpdateStatus();
    }

    void GameOver()
    {
        Debug.Log("Game Over");
        playing = false;
        if (score > highscore)
        {
            highscore = score;
            PlayerPrefs.SetInt("highscore", highscore);
            PlayerPrefs.Save();
        }

        hud.gameObject.SetActive(false);
        deathScreen.gameObject.SetActive(true);
        deathHighScoreLabel.text = "High Score: " + highscore.ToString();
        deathScoreLabel.text = "Score: " + score.ToString();

        foreach (SphericEnemy e in transform.parent.GetComponentsInChildren<SphericEnemy>())
        {
            e.Stop();
        }
    }

    private bool wasPressed = false;

    void FixedUpdate()
    {
        if (playing)
        {
            timeLeft -= Time.fixedDeltaTime;
            if (timeLeft < 0)
            {
                SphericEnemy enemy = Instantiate(enemyPrefab);
                waitTime = Mathf.Max(1f, waitTime - waitTimeReduce);
                timeLeft = waitTime;
            }
        } else
        {
            if (!wasPressed && Touchscreen.current.press.isPressed)
            {
                StartGame();
            }
            wasPressed = Touchscreen.current.press.isPressed;
        }
    }
}
