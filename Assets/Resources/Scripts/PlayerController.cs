using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region OTHER VARIABLES

    [Header("Wheel Colliders")]
    [SerializeField] private List<WheelCollider> wheelColliders;
    [Space]
    [Header("Wheel Objects")]
    [SerializeField] private List<GameObject> wheelObjects;
    [Space]
    [Header("VARIABLES")]
    [SerializeField] private float wheelSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Transform forcePoint;
    [SerializeField] private Transform groundCheckPosition;
    [SerializeField] private Rigidbody myRigidBody;

    #endregion


    #region PRIVATE VARIABLES

    private float _defaultAngularDrag;
    private bool _isGrounded;
    private float prevAngle;
    private int flips;
    private RaycastHit _raycastHit;

    #endregion




    private void Start()
    {
        _defaultAngularDrag = myRigidBody.angularDrag;

        transform.eulerAngles = new Vector3(0, -90, 0);

    }

    private void FixedUpdate()
    {
        if (GameManager.isStart)
        {
            MoveWheels(); // Move Wheels and WheelTurnSimulate
            IsGrounded(); // If it touches the ground
            MotorcycleFlip();
            CalculateFlip();

        }
    }


    #region Player Is the Player on the Ground Check

    // We Give Virtual Light To Check If Our Player Is On The Ground.
    private void IsGrounded()
    {
        if (Physics.Raycast(groundCheckPosition.position, -transform.up, out _raycastHit, 3f))
        {
            _isGrounded = true;
        }
        else
        {
            _isGrounded = false;
        }
    }

    #endregion

    #region Motorcycle Controll METHODS

    // We Spin The Engine Wheels.
    private void WheelTurnSimulate()
    {
        // TO DO Turn Wheel Objects
        wheelObjects[0].transform.Rotate(500 * Time.deltaTime, 0, 0);
        wheelObjects[1].transform.Rotate(500 * Time.deltaTime, 0, 0);

    }

    // We Give Spinning Power to Our Wheel Colliders.
    private void MoveWheels()
    {
        foreach (var wheel in wheelColliders)
        {
            WheelTurnSimulate();
            wheel.motorTorque = wheelSpeed * Time.deltaTime;
        }
    }

    // If the Motorcycle is in the air, we make it roll over.
    private void MotorcycleFlip()
    {
        if (Input.GetMouseButton(0) && !_isGrounded)
        {
            myRigidBody.angularDrag = 0.5f;
            myRigidBody.AddTorque(Vector3.forward * (Time.deltaTime * rotationSpeed), ForceMode.Acceleration);

        }
        else if (Input.GetMouseButtonUp(0))
        {
            myRigidBody.angularDrag = _defaultAngularDrag;
        }
    }


    #endregion


    #region MOTORCYCLE BOOST & FLIP CALCULATE
    // Motorcycle Speed Booster
    private void MotorcycleIncreaseSpeed(float powerAmount)
    {
        myRigidBody.AddForceAtPosition(transform.forward * (Time.deltaTime * powerAmount), forcePoint.position, ForceMode.Acceleration);
    }

    private void CalculateFlip()
    {
        Vector3 dir = transform.right;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if ((prevAngle > -10.0f && prevAngle < 0.0f) && angle >= 0.0)
        {
            UIManager.instance.FlipPush();

            flips++;
            Debug.Log("FLIPS: " + flips);

        }
        prevAngle = angle;
    }
    #endregion
}
