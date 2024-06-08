using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadObject : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Collider collider = GetComponent<Collider>();
        if (collision.gameObject.CompareTag("Car") || collision.gameObject.CompareTag("Police"))
        {
            collider.isTrigger = true;
            Destroy(gameObject, 3f);
        }
    }
}
