using System.Collections;
using System.Collections.Generic;
using Audio;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public GameObject introText;
    public GameObject mainMenuPane;
    public GameObject controlsPane;
    public GameObject creditsPane;
    public float defaultDuration = 11.0f;
    public AudioClip mainMenuTheme;
    public AudioClip staticTheme;

    private bool menuEnabled = false;

    public float creditsSwitchUnlockTime = 1.9f;
    public float controlsSwitchUnlockTime = 0.3f;

    private float nextAllowedSwitch = 0.0f;

    void Start() {
        introText.SetActive(true);
        mainMenuPane.SetActive(false);
        controlsPane.SetActive(false);
        creditsPane.SetActive(false);
        
        AudioManager.Instance.SetMainMenuState();

        Invoke("EnableMenu", defaultDuration);
    }

    public void Play() {
        SceneManager.LoadScene(1);
    }

    public void Quit() {
        Application.Quit();
    }

    private void Update() {
        if (Input.anyKey) {
            if (!menuEnabled) {
                EnableMenu();
            }
            else if (Time.time >= nextAllowedSwitch) {
                OpenMainMenu();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace) || Input.GetKeyDown(KeyCode.Delete)) {
            OpenMainMenu();
        }
    }

    private void EnableMenu() {
        
        if (!menuEnabled) {
            menuEnabled = true;
            introText.SetActive(false);
            mainMenuPane.SetActive(true);
            controlsPane.SetActive(false);
            creditsPane.SetActive(false);
            PlaySounds();
        }
    }

    public void OpenCredits() {
        mainMenuPane.SetActive(false);
        controlsPane.SetActive(false);
        creditsPane.SetActive(true);
        nextAllowedSwitch = Time.time + creditsSwitchUnlockTime;
    }

    public void OpenControls() {
        mainMenuPane.SetActive(false);
        controlsPane.SetActive(true);
        creditsPane.SetActive(false);
        nextAllowedSwitch = Time.time + controlsSwitchUnlockTime;
    }

    public void OpenMainMenu() {
        mainMenuPane.SetActive(true);
        controlsPane.SetActive(false);
        creditsPane.SetActive(false);
    }

    private void PlaySounds() {
        SoundManager.instance.PlayMusic(mainMenuTheme, false);
        Invoke("PlayStatic", mainMenuTheme.length);
    }

    private void PlayStatic() {
        SoundManager.instance.SetMusicVolume(0.3f);
        SoundManager.instance.PlayMusic(staticTheme, true);
    }
}
