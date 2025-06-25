using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private Vector2 minMaxXY;
    
    private void LateUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("No target assigned to CameraController. Please assign a target.");
            
            return;
        }
        
        Vector3 targetPosition = target.position;
        targetPosition.z = -10f;
        
        targetPosition.x = Mathf.Clamp(targetPosition.x, -minMaxXY.y, minMaxXY.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, -minMaxXY.y, minMaxXY.y);
        
        transform.position = targetPosition;
    }
}
