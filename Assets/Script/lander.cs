using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class lander : MonoBehaviour{

private const float GRAVITY_NORMAL = 0.7f;
    public static lander Instance {get; private set;} 
    public event EventHandler OnUpForce;
    public event EventHandler OnRightForce;
    public event EventHandler OnLeftForce;
    public event EventHandler OnBeforeForce;
    public event EventHandler OnCoinCollect;
    public event EventHandler OnFuelCollect;
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs: EventArgs  
{
        public State state;
    }
    public event EventHandler<OnLandedEventArgs> OnLanded;
    public class OnLandedEventArgs: EventArgs{
        public landingtype Landingtype;
        public int score;
        public float dotVector;
        public float landingspeed;
        public float scoreMultiplier;
    }
    public enum landingtype{
        success, 
        WrongLandingArea,
        TooSteepAngle,
        TooFastLanging,

    }
    public enum  State
    {
        WatitingToStart, 
        Normal,
        GameOver, 
        
        
    }

    private Rigidbody2D landerRigidbody2D;
    private float fuelAmount;
    private float fuelAmountMax = 10f;
    private State state;


    private void Awake()
 {
Instance = this;

fuelAmount = fuelAmountMax;
state = State.WatitingToStart;
        landerRigidbody2D = GetComponent<Rigidbody2D>();
        landerRigidbody2D.gravityScale = 0f;
    }
    

    private void FixedUpdate()
    {
        OnBeforeForce?.Invoke(this, EventArgs.Empty);

        switch(state){

            default: case State.WatitingToStart: 
            if (GameInput.Instance.IsUpActionPressed() || 
            GameInput.Instance.IsLeftActionPressed()||
                 GameInput.Instance.IsRightActionPressed()
            )
        {
            
            landerRigidbody2D.gravityScale = GRAVITY_NORMAL;
            SetState(State.Normal);
                    }
    
            break;

             case State.Normal: 
             
                    if (fuelAmount <= 0f){
                         return;
        } 
        
       


        if (GameInput.Instance.IsUpActionPressed() || 
            GameInput.Instance.IsLeftActionPressed()||
                 GameInput.Instance.IsRightActionPressed())
                {
                   ConsumeFuel(); 
                }
        


        if (GameInput.Instance.IsUpActionPressed())
        {
            float force = 700f;
            landerRigidbody2D.AddForce(force * transform.up * Time.deltaTime);
            OnUpForce?.Invoke(this, EventArgs.Empty);
        }
        if (GameInput.Instance.IsRightActionPressed())
        {
            float turnSpeed = -100f;
            landerRigidbody2D.AddTorque(turnSpeed * Time.deltaTime);
            OnRightForce?.Invoke(this, EventArgs.Empty);

        }
        if (GameInput.Instance.IsLeftActionPressed())
        {
            float turnSpeed = +100f;
            landerRigidbody2D.AddTorque(turnSpeed * Time.deltaTime);
            OnLeftForce?.Invoke(this, EventArgs.Empty);
        }
           
break;
case State.GameOver: 
break;

        }
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {

        if (!collider.gameObject.TryGetComponent(out landingpad landingpad))
        {
            Debug.Log("Crashed Landed on Terrain");
            OnLanded?.Invoke(this, new OnLandedEventArgs {
                Landingtype = landingtype.WrongLandingArea,
                dotVector = 0f,
                landingspeed = 0f, 
                scoreMultiplier = 0,
                score = 0,
                
            });

           SetState(State.GameOver);
            return;
        }


        float softLandingVelocityMagnitude = 4f;
        float relativeVelocityMagnitude = collider.relativeVelocity.magnitude;

        if (relativeVelocityMagnitude > softLandingVelocityMagnitude)
        {
            Debug.Log("Landed too hard!! crash landing");

            OnLanded?.Invoke(this, new OnLandedEventArgs {
                Landingtype = landingtype.TooFastLanging, 
                dotVector = 0f,
                landingspeed = relativeVelocityMagnitude,
                scoreMultiplier = 0,
                score = 0,
                
            });
            SetState(State.GameOver);
            return;
        }

        float dotVector = Vector2.Dot(Vector2.up, transform.up);
        float minDotVector = .90f;
        if (dotVector < minDotVector){
            Debug.Log("Landed on a too steep angle");
            OnLanded?.Invoke(this, new OnLandedEventArgs {
                Landingtype = landingtype.TooSteepAngle,
                dotVector = dotVector,
                landingspeed = relativeVelocityMagnitude,
                scoreMultiplier = landingpad.GetScoreMultiplier(), score = 0,
            }); SetState(State.GameOver);

        }


        Debug.Log("Soft and Successfull Landing");

        float maxScoreAmoutLandingAngle = 100;
        float scoreDotVectorMultiplier = 10f;
        float landingAngleScore = maxScoreAmoutLandingAngle - Mathf.Abs(dotVector - 1f) * scoreDotVectorMultiplier * maxScoreAmoutLandingAngle;

        float maxScoreAmountLandingSpeed = 100;
        float landingspeedScore = (softLandingVelocityMagnitude - relativeVelocityMagnitude) * maxScoreAmountLandingSpeed;
        Debug.Log("LandingAngleScore: " + landingAngleScore);
        Debug.Log("LandingSpeedScore: " + landingspeedScore);

        int score = Mathf.RoundToInt((landingAngleScore + landingspeedScore) * landingpad.GetScoreMultiplier());

        Debug.Log("Score:" + score);
        OnLanded?.Invoke(this, new OnLandedEventArgs {
            Landingtype  = landingtype.success,
            dotVector = dotVector,
            landingspeed = relativeVelocityMagnitude,
            scoreMultiplier = landingpad.GetScoreMultiplier() ,
            score = score,
            
        });
        SetState(State.GameOver);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out fuel Fuel))
        {
            float addfuel = 10f;
            fuelAmount += addfuel;
            if(fuelAmount > fuelAmountMax)
            {
                fuelAmount = fuelAmountMax;
            }
            OnFuelCollect?.Invoke(this, EventArgs.Empty);
            Fuel.Destoryself();

        }

        if(collider.gameObject.TryGetComponent(out coin Coin)){
            OnCoinCollect?.Invoke(this, EventArgs.Empty);
           Coin.DestroySelf() ;
        }

    }

    private void SetState(State state)
    {
        this.state = state;
        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs{
            state = state
        });
    }
    private void ConsumeFuel()
    {
        float fuelconsume = 1f;
        fuelAmount -= fuelconsume * Time.deltaTime;
    }
    public float GetFuel()
    {
        return fuelAmount;
    }
    public float GetFuelAmountNormalized()
    {
        return fuelAmount / fuelAmountMax;
    }

    public float GetSpeedX()
    {
        return landerRigidbody2D.linearVelocityX;
    }
    public float GetSpeedY()
    {
        return landerRigidbody2D.linearVelocityY;
    }

}
