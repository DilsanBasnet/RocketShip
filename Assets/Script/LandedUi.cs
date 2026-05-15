using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class LandedUi : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI BannerText;
   [SerializeField] private TextMeshProUGUI StatsText;

    private void Start() {
        lander.Instance.OnLanded += lander_OnLanded;

        Hide();
    }
    private void lander_OnLanded(object sender, lander.OnLandedEventArgs e)
    {
        if(e.Landingtype == lander.landingtype.success)
        {
            BannerText.text = "SUCCESSFUL LANDING";
        } else
        {
            BannerText.text = "<color=#ff0000>Crash Landing</color> ";
        }

        StatsText.text = 
       Mathf.Round( e.landingspeed * 2f) + "\n" + 
        Mathf.Round(e.dotVector * 100f) + "\n" +
        "x" + e.scoreMultiplier + "\n" +
        e.score;
        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false) ;
    }
}
