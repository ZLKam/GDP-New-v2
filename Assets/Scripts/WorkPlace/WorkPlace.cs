using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using taskProgress;
using HeatChecks;

public class WorkPlace : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Entrance;

    private static bool enterRoomBefore = false;

    // Start is called before the first frame update
    void Start()
    {
        if (enterRoomBefore)
            return;
        Player.transform.position = Entrance.transform.position;
        enterRoomBefore = true;
    }

    private void Update()
    {
        if (HeatManager.airconOpen || HeatManager.fanOpen || HeatManager.windowOpen)
            return;
        TaskProgress.heatCurrent = 1f * Time.deltaTime;
        TaskProgress.CO2Current = 1f * Time.deltaTime;
        TaskProgress.heatFillAmount += TaskProgress.heatCurrent * Time.deltaTime;
        TaskProgress.CO2FillAmount += TaskProgress.CO2Current * Time.deltaTime;
    }
}
