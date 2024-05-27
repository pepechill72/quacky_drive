using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SaveZone : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _textTimer;
    [SerializeField] private string _textRun = "Run!!!";

    [Header("Settings")]
    [SerializeField] private float timer;

    [Header("Police")]
    [SerializeField] private string textTag = "Police";

    private NavMesh _visionPolice;

    private List<GameObject> policeCars = new List<GameObject>();

    private void OnTriggerStay(Collider other)
    {
        Timer();

        if (other.CompareTag("Car"))
        {
            TimerRunning();
            TimerStoping();
        }
    }

    private void Timer()
    {
        timer -= Time.deltaTime;
        int seconds = Mathf.FloorToInt(timer);
        int milliseconds = Mathf.FloorToInt((timer - seconds) * 100);
        string timeString = string.Format("{0:00}:{1:00}", seconds, milliseconds);
        _textTimer.text = timeString;
    }

    private void TimerRunning()
    {
        if (timer > 0)
        {
            Debug.Log("SaveZone");

            GameObject[] policeArray = GameObject.FindGameObjectsWithTag(textTag);

            foreach (GameObject police in policeArray)
            {
                policeCars.Add(police);
                _visionPolice = police.GetComponent<NavMesh>();
                _visionPolice.visionRange = 0f;
            }
        }
    }

    private void TimerStoping()
    {
        if (timer <= 0)
        {
            _textTimer.text = _textRun;
            foreach (GameObject police in policeCars)
            {
                _visionPolice = police.GetComponent<NavMesh>();
                _visionPolice.visionRange = 100f;
            }
            policeCars.Clear();
        }
    }
}
