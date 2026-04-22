using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] public Rigidbody2D rb;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private BallSO sO;
    [SerializeField] private PlayerInputBehaivour input;
    public StatusBehaivour status; 
    private bool lastInput = false;
    public delegate void BallDeathHandler();
    public BallDeathHandler ballDeath;
    void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        status = GetComponent<StatusBehaivour>();
    }

    public void DefineBall(BallSO sO, PlayerInputBehaivour input)
    {
        this.sO = sO;
        this.input = input;
        sr.sprite = sO.sprite;
        rb.gravityScale = sO[StatusType.Gravity];
        status.baseStatus = sO.baseStatus;
    }

    private void Update()
    {
        if (lastInput && !input.attackPressed)
        {
            sO.Released(this);
        }
        else if (!lastInput && input.attackPressed)
        {
            sO.Pressed(this);
        }
        lastInput = input.attackPressed;
    }

    public void ApplyForce(Vector2 force)
    {
        rb.linearVelocity = force;
    }

    public void TakeDamage()
    {
        Status life = status.baseStatus.Find(x => x.type == StatusType.HP);
        if (--life.value == 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        ballDeath();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 10);
    }
}
