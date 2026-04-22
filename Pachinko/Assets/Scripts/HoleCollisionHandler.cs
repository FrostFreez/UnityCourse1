using System;
using UnityEngine;

public class HoleCollisionHandler : MonoBehaviour
{
    public delegate void BallCollision(Ball ball);
    public BallCollision ballCollision;
    public int score;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Destroy(collision.gameObject);
            Inventory.points += score;
        }
    }
}
