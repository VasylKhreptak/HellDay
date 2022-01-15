using Cinemachine;
using UnityEngine;

public class CinemachineCameraStateController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;

    private void OnEnable()
    {
        UI_GameOverSignAnimation.onShow += DisableCameraMovement;
        Player.onResurrection += EnableCameraMovement;
    }

    private void OnDisable()
    {
        UI_GameOverSignAnimation.onShow -= DisableCameraMovement;
        Player.onResurrection -= EnableCameraMovement;
    }

    private void EnableCameraMovement()
    {
        SetCameraState(true);
    }

    private void DisableCameraMovement()
    {
        SetCameraState(false);
    }

    private void SetCameraState(bool state)
    {
        _virtualCamera.enabled = state;
    }
}