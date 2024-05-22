using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CallingTimeLine : MonoBehaviour
{
    [SerializeField] private PlayableDirector director;
    [SerializeField] private Transform pointParking;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Car"))
        {
            director.Play();
            gameObject.SetActive(false);
            other.gameObject.transform.position = pointParking.position;
        }
    }
}
