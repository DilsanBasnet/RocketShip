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
        Debug.Log(collision.relativeVelocity.magnitude);
        float softLandingVelocityMagnitude = 4f;
        if(collision.relativeVelocity.magnitude > softLandingVelocityMagnitude)
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
    }
}
