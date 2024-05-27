using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CallingTimeLine : MonoBehaviour
{
    [SerializeField] private PlayableDirector director;
    [SerializeField] private Transform pointParking;
    [SerializeField] private float timer = 8f;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            timer -= Time.deltaTime;
            director.Play();
            other.GetComponent<CarController>().Rb.isKinematic = true;

            if (timer <= 0)
            {
                other.GetComponent<CarController>().Rb.isKinematic = false;
                gameObject.SetActive(false);
            }
        }
    }
}
