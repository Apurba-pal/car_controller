using UnityEngine;

public class FollowCar : MonoBehaviour
{
    public Transform CarTransform;
    public Transform CameraPointTransform;
    private Vector3 velocity = Vector3.zero;
    void Start(){

    }
    void FixedUpdate(){
        transform.LookAt(CarTransform);
        // transform.position = CameraPointTransform.position;
        transform.position = Vector3.SmoothDamp(transform.position, CameraPointTransform.position, ref velocity, 5f * Time.deltaTime);               
    }
    
}
