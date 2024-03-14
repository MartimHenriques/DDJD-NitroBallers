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

        if (!PauseMenu.isPaused)
        {
            if (Input.GetKeyDown(KeyCode.Space) && carController.boosterPowerUp && !carController.isBoosted)
            {
                carController.ApplyBooster();
            }

            if (Input.GetKeyDown(KeyCode.U) && carController.sizePowerUp && !carController.isBig)
            {
                carController.ApplySizeUp();
            }
        }
    }
}
