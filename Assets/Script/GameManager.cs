using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}
    private int score;
    private float time;

private void Awake()
    {
        Instance = this;
    }
    private void Start(){
        lander.Instance.OnCoinCollect += Lander_OnCoinCollect;
        lander.Instance.OnLanded += Lander_OnLanded;
    }

    private void Update()
    {
        time += Time.deltaTime;
    }
private void Lander_OnLanded(object sender, lander.OnLandedEventArgs e)
    {
        AddScore(e.score);
    }
    private void Lander_OnCoinCollect(object sender, System.EventArgs e)
    {
        AddScore(500);
    
    }

    public void AddScore(int addScore)
    {
        score += addScore;
Debug.Log(score);
    }
    public int GetScore(){
    return score;
}

public float GetTime()
    {
        return time;
    }
}


