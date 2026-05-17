using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
  
  [SerializeField] private Button playButton;
  [SerializeField] private Button quitButton;
    private void Awake()
    {
        Time.timeScale = 1f;
        playButton.onClick.AddListener(()=> {
            GameManager.ResetStatic ();
            SceneLoader.LoadScene(SceneLoader.Scene.GameScene);
            
        });

        quitButton.onClick.AddListener(() =>
        {
            Application.Quit()    ;
 });
    }
}
