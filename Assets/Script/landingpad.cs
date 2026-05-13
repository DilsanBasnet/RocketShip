using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
public class landingpad : MonoBehaviour{

    [SerializeField]private int  scoreMultiplier; 
 
 public int GetScoreMultiplier()
    {
        return scoreMultiplier;
    }
}
