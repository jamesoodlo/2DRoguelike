using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    InputHandle inputHandle;
    PlayerStats playerStats;
    public Info info;
    public TextMeshProUGUI scoreText, finalScoreText;
    public TextMeshProUGUI stageText;
    [Header("Panel")]
    public GameObject menuPanel, pausePanel, settingPanel, deadPanel;

    [Header("Setting")]
    public SettingData settingData;
    public Toggle fullScreenValue;
    public Slider effectSlider, musicSlider, ambientSlider;

    [Header("Audio Source")]
    public SoundFx[] soundFx;
    public AudioSource effectSound, musicSound, ambientSound;

    [Header("Loading")]
    public GameObject loadingPanel;
    public Slider loadingBar;
    public float timeToLoading;

    private void Awake() 
    {
        inputHandle = GetComponentInParent<InputHandle>();
        playerStats = GetComponentInParent<PlayerStats>();
    }


    void Start()
    {
        deadPanel.SetActive(false);
        pausePanel.SetActive(false);
        settingPanel.SetActive(false);
    }

    void Update()
    {
        menuPanel.SetActive(settingData.isMenu);

        soundFx = FindObjectsOfType<SoundFx>();

        if(info.stage < 1) info.stage = 1;

        scoreText.text = info.score.ToString();
        stageText.text = info.stage.ToString();
        finalScoreText.text = scoreText.text;

        if(!settingData.isMenu) 
        {
            timeToLoading += Time.deltaTime;
        }
        else
        {
            Time.timeScale = 0;
        }

        loadingBar.value = timeToLoading;

        if(timeToLoading >= 3.0f)
        {
            loadingPanel.SetActive(false);

            if(inputHandle.escape && !settingData.isMenu)
            {
                Time.timeScale = 0;
                pausePanel.SetActive(true);
            }
        }
        else
        {
            SetAllSetting();
        }

        for (int i = 0; i < soundFx.Length; i++)
        {
            if(soundFx != null)
                soundFx[i].volumeSfx = settingData.effectSound;
        }

        if(playerStats.currentHealth <= 0)
        {
            Time.timeScale = 0;
            deadPanel.SetActive(true);
        }
    }

    public void Play()
    {
        settingData.isMenu = false;
        Time.timeScale = 1;
    }

    public void Menu()
    {
        pausePanel.SetActive(false);
        deadPanel.SetActive(false);
        info.stage = 1;
        info.score = 0;
        info.atkOrb = 0;
        info.defOrb = 0;
        info.spdOrb = 0;
        settingData.isMenu = true;
        SceneManager.LoadScene("Game");
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void Pause()
    {
        if(settingData.isMenu)
        {
            settingPanel.SetActive(false);
        }
        else
        {   
            pausePanel.SetActive(true);
            settingPanel.SetActive(false);
        }
    }

    public void setting()
    {
        pausePanel.SetActive(false);
        settingPanel.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void SetAllSetting()
    {
        effectSound.volume = settingData.effectSound;
        effectSlider.value = effectSound.volume; 

        musicSound.volume = settingData.musicSound;
        musicSlider.value = musicSound.volume; 

        ambientSound.volume = settingData.ambientSound;
        ambientSlider.value = ambientSound.volume; 

        fullScreenValue.isOn = settingData.isFullScreen;
    }

    public void MusicSetting()
    {
        settingData.musicSound = musicSlider.value;
        musicSound.volume = settingData.musicSound;
    }

    public void AmbientSetting()
    {
        settingData.ambientSound = ambientSlider.value;
        ambientSound.volume = settingData.ambientSound;
    }

    public void EffectSetting()
    {
        settingData.effectSound = effectSlider.value;
        effectSound.volume = settingData.effectSound;
    }

    public void FullScreenSetting(bool isFullScreen)
    {
        isFullScreen = fullScreenValue.isOn;
        settingData.isFullScreen = fullScreenValue.isOn;
        Screen.fullScreen = isFullScreen;   
    }
}
