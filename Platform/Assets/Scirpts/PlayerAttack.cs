using UnityEngine;

public class Attack : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject toShoot;
    private PlayerInput input;
    void Start()
    {
        input = GetComponent<PlayerInput>();
    }

    void Update()
    {
        if (input.attackPressed)
        {
            FireBall fireball = Instantiate(toShoot, spawnPoint.position, Quaternion.identity).GetComponent<FireBall>();
            Vector2 fireballDirection = new(input.horizontalMove, 0);
            if (fireballDirection.x == 0) fireballDirection.x = 1;
            fireball.Shot(fireballDirection);
        }
    }
}
