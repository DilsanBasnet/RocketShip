using Unity.Cinemachine;
using UnityEngine;
 public class CinemachineCameraZoom2D : MonoBehaviour
{
    
    public static CinemachineCameraZoom2D Instance {get; private set;}
    [SerializeField] private CinemachineCamera cinemachineCamera;
    private float targetOrthographic =10f;
    private const float Normal_Orthographic_Size = 10f;

    private void Awake()
    {
        Instance = this;
    }
    private void Update(){
        float zoomSpeed = 2f;
        cinemachineCamera.Lens.OrthographicSize =Mathf.Lerp(cinemachineCamera.Lens.OrthographicSize, targetOrthographic, Time.deltaTime* zoomSpeed);
    }
    public void SetTarge(float targetOrthographic) {
        this.targetOrthographic = targetOrthographic;
        
    }

    public void setNormalOrthographic()
    {
        SetTarge(Normal_Orthographic_Size);
    }
}

