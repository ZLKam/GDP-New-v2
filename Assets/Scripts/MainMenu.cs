using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using taskProgress;
using HeatChecks;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Transform cameraPos;

    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject deathScreen;

    [SerializeField] private Image deathImage;
    [SerializeField] private TextMeshProUGUI deathText;

    // Start is called before the first frame update
    void Start()
    {
        TaskProgress.CO2Current = 0;
        TaskProgress.heatCurrent = 0;

        TaskProgress.heatFillAmount = 0f;
        TaskProgress.CO2FillAmount = 0f;

        HeatManager.airconOpen = false;
        HeatManager.fanOpen = false;
        HeatManager.windowOpen = false;

        DisplayDayText.seenTutorial = false;
        ChangeSceneController.enteredWork = false;
        ReceptionistController.seenPolicy = false;

        Time.timeScale = 1f;

        if (!TaskProgress.co2Death && !TaskProgress.heatDeath)
        {
            mainMenu.SetActive(true);
            deathScreen.SetActive(false);
        }
        else if (TaskProgress.heatDeath)
        {
            mainMenu.SetActive(false);
            deathScreen.SetActive(true);
            deathImage.color = Color.red;
            deathText.text = "You died of heat stroke!";
        }
        else if (TaskProgress.co2Death) 
        {
            mainMenu.SetActive(false);
            deathScreen.SetActive(true);
            deathImage.color = Color.black;
            deathText.text = "You got fired!";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnterGame() 
    {
        SceneManager.LoadScene("Day1 Outside");
    }

    public void Restart() 
    {
        TaskProgress.co2Death = false;
        TaskProgress.heatDeath = false;
        mainMenu.SetActive(true);
        deathScreen.SetActive(false);
    }
}
