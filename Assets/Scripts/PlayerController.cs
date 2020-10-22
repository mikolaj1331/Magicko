using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeedMultiplier;
    [SerializeField] float moveSpeed;

    void Update()
    {
        // Getting input values
        float xPosInput = CrossPlatformInputManager.GetAxis("Horizontal");
        float zPosInput = CrossPlatformInputManager.GetAxis("Vertical");

        //Calculating offset
        float xOffset = xPosInput * moveSpeedMultiplier * Time.deltaTime;
        float zOffset = zPosInput * moveSpeedMultiplier * Time.deltaTime;

        moveSpeed = Mathf.Max(Mathf.Abs(xOffset * moveSpeedMultiplier), Mathf.Abs(zOffset * moveSpeedMultiplier));

        //Calculating location we end up
        float positionX = transform.localPosition.x + xOffset;
        float positionZ = transform.localPosition.z + zOffset;

        //Moving
        transform.localPosition = new Vector3 (positionX,transform.localPosition.y,positionZ);
    }    
}
