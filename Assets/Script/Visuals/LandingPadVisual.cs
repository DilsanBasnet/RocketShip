using TMPro;
using UnityEngine;

public class LandingPadVisual : MonoBehaviour{
    
    [SerializeField] private TextMeshPro scoreMultiplierTextMesh;
    private void Awake()
    {
      landingpad landingpad =  GetComponent<landingpad>() ;
      scoreMultiplierTextMesh.text = "x" + landingpad.GetScoreMultiplier();
    }
}
