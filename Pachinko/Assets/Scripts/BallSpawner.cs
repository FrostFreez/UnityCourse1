using UnityEngine;
using System.Collections.Generic;
using static StatusType;

public class BallSpawner : MonoBehaviour
{
    private PlayerInputBehaivour input;
    public List<BallSO> ballSOs = new();
    public BallSO nextBall;
    public bool ready = true;
    private bool lastFrameInput = false;
    private StatusBehaivour status;
    public Vector2 totalForce;

    public Inventory inventory;

    public GameObject ballPrefab;

    public delegate void NewBallSo();
    public NewBallSo newBallD;
    public delegate void NewBallSpawned(Ball ball);
    public NewBallSpawned newBallSpawned;

    private void Start()
    {
        input = GetComponent<PlayerInputBehaivour>();
        status = GetComponent<StatusBehaivour>();

        ballSOs = new();
        for (int i = 0; i < inventory.balls.Count; ++i)
        {
            ballSOs.Add(inventory.balls[i]);
        }
        nextBall = ballSOs[0];
        UpdateStatus(nextBall);
    }

    public void Update()
    {
        totalForce = input.moveDirection * status[Force];
        if (lastFrameInput && ready && !input.attackPressed)
        {
            Ball newBall = Instantiate(ballPrefab, transform.position, Quaternion.identity).GetComponent<Ball>();
            ready = false;
            newBall.DefineBall(nextBall, input);
            if (ballSOs.Count > 0)
            {
                nextBall = ballSOs[0];
                UpdateStatus(nextBall);
                ballSOs.RemoveAt(0);
            }
            else
            {
                nextBall = null;
            }
            newBall.ApplyForce(totalForce);
            newBallSpawned(newBall);
            newBall.ballDeath += GetReady;
        }

        lastFrameInput = input.attackPressed;
    }

    private void UpdateStatus(BallSO sO)
    {
        status.baseStatus = sO.baseStatus;
    }

    public void GetReady()
    {
        if (nextBall)
        {
            ready = true;
            newBallD();
        }
    }
}
