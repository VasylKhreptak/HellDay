using Cinemachine;
using UnityEngine;

public class CinemachineCameraStateController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;

    private void OnEnable()
    {
        UI_GameOverSign.onShow += DisableCameraMovement;
        Player.onResurrection += EnableCameraMovement;
        UI_LevelCompleteSign.onShow += DisableCameraMovement;
    }

    private void OnDisable()
    {
        UI_GameOverSign.onShow -= DisableCameraMovement;
        Player.onResurrection -= EnableCameraMovement;
        UI_LevelCompleteSign.onShow -= DisableCameraMovement;
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