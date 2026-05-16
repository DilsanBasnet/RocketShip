using System;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;


public class StatsUi : MonoBehaviour
{
[SerializeField] private TextMeshProUGUI statsTextMesh;
[SerializeField] private GameObject SpeedUpArrow;
[SerializeField] private GameObject SpeedDownArrow;
[SerializeField] private GameObject SpeedLeftArrow;
[SerializeField] private GameObject SpeedRightArrow;
[SerializeField] private UnityEngine.UI.Image fuelImage;






private void Update()
    {
        UpdateStatsTextMesh();
    }
    private void UpdateStatsTextMesh()
    {

        SpeedUpArrow.SetActive(lander.Instance.GetSpeedY() >= 0);
        SpeedDownArrow.SetActive(lander.Instance.GetSpeedX() < 0);
        SpeedLeftArrow.SetActive(lander.Instance.GetSpeedX() < 0);
        SpeedRightArrow.SetActive(lander.Instance.GetSpeedX() >= 0);

fuelImage.fillAmount  = lander.Instance.GetFuelAmountNormalized();
        statsTextMesh.text=
        GameManager.Instance.GetLevelNumber() + "\n" +
        GameManager.Instance.GetScore() + "\n" +
       Mathf.Round( GameManager.Instance.GetTime()) + "\n" +
        Math.Abs(Mathf.Round(lander.Instance.GetSpeedX() * 10f)) + "\n" +
        math.abs(Mathf.Round(lander.Instance.GetSpeedY() * 10f));
         
    }

}
