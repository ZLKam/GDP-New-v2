using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace taskProgress
{
    public class TaskProgress : MonoBehaviour
    {
        public static float taskMaximum = 100;
        public static float taskCurrent;
        [SerializeField] private Image taskMask;

        public static float heatMaximum = 100;
        public static float heatCurrent;
        [SerializeField] private Image heatMask;

        public static float CO2Maximum = 100;
        public static float CO2Current;
        [SerializeField] private Image CO2Mask;

        public static float heatFillAmount;
        public static float CO2FillAmount;

        public static GameObject taskProgressBar;
        // Start is called before the first frame update
        void Start()
        {
            taskProgressBar = transform.Find("TaskProgress").gameObject;
            if (taskProgressBar.activeInHierarchy) 
            {
                taskProgressBar.SetActive(false);
            }
            heatMask.fillAmount = heatFillAmount;
            CO2Mask.fillAmount = CO2FillAmount;
        }

        // Update is called once per frame
        void Update()
        {
            GetCurrentFill();
        }

        public void GetCurrentFill()
        {
            taskMask.fillAmount = taskCurrent / taskMaximum;
            heatMask.fillAmount = heatFillAmount;
            CO2Mask.fillAmount = CO2FillAmount;

            if (heatMask.fillAmount >= 1f ||CO2Mask.fillAmount >= 1f)
            {
                Time.timeScale = 0;
                Debug.Log("you died");
            }
            
            heatFillAmount = heatMask.fillAmount;
            CO2FillAmount = CO2Mask.fillAmount;
        }
    }
}
