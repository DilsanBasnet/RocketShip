using UnityEngine;
using UnityEngine.InputSystem;

public class lander : MonoBehaviour{
    private Rigidbody2D landerRigidbody2D;

    private void Awake(){

        landerRigidbody2D = GetComponent<Rigidbody2D>();


    
    }

    private void FixedUpdate(){


        if (Keyboard.current.upArrowKey.isPressed){
            float force = 700f;
            landerRigidbody2D.AddForce(force * transform.up * Time.deltaTime);
        }
        if (Keyboard.current.leftArrowKey.isPressed){
            float turnSpeed = +100f;
            landerRigidbody2D.AddTorque(+turnSpeed * Time.deltaTime);
        }
        if (Keyboard.current.rightArrowKey.isPressed){
            float turnSpeed = -100f;
            landerRigidbody2D.AddTorque(turnSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision){

      if (!collision.gameObject.TryGetComponent(out landingpad landingpad)) {
            Debug.Log("Crashed Landed on Terrain");
            return;
        }


        float softLandingVelocityMagnitude = 4f;
float relativeVelocityMagnitude = collision.relativeVelocity.magnitude;

        if (relativeVelocityMagnitude > softLandingVelocityMagnitude)
        {
            Debug.Log("Landed too hard!! crash landing");
            return;
        }

        float dotVector = Vector2.Dot(Vector2.up, transform.up);
        float minDotVector = .90f; 
        if(dotVector < minDotVector){
            Debug.Log("Landed on a too steep angle");
        }


        Debug.Log("Soft and Successfull Landing");

        float maxScoreAmoutLandingAngle = 100;
        float scoreDotVectorMultiplier = 10f;
        float landingAngleScore = maxScoreAmoutLandingAngle - Mathf.Abs(dotVector - 1f) * scoreDotVectorMultiplier * maxScoreAmoutLandingAngle;

        float maxScoreAmountLandingSpeed = 100;
        float landingspeedScore = (softLandingVelocityMagnitude - relativeVelocityMagnitude) * maxScoreAmountLandingSpeed;
       Debug.Log("LandingAngleScore: " + landingAngleScore);
       Debug.Log("LandingSpeedScore: " + landingspeedScore);
    }
}
