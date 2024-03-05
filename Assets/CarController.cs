using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float accelerationFactor = 30.0f;
    public float steeringFactor = 3.5f;
    public float driftFactor = 0.95f;

    float accelerationInput = 0;
    float steeringInput = 0;

    float rotationAngle = 0;

    public Rigidbody2D carRigidBody;

    private void Start()
    {
        transform.rotation = Quaternion.Euler(0, 0, -90);
    }

    private void FixedUpdate()
    {
        ApplyEngineForce();

        KillOrthogonalVelocity();

        ApplySteering();
    }

    void ApplyEngineForce()
    {
        if (accelerationInput == 0)
        {
            carRigidBody.drag = 3;
        }
        else
            carRigidBody.drag = 0;

        Vector2 engineForceVector = accelerationFactor * accelerationInput * transform.up;

        carRigidBody.AddForce(engineForceVector, ForceMode2D.Force);
    }

    void ApplySteering()
    {
        float minSpeedBeforeTurningFactor = carRigidBody.velocity.magnitude;
        minSpeedBeforeTurningFactor = Mathf.Clamp01(minSpeedBeforeTurningFactor);

        rotationAngle -= steeringFactor * steeringInput * minSpeedBeforeTurningFactor;

        carRigidBody.MoveRotation(rotationAngle);
    }

    void KillOrthogonalVelocity()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(carRigidBody.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(carRigidBody.velocity, transform.right);

        carRigidBody.velocity = forwardVelocity + rightVelocity * driftFactor;
    }

    public void SetInputVector(Vector2 inputVector)
    {
        steeringInput = inputVector.x;
        accelerationInput = inputVector.y;
    }
}
