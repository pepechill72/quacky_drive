using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// For user multiplatform control.
/// </summary>
[RequireComponent(typeof(CarController))]
public class UserControl : MonoBehaviour
{

    CarController ControlledCar;

    public float Horizontal { get; private set; }
    public float Vertical { get; private set; }
    public bool Brake { get; private set; }

    public static MobileControlUI CurrentUIControl { get; set; }

    public bool IsSaveZone;

    private void Awake()
    {
        ControlledCar = GetComponent<CarController>();
        CurrentUIControl = FindObjectOfType<MobileControlUI>();
    }

    void Update()
    {
        if (CurrentUIControl != null && CurrentUIControl.ControlInUse)
        {
            //Mobile control.
            Horizontal = CurrentUIControl.GetHorizontalAxis;
            Vertical = CurrentUIControl.GetVerticalAxis;
        }
        else
        {
            //Standart input control (Keyboard or gamepad).
            Horizontal = SimpleInput.GetAxis("Horizontal");
            Vertical = SimpleInput.GetAxis("Vertical");
            if (Vertical == 0 && !IsSaveZone)
                Vertical = 1;
            Brake = SimpleInput.GetButton("Jump");
        }

        //Apply control for controlled car.
        ControlledCar.UpdateControls(Horizontal, Vertical, Brake);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("SaveZone"))
        {
            IsSaveZone = true;

            gameObject.transform.position = other.transform.position;
            gameObject.transform.rotation = other.transform.rotation;

            ControlledCar.Rb.isKinematic = true;

            if (Vertical < 0)
            {
                IsSaveZone = false;
                ControlledCar.Rb.isKinematic = false;
                Destroy(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SaveZone"))
        {
            Debug.Log("Exit");
            IsSaveZone = false;
        }
    }
}
