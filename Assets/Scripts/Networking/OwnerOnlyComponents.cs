using FishNet.Object;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class OwnerOnlyComponents : NetworkBehaviour
{
    private CharacterController _characterController;
    private ThirdPersonController _controller;
    private StarterAssetsInputs _inputs;
    private PlayerInput _playerInput;
    private Camera _mainCamera;
    private Cinemachine.CinemachineVirtualCamera _cinemachineVirtualCamera;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _controller = GetComponent<ThirdPersonController>();
        _inputs = GetComponent<StarterAssetsInputs>();
        _playerInput = GetComponent<PlayerInput>();
        _mainCamera = GetComponentInChildren<Camera>();
        _cinemachineVirtualCamera = GetComponentInChildren<Cinemachine.CinemachineVirtualCamera>();

        _characterController.enabled = false;
        _controller.enabled = false;
        _inputs.enabled = false;
        _playerInput.enabled = false;
        _mainCamera.enabled = false;
        _mainCamera.gameObject.SetActive(false);
        _cinemachineVirtualCamera.enabled = false;
        _cinemachineVirtualCamera.gameObject.SetActive(false);
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        if (IsOwner)
        {
            _characterController.enabled = true;
            _controller.enabled = true;
            _inputs.enabled = true;
            _playerInput.enabled = true;
            _mainCamera.enabled = true;
            _mainCamera.gameObject.SetActive(true);
            _cinemachineVirtualCamera.enabled = true;
            _cinemachineVirtualCamera.gameObject.SetActive(true);
        }
    }
}
