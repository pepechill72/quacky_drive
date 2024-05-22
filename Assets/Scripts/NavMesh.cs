using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMesh : MonoBehaviour
{
    [SerializeField] private GameObject target;

    private NavMeshAgent agent;
    private float timer;
    private bool isChasing;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        agent.SetDestination(target.transform.position);

        Chas();
    }

    private void Chas()
    {
        if (isChasing)
        {
            timer += Time.deltaTime;

            if (timer >= 3)
                GameOver();
        }
    }

    void GameOver()
    {
        Debug.LogError("Game Over!");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Car"))
        {
            Debug.Log("Догнали!");
            timer = 0;
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
