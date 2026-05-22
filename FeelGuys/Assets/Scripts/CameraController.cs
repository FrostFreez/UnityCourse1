using Unity.Cinemachine;
using UnityEngine;
public class CameraController : CoreComponent
{
    private PlayerInput input;
    [SerializeField] private Transform cameraAnchorHorizontal;
    [SerializeField] private float cameraRotationX;
    [SerializeField] private float cameraRotationY;
    [SerializeField] private float sensibility;
    [SerializeField] private bool invertY;
    [SerializeField] private Transform cameraAnchor;
    public override void StartComponent()
    {
        input = controller.FindComponent<PlayerInput>();
        GameController.instance.transform.GetChild(0).GetComponent<CinemachineCamera>().Follow = cameraAnchor;

        Cursor.lockState = CursorLockMode.Locked;
    }
    public override void UpdateComponent()
    {
        cameraRotationY += input.lookRotation.x * sensibility;
        cameraRotationX -= input.lookRotation.y * (invertY ? -1 : 1) * sensibility;

        cameraRotationX = Mathf.Clamp(cameraRotationX, -90, 90);

        transform.rotation = Quaternion.Euler(new(0, cameraRotationY, 0));
        cameraAnchorHorizontal.rotation = Quaternion.Euler(new(cameraRotationX, cameraRotationY, 0));
    }
}
