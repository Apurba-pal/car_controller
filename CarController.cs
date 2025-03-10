using UnityEngine;

public class CarController : MonoBehaviour
{
    // we can use rigidbody.addforce
    // or transform.translate
    // WheelCollider.motorTorque
    // WheelCollider.getWorldPose ... to get the position of the wheel collider 

    [SerializeField] WheelCollider frontRightWheelCollider;
    [SerializeField] WheelCollider frontLeftWheelCollider;
    [SerializeField] WheelCollider rearRightWheelCollider;
    [SerializeField] WheelCollider rearLeftWheelCollider;

    [SerializeField] Transform frontRightWheelTransform;
    [SerializeField] Transform frontLeftWheelTransform;
    [SerializeField] Transform rearRightWheelTransform;
    [SerializeField] Transform rearLeftWheelTransform;

    [SerializeField] Transform CarCentreOfMassTransform;
    [SerializeField] Rigidbody carRigidbody;

    [SerializeField] float motorForce = 300f;
    [SerializeField] float steeringAngle = 30f;
    [SerializeField] float BrakeForce = 3000f;

    float horizontalInput;
    float verticalInput;


    void Start(){
    carRigidbody = GetComponent<Rigidbody>(); // Ensure it's assigned
    carRigidbody.centerOfMass = CarCentreOfMassTransform.localPosition;
}


    void FixedUpdate(){
        MotorForce();
        UpdateWheels();
        getInput();
        Steering();
        applyBrakes();
    }

    void getInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");  // horizontal input like A, D , left arrow , right arrow
        verticalInput = Input.GetAxis("Vertical");    // for vertical inputs like W, S , up arrow , down arrow 
    }


    void applyBrakes(){
        if(Input.GetKey(KeyCode.Space)){
            frontRightWheelCollider.brakeTorque = BrakeForce;
            frontLeftWheelCollider.brakeTorque = BrakeForce;
            rearRightWheelCollider.brakeTorque = BrakeForce;
            rearLeftWheelCollider.brakeTorque = BrakeForce;
        }
        else{
            frontRightWheelCollider.brakeTorque = 0f;
            frontLeftWheelCollider.brakeTorque = 0f;
            rearRightWheelCollider.brakeTorque = 0f;
            rearLeftWheelCollider.brakeTorque = 0f;
        }
    }


    void MotorForce(){
        frontRightWheelCollider.motorTorque = motorForce * verticalInput;
        frontLeftWheelCollider.motorTorque = motorForce * verticalInput;
    }


    void Steering(){
        frontRightWheelCollider.steerAngle = steeringAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = steeringAngle * horizontalInput;
    }


    void UpdateWheels(){
        RotateWheel(frontRightWheelCollider, frontRightWheelTransform);
        RotateWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        RotateWheel(rearRightWheelCollider, rearRightWheelTransform);
        RotateWheel(rearLeftWheelCollider, rearLeftWheelTransform);
    }


    void RotateWheel(WheelCollider wheelCollider, Transform transform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        transform.position = pos;
        transform.rotation = rot;
    }
}
