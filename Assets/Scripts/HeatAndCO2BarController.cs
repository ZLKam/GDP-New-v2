using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using taskProgress;

public class HeatAndCO2BarController : MonoBehaviour
{
    [SerializeField] private Image heatMask;
    [SerializeField] private Image co2Mask;

    private void Start()
    {
        heatMask.fillAmount = TaskProgress.heatFillAmount;
        co2Mask.fillAmount = TaskProgress.CO2FillAmount;
    }

    private void Update()
    {
        TaskProgress.heatCurrent = 1f * Time.deltaTime;
        TaskProgress.CO2Current = 1f * Time.deltaTime;
        heatMask.fillAmount += TaskProgress.heatCurrent / TaskProgress.heatMaximum;
        co2Mask.fillAmount += TaskProgress.CO2Current / TaskProgress.CO2Maximum;
        TaskProgress.heatFillAmount = heatMask.fillAmount;
        TaskProgress.CO2FillAmount = co2Mask.fillAmount;

        if (heatMask.fillAmount >= 1f || co2Mask.fillAmount >= 1f)
        {
            Time.timeScale = 0;
            Debug.Log("you died");
        }
    }
}
