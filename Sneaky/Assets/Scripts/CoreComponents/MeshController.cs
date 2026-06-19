using UnityEngine;

public class MeshController : CoreComponent
{
    public Transform mesh;
    public Animator anim;

    public override void StartComponent()
    {
        base.StartComponent();
        anim = GetComponent<Animator>();
        controller.anim = anim;
    }
}
