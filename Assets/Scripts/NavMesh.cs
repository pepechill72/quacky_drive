using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class NavMesh : MonoBehaviour
{
    public float visionRange = 100f;

    [SerializeField] private Transform player;
    [SerializeField] private Vector3 originalPosition;
    [SerializeField] private GameObject saveZone;
    [SerializeField] private TextMeshProUGUI _textTimer;
    [SerializeField] private TextMeshProUGUI _textUI;

    private NavMeshAgent _agent;
    private float _timer;

    private bool isChasing;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        originalPosition = transform.position;
    }


    void Update()
    {
        Chas();
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
            _timer += Time.deltaTime;

            if (_timer >= 2)
                GameOver();
        }
    }

    void GameOver()
    {
        Debug.LogError("Game Over!");
        _textUI.text = "You are under arrest";
        _timer = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Car"))
        {
            Debug.Log("Догнали!");
            _timer = 0;
            isChasing = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Car"))
        {
            Debug.Log("Убежал!");
            isChasing = false;
        }
    }
}
