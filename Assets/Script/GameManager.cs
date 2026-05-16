using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}

   private static int levelNumber = 1 ;
    [SerializeField] private List<GameLevel> gamelevelList;
    [SerializeField] private CinemachineCamera cinemachineCamera;
    private int score;
    private float time;
    private bool isTimerActive;

private void Awake()
    {
        Instance = this;
    }
    private void Start(){
        lander.Instance.OnCoinCollect += Lander_OnCoinCollect;
        lander.Instance.OnLanded += Lander_OnLanded;
        lander.Instance.OnStateChanged += lander_OnStateChanged;
        LoadCurrentLevel();
    }

    private void lander_OnStateChanged(object sender, lander.OnStateChangedEventArgs e)
    {
        isTimerActive = e.state == lander.State.Normal;

        if(e.state == lander.State.Normal)
        {
            cinemachineCamera.Target.TrackingTarget = lander.Instance.transform;
            CinemachineCameraZoom2D.Instance.setNormalOrthographic();
        }
    }

    private void Update()
 {
    if(isTimerActive)
        {
          time += Time.deltaTime;  
        }
    }

    private void LoadCurrentLevel()
    {
        // game level for😥
        foreach(GameLevel gameLevel in gamelevelList)
        {
            if(gameLevel.GetLevelNumber() == levelNumber)
            {
              GameLevel spawnedGameLevel =  Instantiate(gameLevel, Vector3.zero, Quaternion.identity);
              spawnedGameLevel.GetLanderStartPosition();
              lander.Instance.transform.position = spawnedGameLevel.GetLanderStartPosition();
              cinemachineCamera.Target.TrackingTarget = spawnedGameLevel.getCameraStartTransform();
              CinemachineCameraZoom2D.Instance.SetTarge(spawnedGameLevel.GetZoomedOutOrthographic());
            }
        }
        
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
    public void GoToNextLevel() {
        levelNumber++;
        SceneManager.LoadScene(0);
        
    }
    public void RetryLevel(){
        SceneManager.LoadScene(0);
    }
    public int GetLevelNumber()
    {
        return levelNumber;
    }
}


