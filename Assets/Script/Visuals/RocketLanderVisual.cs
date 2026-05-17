using UnityEngine;
public class RocketLanderVisual : MonoBehaviour{
    [SerializeField] private ParticleSystem LeftThruster;
    [SerializeField] private ParticleSystem MiddleThruster;
    [SerializeField] private ParticleSystem RightThruster;
    [SerializeField] private GameObject landerExplosion;
private lander Lander;

private void Awake()  {


        Lander = GetComponent<lander>();
     Lander.OnUpForce += Lander_onUpForce;
Lander.OnLeftForce += Lander_onLeftForce;
    Lander.OnRightForce += Lander_onRightForce;
   Lander.OnBeforeForce += Lander_onBeforeForce;

     SetEnabledThruster(RightThruster, false);
SetEnabledThruster(MiddleThruster, false);
     SetEnabledThruster(LeftThruster, false);
}

    private void Start()
    {
        Lander.OnLanded += lander_OnLanded;
    }

    private void lander_OnLanded(object sender, lander.OnLandedEventArgs e)
    {
        switch (e.Landingtype)
        {
            case lander.landingtype.TooFastLanging: 
            case lander.landingtype.TooSteepAngle:
            case lander.landingtype.WrongLandingArea: 
            Instantiate(landerExplosion, transform.position, Quaternion.identity) ;
            gameObject.SetActive(false);
            break;
            
        }
    }

    private void  Lander_onBeforeForce(object sender, System.EventArgs e){
    
             SetEnabledThruster(RightThruster, false);
SetEnabledThruster(MiddleThruster, false);
     SetEnabledThruster(LeftThruster, false);
    }

    

    private void  Lander_onLeftForce(object sender, System.EventArgs e){
     SetEnabledThruster(RightThruster, true);
     SetEnabledThruster(MiddleThruster, true);

    }


    private void  Lander_onRightForce(object sender, System.EventArgs e){
    SetEnabledThruster(LeftThruster, true);
    SetEnabledThruster(MiddleThruster, true);
    }
    private void  Lander_onUpForce(object sender, System.EventArgs e){
    SetEnabledThruster(LeftThruster, true);
        SetEnabledThruster(MiddleThruster, true);
SetEnabledThruster(RightThruster, true);
    }
    private void SetEnabledThruster(ParticleSystem particlesystem, bool enabled) {
       ParticleSystem.EmissionModule emissionModule = particlesystem.emission;
       emissionModule.enabled = enabled;
    }

}
