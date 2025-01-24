using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float damping;

    private Vector3 velocity = Vector3.zero;
    
    private void LateUpdate() {
        Vector3 desiredPosition = new Vector3(
            target.position.x + offset.x,
            target.position.y + offset.y,
            transform.position.z
        );
        
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime / damping);
    }   
}
