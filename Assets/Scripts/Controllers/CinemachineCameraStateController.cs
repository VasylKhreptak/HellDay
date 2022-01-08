using Cinemachine;
using UnityEngine;

public class CinemachineCameraStateController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;

    private void OnEnable()
    {
        UI_GameOverSignAnimation.onBeingShowm += DisableCamera;
        Player.onResurrection += EnableCamera;
    }

    private void OnDisable()
    {
        UI_GameOverSignAnimation.onBeingShowm -= DisableCamera;
        Player.onResurrection -= EnableCamera;
    }

    private void EnableCamera()
    {
        _virtualCamera.enabled = true;
    }

    private void DisableCamera()
    {
        _virtualCamera.enabled = false;
    }
}
