﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatGUI : MonoBehaviour {

    public GameObject combatGUI;
    public GameObject gameOverGUI;
    private Character player;

    private bool gameOver = false;

	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player").GetComponent<Character>();
        gameOverGUI.SetActive(false);
        combatGUI.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
        if (player.IsDead && !gameOver) {
            GameOver();
        }
	}

    private void GameOver() {
        gameOver = true;
        combatGUI.SetActive(false);
        gameOverGUI.SetActive(true);
    }

    public void Restart() {
        CombatManager.GameOver = false;
        gameOverGUI.SetActive(false);
        combatGUI.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitProgram() {
        Application.Quit();
    }
}
