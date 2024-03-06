using UnityEngine;

public class CarController : MonoBehaviour
{
    public float accelerationFactor;
    public float steeringFactor;
    public float driftFactor;

    float accelerationInput = 0;
    float steeringInput = 0;

    float rotationAngle = 0;

    public Rigidbody2D carRigidBody;

    private void Start()
    {
        accelerationFactor = 5.0f;
        steeringFactor = 3.5f;
        driftFactor = 0.3f;
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

        //Vector2 engineForceVector = transform.up;
        //carRigidBody.AddForce(engineForceVector, ForceMode2D.Force);
        
        carRigidBody.velocity = transform.up * accelerationFactor * accelerationInput;

    }

    void ApplySteering()
    {
        rotationAngle -= steeringFactor * steeringInput;

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
