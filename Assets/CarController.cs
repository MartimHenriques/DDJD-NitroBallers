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

    public bool boosterPowerUp;
    public bool isBoosted;
    public bool sizePowerUp;
    public bool isBig;

    public Rigidbody2D carRigidBody;

    private void Start()
    {
        accelerationFactor = 5f;
        steeringFactor = 3.5f;

        boosterPower = 0;
        boosterPowerUp = false;
        isBoosted = false;
        sizePowerUp = false;
        isBig = false;
    }

    private void FixedUpdate()
    {
        ApplyEngineForce();

        ApplySteering();
    }

    void ApplyEngineForce()
    {
        carRigidBody.velocity = accelerationFactor * accelerationInput * transform.up + transform.up * boosterPower;
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

    public void SetBoosterPowerUp(bool value)
    {
        boosterPowerUp = value;
    }

    public void ApplyBooster()
    {
        boosterPowerUp = false;
        StartCoroutine(BoosterFuel());
    }

    public IEnumerator BoosterFuel()
    {
        isBoosted = true;
        boosterPower = 4;
        steeringFactor += 1;

        yield return new WaitForSeconds(1.5f);

        isBoosted = false;
        boosterPower = 0;
        steeringFactor -= 1;
    }

    public void SetSizePowerUp(bool value)
    {
        sizePowerUp = value;
    }
    public void ApplySizeUp()
    {
        sizePowerUp = false;
        StartCoroutine(SizeUp());
    }

    public IEnumerator SizeUp()
    {
        isBig = true;
        
        float initialScale = transform.localScale.x;
        float targetScale = 1.25f;
        float duration = 0.5f;

        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            float scale = Mathf.Lerp(initialScale, targetScale, elapsedTime / duration);
            transform.localScale = new Vector3(scale, scale, 1);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localScale = new Vector3(targetScale, targetScale, 1);

        yield return new WaitForSeconds(2); 
        
        isBig = false;

        elapsedTime = 0.0f;
        while (elapsedTime < duration)
        {
            float scale = Mathf.Lerp(targetScale, initialScale, elapsedTime / duration);
            transform.localScale = new Vector3(scale, scale, 1);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localScale = new Vector3(initialScale, initialScale, 1);

    }
}
