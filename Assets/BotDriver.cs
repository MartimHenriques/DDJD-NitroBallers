using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BotDriver : MonoBehaviour
{
    [SerializeField] private Transform targetPositionTransform;

    public CarController carController;
    private Vector3 targetPosition;

    private void Awake()
    {
        carController = GetComponent<CarController>();
    }

    void Update()
    {
        targetPosition = targetPositionTransform.position;

        float acceleration;
        float steering;

        float reachedTargetDistance = 0.5f;
        float distanceToTarget = Vector3.Distance(transform.position, targetPosition);

        if (distanceToTarget > reachedTargetDistance) {

            Vector3 dirToMove = (targetPosition - transform.position).normalized;
            float dot = Vector3.Dot(transform.up, dirToMove);

            if (dot > 0){
                acceleration = 1;
            }
            else{
                acceleration = -1;
            }
            
            float cross = Vector3.Cross(transform.up, dirToMove).z;
            if (cross < 0){
                if(Mathf.Abs(cross) < 0.2)
                    steering = 0.25f; 
                else
                    steering = 1;
            }
            else{
                if(Mathf.Abs(cross) < 0.2)
                    steering = -0.25f; 
                else
                    steering = -1; 
            }
        }
        else{
            acceleration = 0;
            steering = 0;
        }

        carController.SetInputVector(new Vector2(steering, acceleration));


        if (carController.boosterPowerUp && !carController.isBoosted)
        {
            carController.ApplyBooster();
        }
        if (carController.sizePowerUp && !carController.isBig)
        {
            carController.ApplySizeUp();
        }
    }

    public void SetTargetPosition(Vector3 position)
    {
        targetPosition = position;
    }
}
