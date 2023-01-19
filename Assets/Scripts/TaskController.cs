using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using taskProgress;
using HeatChecks;

public class TaskController : MonoBehaviour
{
    [SerializeField] private List<string> tasks = new();

    [SerializeField] private GameObject mornNewsUI;

    public Transform player;
    public TextMeshProUGUI taskHintText;
    public Button interactTaskButton;
    public GameObject joystick;

    private Transform currentTask;
    private Collider2D currentTaskCollider;
    private GameObject taskArrow;

    private bool enteredTaskBefore = false;
    private bool showedCompletedText = false;
    private bool taskFinished = false;
    private bool inProgress = false;

    RandomEvent randomEvent;

    // Start is called before the first frame update
    void Start()
    {
        interactTaskButton.gameObject.SetActive(false);
        randomEvent = GetComponent<RandomEvent>();
        foreach (Transform child in transform)
        {
            if (child.name == "Task Arrow")
                continue;
            tasks.Add(child.name);
        }
        taskArrow = transform.Find("Task Arrow").GetComponent<SpriteRenderer>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (tasks.Count > 0)
        {
            currentTask = transform.Find(tasks[0]);
            currentTaskCollider = currentTask.gameObject.GetComponent<Collider2D>();
            DrawTaskHint(player.position, currentTask.position);
            taskHintText.text = "Next Task:" + "\n" + currentTask.name;

            CheckCollision();
            if (inProgress)
            {
                joystick.gameObject.SetActive(false);
                if (TaskProgress.taskCurrent >= TaskProgress.taskMaximum)
                {
                    inProgress = false;
                    taskFinished = true;
                    TaskProgress.taskProgressBar.SetActive(false);
                    TaskProgress.taskCurrent = 0;
                }
            }
            CheckPlayerInTask();
        }
        else
        {
            if (!showedCompletedText)
            {
                StartCoroutine(TaskHintTextCoroutine());
            }
            interactTaskButton.gameObject.SetActive(false);
            Debug.Log("all tasks completed");
        }

        SpriteRenderer spriteRenderer = currentTask.GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = true;
    }

    private void FixedUpdate()
    {
        if (inProgress && currentTaskCollider.CompareTag("Task"))
        {
            TaskProgress.taskCurrent += 0.2f;
            TaskProgress.heatFillAmount += 10f * Time.deltaTime / TaskProgress.heatMaximum;
        }
        else if (inProgress && currentTaskCollider.CompareTag("CookingTask"))
        {
            TaskProgress.taskCurrent += 2f;
        }
        else if (inProgress && currentTaskCollider.CompareTag("FoodTask")) 
        {
            TaskProgress.taskCurrent += 100f;
            mornNewsUI.SetActive(true);
            Time.timeScale = 0f;
        }
        else if (inProgress && currentTaskCollider.CompareTag("ExerciseTask"))
        {
            TaskProgress.taskCurrent += 0.3f;
            TaskProgress.heatFillAmount += 12f * Time.deltaTime / TaskProgress.heatMaximum;
        }
        else if (inProgress && currentTaskCollider.CompareTag("InstantTask"))
        {
            TaskProgress.taskCurrent += 100f;
        }

        if (HeatManager.airconOpen) 
        {
            TaskProgress.heatFillAmount -= 8f * Time.deltaTime / TaskProgress.heatMaximum;
            TaskProgress.CO2FillAmount += 8f * Time.deltaTime / TaskProgress.CO2Maximum;
        }
        if (HeatManager.fanOpen)
        {
            TaskProgress.heatFillAmount -= 5f * Time.deltaTime / TaskProgress.heatMaximum;
            TaskProgress.CO2FillAmount += 2f * Time.deltaTime / TaskProgress.CO2Maximum;
        }
        if (HeatManager.windowOpen)
        {
            TaskProgress.heatFillAmount -= 2f * Time.deltaTime / TaskProgress.heatMaximum;
        }
        if (HeatManager.windowOpen && HeatManager.airconOpen) 
        {
            TaskProgress.CO2FillAmount += 10f * Time.deltaTime / TaskProgress.CO2Maximum;
        }
        if (HeatManager.windowOpen && HeatManager.airconOpen && HeatManager.fanOpen)
        {
            TaskProgress.CO2FillAmount += 20f * Time.deltaTime / TaskProgress.CO2Maximum;
        }
    }

    private void DrawTaskHint(Vector3 playerPosition, Vector3 taskPosition)
    {
        taskArrow.GetComponent<SpriteRenderer>().enabled = true;
        Vector3 offset = new(0, 1);
        Vector3 directionToTask = taskPosition - playerPosition;

        if (directionToTask.magnitude < 3f)
        {
            taskArrow.GetComponent<SpriteRenderer>().enabled = false;
            return;
        }
        directionToTask.Normalize();
        directionToTask *= 2f;

        Vector3 endPoint = playerPosition + directionToTask;
        taskArrow.transform.position = endPoint + offset;

        endPoint = transform.TransformPoint(endPoint);
        Vector3 rightAnglePoint = new(endPoint.x, playerPosition.y);
        float oppositeLength = Mathf.Abs((endPoint - rightAnglePoint).magnitude);
        float adjacentLength = Mathf.Abs((rightAnglePoint - playerPosition).magnitude);

        float theta = Mathf.Atan(oppositeLength / adjacentLength) * 180 / Mathf.PI;
        float xDirection = rightAnglePoint.x - playerPosition.x;
        float yDirection = endPoint.y - rightAnglePoint.y;
        if (xDirection > 0 && yDirection > 0)
        {
            taskArrow.transform.rotation = Quaternion.Euler(0, 0, theta);
        }
        else if (xDirection < 0 && yDirection > 0)
        {
            taskArrow.transform.rotation = Quaternion.Euler(0, 0, 180 - theta);
        }
        else if (xDirection < 0 && yDirection < 0)
        {
            taskArrow.transform.rotation = Quaternion.Euler(0, 0, 180 + theta);
        }
        else if (xDirection > 0 && yDirection < 0)
        {
            taskArrow.transform.rotation = Quaternion.Euler(0, 0, -theta);
        }
        
    }

    private void CheckPlayerInTask()
    {
        if (taskFinished == true)
        {
            transform.Find(tasks[0]).gameObject.SetActive(false);
            tasks.Remove(tasks[0]);
            if (tasks.Count > 0)
                transform.Find(tasks[0]).gameObject.SetActive(true);
            else
                return;
            taskFinished = false;
            enteredTaskBefore = false;
            joystick.gameObject.SetActive(true);
        }
    }

    IEnumerator TaskHintTextCoroutine()
    {
        taskHintText.text = "You had completed all tasks!";
        showedCompletedText = true;
        yield return new WaitForSeconds(5f);
        taskHintText.text = null;
    }

    private void CheckCollision()
    {
        if (currentTaskCollider.IsTouching(player.GetComponent<Collider2D>()) && !inProgress)
        {
            interactTaskButton.GetComponentInChildren<TextMeshProUGUI>().text = "Do Task";
            interactTaskButton.gameObject.SetActive(true);
            interactTaskButton.onClick.RemoveAllListeners();
            interactTaskButton.onClick.AddListener(DoTask);
            
            if (!enteredTaskBefore)
            {
                randomEvent.rand = Random.Range(0f, 1f);
                randomEvent.DisplayEventTextFunction();
                enteredTaskBefore = true;
            }
        }
        else
        {
            if (ChangeSceneController.touchingDoor)
                return;
            interactTaskButton.gameObject.SetActive(false);
        }
    }

    private void DoTask()
    {
        interactTaskButton.gameObject.SetActive(false);
        TaskProgress.taskProgressBar.SetActive(true);
        //TaskProgress.taskProgressBar.transform.position = new Vector3(player.position.x, player.position.y + 2f + player.position.z);
        inProgress = true;
    }
}
