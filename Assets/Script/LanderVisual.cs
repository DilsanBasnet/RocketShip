using UnityEngine;
public class LanderVisual : MonoBehaviour{
    [SerializeField] private ParticleSystem LeftThruster;
    [SerializeField] private ParticleSystem MiddleThruster;
    [SerializeField] private ParticleSystem RightThruster;

    private void Start()
    {
       ParticleSystem.EmissionModule emissionModule = LeftThruster.emission;
       emissionModule.enabled = false;
    }

}
