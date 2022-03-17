using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    [SerializeField] float controlSpeed = 5f;
    [SerializeField] float xRange = 5f;
    [SerializeField] float yRange = 5f;
    [SerializeField] float yRangeTop = 10f;

    [SerializeField] float pitchValue = -2f;
    [SerializeField] float controlpitchValue = -10f;
    [SerializeField] float yawValue = -5f;
    [SerializeField] float rollValue = 5f;


    float xThrow, yThrow;

    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
    }


    void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * pitchValue;
        float pitchDueToControlThrow = yThrow * controlpitchValue;

        //more comfortable to rotate player
        float pitch = pitchDueToPosition + pitchDueToControlThrow;

        float yaw = transform.localPosition.x * yawValue;
        float roll = xThrow * rollValue;

        
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void ProcessTranslation()
    {
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");


        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXpos = Mathf.Clamp(rawXPos, -xRange, xRange); //clamped for space cant out of range camera in x

        float yOffset = yThrow * Time.deltaTime * controlSpeed;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYpos = Mathf.Clamp(rawYPos, -yRange, yRangeTop); //clamped for space cant out of range camera in y


        transform.localPosition = new Vector3(clampedXpos, clampedYpos, transform.localPosition.z);
    }
}
