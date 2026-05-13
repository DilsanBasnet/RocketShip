using UnityEngine;
public class LanderVisual : MonoBehaviour{
    [SerializeField] private ParticleSystem LeftThruster;
    [SerializeField] private ParticleSystem MiddleThruster;
    [SerializeField] private ParticleSystem RightThruster;
private lander lander;

private void Awake()  {
        lander = GetComponent<lander>();
     lander.onUpForce += Lander_onUpForce;
    lander.onLeftForce += Lander_onLeftForce;
    lander.onRightForce += Lander_onRightForce;
    lander.onBeforeForce += Lander_onBeforeForce;

     SetEnabledThruster(RightThruster, false);
SetEnabledThruster(MiddleThruster, false);
     SetEnabledThruster(LeftThruster, false);
}

private void  Lander_onBeforeForce(object sender, System.EventArgs e){
    
             SetEnabledThruster(RightThruster, false);
SetEnabledThruster(MiddleThruster, false);
     SetEnabledThruster(LeftThruster, false);
    }
    private void  Lander_onUpForce(object sender, System.EventArgs e){
      SetEnabledThruster(LeftThruster, true);
        SetEnabledThruster(MiddleThruster, true);
SetEnabledThruster(RightThruster, true);
    }
    private void  Lander_onLeftForce(object sender, System.EventArgs e){
 SetEnabledThruster(LeftThruster, true);
        SetEnabledThruster(MiddleThruster, false);
     SetEnabledThruster(RightThruster, true);
    }
    private void  Lander_onRightForce(object sender, System.EventArgs e){
    SetEnabledThruster(LeftThruster, true);
     SetEnabledThruster(MiddleThruster, false);
SetEnabledThruster(RightThruster, true);
    }
    private void SetEnabledThruster(ParticleSystem particlesystem, bool enabled) {
       ParticleSystem.EmissionModule emissionModule = LeftThruster.emission;
       emissionModule.enabled = enabled;
    }

}
