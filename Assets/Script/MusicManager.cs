using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static float musicTime;
  private AudioSource musicAudio;
  

    private void Awake(){
        musicAudio = GetComponent<AudioSource>();
        musicAudio.time = musicTime;
    }

    private void Update(){
       musicTime =  musicAudio.time;
        
    }
}
