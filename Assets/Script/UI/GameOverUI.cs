using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private TextMeshProUGUI scoreText; 

    private void Awake()
    {
        mainMenuButton.onClick.AddListener(() =>
        {
            SceneLoader.LoadScene(SceneLoader.Scene.MainMenueScene);
        });
    }

    private void Start()
    {
       scoreText.text = "FINAL SCORE: " + GameManager.Instance.GetTotalScore(). ToString();
       mainMenuButton.Select();
    }
}
