using UnityEngine;

public class GameManager : MonoBehaviour
{
[SerializeField] private lander Lander;
    private int score;

    private void Start()
    {
        Lander.OnCoinCollect += Lander_OnCoinCollect;
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
}
