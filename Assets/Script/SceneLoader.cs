using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader {

    public enum Scene
    {
        MainMenueScene,
        GameScene,
    }
    public static void LoadScene(Scene scene){
        SceneManager.LoadScene(scene.ToString()) ;
    }
}
