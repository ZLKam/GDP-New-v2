using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DisplayDayText : MonoBehaviour
{
    RandomEvent randomEvent;

    public TextMeshProUGUI dayText;

    public GameObject tutorialObject;

    public static bool seenTutorial;

    // Start is called before the first frame update
    void Start()
    {
        //if (SceneManager.GetActiveScene().name == "Day1 Outside")
        //{
        //    dayText.text = "Day1 ...";
        //    dayText.gameObject.SetActive(true);
        //    StartCoroutine(ShowDayNumber());
        //}
        string sceneName = SceneManager.GetActiveScene().name;
        switch (sceneName)
        {
            case "Day1 Outside":
                dayText.text = "Day 1 ...";
                break;
            case "Day1 Inside Level 1":
                dayText.text = "Level 1 ...";
                break;
            case "Day1 Inside Level 2":
                dayText.text = "Level 2 ...";
                break;
        }
        dayText.gameObject.SetActive(true);

        if (tutorialObject != null && !seenTutorial)
        {
            tutorialObject.SetActive(true);
            Time.timeScale = 0;
        }
        
        StartCoroutine(ShowDayNumber());
    }


    IEnumerator ShowDayNumber() 
    {
        yield return new WaitForSeconds(1);
        dayText.gameObject.SetActive(false);
    }

    public void ExitTutorial() 
    {
        tutorialObject.SetActive(false);
        Time.timeScale = 1;
        seenTutorial = true;
    }
}
