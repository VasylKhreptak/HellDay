using Cinemachine;
using UnityEngine;

public class CinemachineCameraStateController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;

    private void OnEnable()
    {
        UI_GameOverSignAnimation.onShow += () => { SetCameraState(false); };
        Player.onResurrection += () => { SetCameraState(true); };
    }

    private void OnDisable()
    {
        UI_GameOverSignAnimation.onShow -= () => { SetCameraState(false); };
        Player.onResurrection -= () => { SetCameraState(true); };
    }

    private void SetCameraState(bool state)
    {
        _virtualCamera.enabled = state;
    }
}