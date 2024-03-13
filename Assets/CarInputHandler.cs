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

        if (Input.GetKeyDown(KeyCode.Space) && carController.boosterFuel)
        {
            Debug.Log("Space key was pressed.");
            carController.ApplyBooster();
        }
    }
}
