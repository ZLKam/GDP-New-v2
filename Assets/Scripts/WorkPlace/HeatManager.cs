using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace HeatChecks
{
    public class HeatManager : MonoBehaviour
    {
        public static bool windowOpen;
        public static bool fanOpen;
        public static bool airconOpen;

        [SerializeField] ApplianceAttribute window;
        [SerializeField] ApplianceAttribute fan;
        [SerializeField] ApplianceAttribute aircon;

        private Sprite windowOpenSprite;
        private Sprite windowCloseSprite;
        private float yRot;
        private Sprite airconOpenSprite;
        private Sprite airconCloseSprite;
        
        // Start is called before the first frame update
        void Start()
        {
            windowOpenSprite = Resources.Load<Sprite>("windowopen");
            windowCloseSprite = Resources.Load<Sprite>("windowdraped");

            airconCloseSprite = Resources.Load<Sprite>("Air-con");
            airconOpenSprite = Resources.Load<Sprite>("aicongif");
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var startLinePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                if (window != null && window.IsCollidingWith(startLinePos.x, startLinePos.y))
                {
                    windowOpen = !windowOpen;
                }
                if (fan != null && fan.IsCollidingWith(startLinePos.x, startLinePos.y))
                {
                    fanOpen = !fanOpen;
                }
                if (aircon != null && aircon.IsCollidingWith(startLinePos.x, startLinePos.y))
                {
                    airconOpen = !airconOpen;
                }
            }

            if (windowOpen)
            {
                window.GetComponent<SpriteRenderer>().sprite = windowOpenSprite;
            }
            else
            {
                window.GetComponent<SpriteRenderer>().sprite = windowCloseSprite;
            }
            if (fanOpen)
            {
                yRot++;
                fan.transform.rotation = Quaternion.Euler(new Vector3(0, yRot, 0));
            }
            else
            {
                yRot = 0;
                fan.transform.rotation = Quaternion.Euler(Vector3.zero);
            }
            if (airconOpen)
            {
                aircon.GetComponent<SpriteRenderer>().sprite = airconOpenSprite;
                aircon.GetComponent<SpriteRenderer>().color = Color.green;
            }
            else
            {
                aircon.GetComponent<SpriteRenderer>().sprite = airconCloseSprite;
                aircon.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }
}
