using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        EntityController controller = other.gameObject.GetComponentInParent<EntityController>();
        if (controller == null) return;
        Killable killableEntity = controller.FindComponent<Killable>();
        if (killableEntity != null)
        {
            killableEntity.Kill();
        }
    }
}
