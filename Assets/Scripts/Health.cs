using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] private float health = 100f;
    [SerializeField] private float baseDamage = 10f;
    [SerializeField] private float noRigidbodyMultiplier = 2f;

    private void OnCollisionEnter(Collision collision)
    {
        float damage = baseDamage;

        if (!collision.gameObject.CompareTag("Police"))
        {
            if (collision.gameObject.GetComponent<Rigidbody>() == null)
            {
                damage *= noRigidbodyMultiplier;
            }
            else
            {
                Rigidbody otherRigidbody = collision.gameObject.GetComponent<Rigidbody>();
                float mass = otherRigidbody.mass;
                damage *= mass / 100;
            }

            ApplyDamage(damage);
        }
    }

    public void ApplyDamage(float damage)
    {
        health -= damage;
        Debug.Log("Здоровье машины: " + health);

        if (health <= 0)
        {
            SceneManager.LoadScene("LostScene");
            Debug.Log("Машина разрушена!");
        }
    }
}
