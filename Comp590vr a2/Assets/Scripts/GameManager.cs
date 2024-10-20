﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int highscore = 0;
    public int playerHealth = 3;
    public int score = 0;
    public bool playing = false;

    [SerializeField] TextMeshProUGUI menuMessageLabel;
    [SerializeField] TextMeshProUGUI scoresLabel;

    [SerializeField] TextMeshProUGUI statusLabel;

    [SerializeField] Canvas hud;
    [SerializeField] GameObject menu;

    [SerializeField] public SphericEnemy enemyPrefab;
    private float timeLeft;
    private float waitTime = 5f;
    private float waitTimeReduce = .1f;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("highscore"))
        {
            highscore = PlayerPrefs.GetInt("highscore");
            scoresLabel.text = "High Score: " + highscore.ToString();
        }
        UpdateStatus();
    }

    void UpdateStatus() {
        statusLabel.text = "Score: " + score.ToString() + "\nHealth: " + playerHealth.ToString();
    }


    public void StartGame()
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
        menu.SetActive(false);
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
        menu.SetActive(true);
        menuMessageLabel.text = "You Died!";
        scoresLabel.text = "Score: " + score.ToString() + "\nHigh Score: " + highscore.ToString();

        foreach (SphericEnemy e in transform.parent.GetComponentsInChildren<SphericEnemy>())
        {
            e.Stop();
        }
    }

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
        }
    }
}
