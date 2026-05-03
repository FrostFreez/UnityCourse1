using UnityEngine;

public class EntityController : MonoBehaviour
{
    public StateMachine sm = new();
    public Rigidbody2D rb;
    public Animator anim;
    public MovementHandler mh;
    public SpriteRenderer sr;

    public virtual void Update()
    {
        sm.state.Update();
    }
    public void FixedUpdate()
    {
        sm.state.PhysicsUpdate();
    }
}
