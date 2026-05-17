using UnityEngine;

public class SoundManage : MonoBehaviour
{
    [SerializeField] private AudioClip fuelCollectAudio;
    [SerializeField] private AudioClip coinCollectAudio;
    [SerializeField] private AudioClip crashAudio;
    [SerializeField] private AudioClip landingSuccessAudio;

    private void Start()
    {
        lander.Instance.OnFuelCollect += lander_OnFuelCollect;
        lander.Instance.OnCoinCollect += lander_OnCoinCollect;
        lander.Instance.OnLanded += lander_OnLanded;
    }


    private void lander_OnCoinCollect(object sender, System.EventArgs e){
        AudioSource.PlayClipAtPoint(coinCollectAudio, Camera.main.transform.position);
    }
    private void lander_OnLanded(object sender, lander.OnLandedEventArgs e){
        switch(e.Landingtype) {
            case lander.landingtype.success: 
            AudioSource.PlayClipAtPoint(landingSuccessAudio, Camera.main.transform.position);
            break;
            default: 
            AudioSource.PlayClipAtPoint(crashAudio, Camera.main.transform.position);
            break;
        }
    }

    private void lander_OnFuelCollect(object sender, System.EventArgs e){
        AudioSource.PlayClipAtPoint(fuelCollectAudio, Camera.main.transform.position);
    }

}


