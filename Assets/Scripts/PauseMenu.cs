﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    GameObject menu;
    public GameObject[] buttons;
    bool paused = false;

    // Use this for initialization
    void Start () {
        menu = transform.GetChild(0).gameObject;
        foreach(GameObject button in buttons) {
            button.gameObject.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (!paused) { Pause(); }
            else { Resume(); }
        }
    }

    private void Pause() {
        Time.timeScale = 0;
        foreach (GameObject button in buttons) {
            button.gameObject.SetActive(true);
        }
        paused = true;
    }

    public static void QuitGame() {
        Application.Quit();
        Debug.Log("Quit");
    }

    public void Resume() {
        foreach (GameObject button in buttons) {
            button.gameObject.SetActive(false);
        }
        Time.timeScale = 1;
        paused = false;
    }

    public void Restart() {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
