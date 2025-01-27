using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float damping;
    
    private void LateUpdate() {
        Vector3 desiredPosition = new Vector3(
            target.position.x,
            target.position.y,
            transform.position.z
        );
        
        //lerp used here makes movement of camera more smooth
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime / damping);
    }   
}
