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

    [SerializeField]
    private GameObject[] ObjectsToDisable;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _controller = GetComponent<ThirdPersonController>();
        _inputs = GetComponent<StarterAssetsInputs>();
        _playerInput = GetComponent<PlayerInput>();

        _characterController.enabled = false;
        _controller.enabled = false;
        _inputs.enabled = false;
        _playerInput.enabled = false;

        foreach (var obj in ObjectsToDisable)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }
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

            foreach (var obj in ObjectsToDisable)
            {
                if (obj != null)
                {
                    obj.SetActive(true);
                }
            }
        }
    }
}
