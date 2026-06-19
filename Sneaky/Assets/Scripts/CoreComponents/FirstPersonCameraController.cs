using UnityEngine;
public class FirstPersonCameraController : CoreComponent
{
    [SerializeField] private InputVector2 look;
    private float cameraRotationX;
    private float cameraRotationY;
    [SerializeField] private float sensibility;
    [SerializeField] private bool invertY;
    public override void StartComponent()
    {
        look = controller.FindCore<PlayerInput>().GetInputItem<InputVector2>("look");

        Cursor.lockState = CursorLockMode.Locked;
    }
    public override void UpdateComponent()
    {
        cameraRotationY += look.vector.x * sensibility;
        cameraRotationX -= look.vector.y * (invertY ? -1 : 1) * sensibility;

        cameraRotationX = Mathf.Clamp(cameraRotationX, -90, 90);

        controller.transform.rotation = Quaternion.Euler(new(0, cameraRotationY, 0));
        transform.rotation = Quaternion.Euler(new(cameraRotationX, cameraRotationY, 0));
    }
}
