using UnityEngine;

public class GameLevel : MonoBehaviour
{
   [SerializeField] private int levelNumber;
   [SerializeField] private Transform landerStartPositionTransform;
   [SerializeField] private Transform cameraStartTransform;
   [SerializeField] private float zoomedOutOrthographic;

   public int GetLevelNumber()
    {
        return levelNumber;
 
    }

    public Vector3 GetLanderStartPosition()
    {
        return landerStartPositionTransform.position;
    }

public Transform getCameraStartTransform()
    {
        return cameraStartTransform;
    }
    public float GetZoomedOutOrthographic()
    {
        return zoomedOutOrthographic;
    }
}
