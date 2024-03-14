using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInputHandler : MonoBehaviour
{
    public CarController carController;

    // Update is called once per frame
    void Update()
    {
        Vector2 inputVector = Vector2.zero;

        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.y = Input.GetAxis("Vertical");

        carController.SetInputVector(inputVector);

        if (Input.GetKeyDown(KeyCode.Space) && carController.boosterPowerUp && !carController.isBoosted)
        {
            Debug.Log("Space key was pressed.");
            carController.ApplyBooster();
        }

        if (Input.GetKeyDown(KeyCode.U) && carController.sizePowerUp && !carController.isBig)
        {
            Debug.Log("U key was pressed.");
            carController.ApplySizeUp();
        }
    }
}
