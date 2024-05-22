using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyVision : MonoBehaviour
{
    [SerializeField] private string targetTag = "Car";
    [SerializeField] private int rays = 8;
    [SerializeField] private int distance = 33;
    [SerializeField] private float angle = 40;
    [SerializeField] private Vector3 offset;

    private Transform _target;
    private NavMeshAgent _agent;
    private NavMesh _nav;

    void Start()
    {

        _target = GameObject.FindGameObjectWithTag(targetTag).transform;
        _agent = GetComponent<NavMeshAgent>();
        _nav = GetComponent<NavMesh>();
    }

    bool GetRaycast(Vector3 dir)
    {
        bool result = false;
        RaycastHit hit = new RaycastHit();
        Vector3 pos = transform.position + offset;
        if (Physics.Raycast(pos, dir, out hit, distance))
        {
            if (hit.transform == _target)
            {
                result = true;
                Debug.DrawLine(pos, hit.point, Color.green);
            }
            else
            {
                Debug.DrawLine(pos, hit.point, Color.blue);
            }
        }
        else
        {
            Debug.DrawRay(pos, dir * distance, Color.red);
        }
        return result;
    }

    bool RayToScan()
    {
        bool result = false;
        bool a = false;
        bool b = false;
        float j = 0;
        for (int i = 0; i < rays; i++)
        {
            var x = Mathf.Sin(j);
            var y = Mathf.Cos(j);

            j += angle * Mathf.Deg2Rad / rays;

            Vector3 dir = transform.TransformDirection(new Vector3(x, 0, y));
            if (GetRaycast(dir)) a = true;

            if (x != 0)
            {
                dir = transform.TransformDirection(new Vector3(-x, 0, y));
                if (GetRaycast(dir)) b = true;
            }
        }

        if (a || b) result = true;
        return result;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, _target.position) < distance)
        {
            if (RayToScan())
            {
                //_agent.enabled = true;   // Контакт с целью
                _nav.enabled = true;
            }
            else
            {
                // Поиск цели...
                //_nav.enabled = false;
            }
        }
    }
}
