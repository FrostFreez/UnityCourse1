using UnityEngine;

public class Pistols : MonoBehaviour
{
    private StatusBehaivour status;
    public Transform[] pistols;
    public Ball ball;
    [SerializeField] private float cooldown;
    [SerializeField] private float timer;
    private void Start()
    {
        timer = cooldown;
        status = GetComponent<StatusBehaivour>();
    }
    void Update()
    {
        if (ball != null)
        {
            for (int i = 0; i < pistols.Length; i++)
            {
                Vector2 direction = ball.transform.position - pistols[i].position;
                float angle = Mathf.Atan(direction.y / direction.x) * Mathf.Rad2Deg;
                if (angle < 0) { angle = 180 + angle; }
                pistols[i].eulerAngles = new Vector3(0, 0, angle);
            }
            if(timer < 0)
            {
                ball.ApplyForce((ball.transform.position - pistols[Random.Range(0, pistols.Length)].position).normalized * status[StatusType.LauncherSpeed]);
                ball.TakeDamage();
                timer = cooldown;
            }
            timer -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            ball = other.GetComponent<Ball>();
        }
    }
}
