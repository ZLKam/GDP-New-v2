using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IndicatorControl : MonoBehaviour
{
    public TextMeshProUGUI heatIndicator;
    public TextMeshProUGUI co2Indicator;

    private float fadeTime;
    private bool fadingIn;

    // Start is called before the first frame update
    void Start()
    {
        heatIndicator.CrossFadeAlpha(1, 0.0f, false);
        co2Indicator.CrossFadeAlpha(1, 0.0f, false);
        fadeTime = 0;
        fadingIn = false;

        FadeOut();
    }

    void FadeOut()
    {
        heatIndicator.CrossFadeAlpha(0, 0.5f, false);
        co2Indicator.CrossFadeAlpha(0, 0.5f, false);
        fadeTime += Time.deltaTime;
        if (heatIndicator.color.a == 1 && fadeTime > 1.5f) 
        {
            fadingIn = false;
            fadeTime = 0;
        }
    }

    void FadeIn() 
    {
        heatIndicator.CrossFadeAlpha(1, 0.2f, false);
        co2Indicator.CrossFadeAlpha(1, 0.2f, false);
        fadeTime += Time.deltaTime;
        
    }

    public void IndicatorButton() 
    {
        heatIndicator.CrossFadeAlpha(0, 0.0f, false);
        co2Indicator.CrossFadeAlpha(0, 0.0f, false);
        if (heatIndicator.color.a == 1 && co2Indicator.color.a ==1) 
        {
            StartCoroutine(FadeInandOut());
        }
    }

    IEnumerator FadeInandOut() 
    {
        FadeIn();
        yield return new WaitForSeconds(1f);
        FadeOut();
    }
}
