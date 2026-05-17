using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance {get; private set;}

   private static int levelNumber = 1 ;
   private static int totalScore = 0;

   public event EventHandler OnGamePaused;
   public event EventHandler OnGameUnpaused;

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
        GameInput.Instance.onMenuButtonPressed += GameInput_onMenuButtonPressed;
        LoadCurrentLevel();
    }
    private void GameInput_onMenuButtonPressed(object sender, System.EventArgs e)
    {
        PauseUnpauseGame() ;
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
    private void LoadCurrentLevel(){
       GameLevel gameLevel = GetGameLevel () ;
       GameLevel spawnedGameLevel =  Instantiate(gameLevel, Vector3.zero, Quaternion.identity);
              spawnedGameLevel.GetLanderStartPosition();
              lander.Instance.transform.position = spawnedGameLevel.GetLanderStartPosition();
              cinemachineCamera.Target.TrackingTarget = spawnedGameLevel.getCameraStartTransform();
              CinemachineCameraZoom2D.Instance.SetTarge(spawnedGameLevel.GetZoomedOutOrthographic());
    }

    private GameLevel  GetGameLevel(){
        foreach(GameLevel gameLevel in gamelevelList)
        {
            if(gameLevel.GetLevelNumber() == levelNumber) {
                return gameLevel;
            }
    }
    return null;
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

    public int GetTotalScore()
    {
        return totalScore;
    }

    public void GoToNextLevel() {
        levelNumber++;
        totalScore  += score;
        if(GetGameLevel() == null)
        {
            SceneLoader.LoadScene(SceneLoader.Scene.GameOverScene);
        }
        else
        {
            SceneLoader.LoadScene(SceneLoader.Scene.GameScene);
        }
        
    }
    public void RetryLevel(){
        SceneLoader.LoadScene(SceneLoader.Scene.GameScene);
    }
    public int GetLevelNumber()
    {
        return levelNumber;
    }

public void PauseUnpauseGame()
    {
        if(Time.timeScale == 1f)
        {
            PauseGame();
        }
        else
        {
            UnpauseGame();
        }
    }
    public void PauseGame() {
        Time.timeScale = 0f;
        OnGamePaused?.Invoke(this, EventArgs.Empty);
    }

    public void UnpauseGame(){
        Time.timeScale = 1f;
        OnGameUnpaused?.Invoke(this, EventArgs.Empty);
    }
}


