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
        Debug.Log("oncollision 2d enter");
    }
}
