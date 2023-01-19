using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceptionistController : MonoBehaviour
{
    [SerializeField] private GameObject policyUI;
    [SerializeField] private GameObject mornNewsUI;

    private bool seenPolicy = false;
    // Start is called before the first frame update
    void Start()
    {
        policyUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !seenPolicy) 
        {
            policyUI.SetActive(true);
            Time.timeScale = 0f;
            seenPolicy = !seenPolicy;
        }
    }
    public void ClosePressed()
    {
        if (policyUI.activeInHierarchy)
        {
            policyUI.SetActive(false);
            Time.timeScale = 1f;
        }
        else if(mornNewsUI.activeInHierarchy)
        {
            mornNewsUI.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
