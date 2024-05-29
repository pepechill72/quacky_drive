using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class NavMesh : MonoBehaviour
{
    public float visionRange = 100f;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _textTimer;
    [SerializeField] private TextMeshProUGUI _textUI;

    [Header("Settings")]
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 originalPosition;
    [SerializeField] private GameObject saveZone;
    [SerializeField] private float _timerArrest = 3f;

    private NavMeshAgent _agent;
    private float _curTimer;
    private bool isChasing;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        originalPosition = transform.position;

        _curTimer = _timerArrest;
    }


    void Update()
    {
        if (saveZone == null)
        {
            visionRange = 100f;
            _textTimer.SetActive(false);    
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= visionRange)
        {
            _agent.SetDestination(player.position);
        }
        else
        {
            _agent.SetDestination(originalPosition);
        }
    }

    private void Chas()
    {
        if (isChasing)
        {
            _timerArrest -= Time.deltaTime;

            int seconds = Mathf.FloorToInt(_timerArrest);
            int milliseconds = Mathf.FloorToInt((_timerArrest - seconds) * 100);
            string timeString = string.Format("{0:00}:{1:00}", seconds, milliseconds);

            _textUI.text = $"Arrest through {timeString}";

            if (_timerArrest <= 0)
                GameOver();
        }
    }

    void GameOver()
    {
        Debug.LogError("Game Over!");
        SceneManager.LoadScene("LostScene");
        _timerArrest = 0;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Car"))
        {
            Debug.Log("Догнали!");
            isChasing = true;
            Chas();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Car"))
        {
            Debug.Log("Убежал!");
            isChasing = false;
            _timerArrest = _curTimer;
            _textUI.text = " ";
        }
    }
}
