using System.Collections;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float accelerationFactor;
    public float steeringFactor;
    public float boosterPower;

    float accelerationInput = 0;
    float steeringInput = 0;
    float rotationAngle = 0;

    public bool boosterFuel;

    public Rigidbody2D carRigidBody;

    private void Start()
    {
        accelerationFactor = 5f;
        steeringFactor = 3.5f;
        boosterPower = 0;
        boosterFuel = false;
    }

    private void FixedUpdate()
    {
        ApplyEngineForce();

        ApplySteering();
    }

    void ApplyEngineForce()
    {
        carRigidBody.velocity = transform.up * accelerationFactor * accelerationInput + transform.up * boosterPower;
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

    public void SetBoosterFuel(bool value)
    {
        boosterFuel = value;
    }

    public void ApplyBooster()
    {
        boosterFuel = false;
        StartCoroutine(BoosterFuel());
    }

    public IEnumerator BoosterFuel()
    {
        boosterPower = 3;
        yield return new WaitForSeconds(1.5f);
        boosterPower = 0;
    }
}
