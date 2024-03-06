using UnityEngine;

public class CarController : MonoBehaviour
{
    public float accelerationFactor;
    public float steeringFactor;

    float accelerationInput = 0;
    float steeringInput = 0;

    float rotationAngle = 0;

    public Rigidbody2D carRigidBody;

    private void Start()
    {
        accelerationFactor = 5f;
        steeringFactor = 3.5f;
    }

    private void FixedUpdate()
    {
        ApplyEngineForce();

        ApplySteering();
    }

    void ApplyEngineForce()
    {
        carRigidBody.velocity = transform.up * accelerationFactor * accelerationInput;
    }

    void ApplySteering()
    {
        rotationAngle -= steeringFactor * steeringInput;

        carRigidBody.MoveRotation(rotationAngle);
    }

    public void SetInputVector(Vector2 inputVector)
    {
        steeringInput = inputVector.x;
        accelerationInput = inputVector.y;
    }
}
