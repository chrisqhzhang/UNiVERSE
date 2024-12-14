using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Tooltip("An array of transforms representing camera positions")]
    [SerializeField] Transform[] povs;
    [Tooltip("The speed at which the camera follows the player")]
    [SerializeField] float speed;
    
    private int index = 0;
    private Vector3 target;
    
    // Update is called once per frame
    private void Update()
    {
        // Numbers 0-3 represent different povs; Set target to the relevant POV
        /*foreach (Transform pov in povs)
            switch (index)
            {
                case 0:
                    target = povs[0].position;
                    break;
                case 1:
                    target = povs[1].position;
                    break;
                case 2:
                    target = povs[2].position;
                    break;
            }

        if (Input.GetKeyDown(KeyCode.C)) index++;
        else if (index > 2) index = 0;*/
        
        target = povs[index].position;
    }

    private void FixedUpdate()
    {
        // Move camera to desired position/orientation; Must be in FixedUpdate to avoid camera jitters
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
        transform.forward = povs[index].forward;
    }
}
