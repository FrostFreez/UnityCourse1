using System;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private Rigidbody rb;

    [Header(""), SerializeField] private bool position = true;
    [SerializeField] private bool positionRunback = true;
    [SerializeField] private Path[] path = new Path[] {
        new() {position = Vector3.one, curve = AnimationCurve.EaseInOut(0, 0, 1, 1), time = 1 }
    };
    private float positionTotalTime;
    private float timePosition = 0;
    private int timePositionDirection = 1;
    private Vector3 originalPosition = Vector3.zero;

    [Header(""), SerializeField] private bool rotation = true;
    [SerializeField] private bool rotationRunback = true;
    [SerializeField] private Alignment[] alignemnt = new Alignment[] {
        new() {rotation = Vector3.one * 180, curve = AnimationCurve.EaseInOut(0, 0, 1, 1), time = 1 }
    };
    private float rotationTotalTime = 0;
    private float timeRotation = 0;
    private float timeRotationDirection = 1;
    private Vector3 originalRotation = Vector3.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalPosition = transform.position;
        originalRotation = transform.localRotation.eulerAngles;
        foreach (var p in path) positionTotalTime += p.time;
        foreach (var a in alignemnt) rotationTotalTime += a.time;
    }

    void Update()
    {
        if (position)
        {
            timePosition += Time.deltaTime * timePositionDirection;
            if (timePosition > positionTotalTime)
            {
                if (positionRunback)
                {
                    timePositionDirection *= -1;
                    timePosition += Time.deltaTime * timePositionDirection;
                }
                else
                {
                    timePosition -= positionTotalTime;
                    transform.position = originalPosition;
                }
            }
            else if (timePosition < 0)
            {
                timePositionDirection *= -1;
                timePosition += Time.deltaTime * timePositionDirection;
            }

            float currentTimePosition = timePosition;
            int currentPath = 0;

            for (; currentTimePosition > path[currentPath].time; currentTimePosition -= path[currentPath].time, currentPath++) { }

            Vector3 newPosition = Vector3.zero;

            if (currentPath == 0)
            {
                newPosition = Vector3.Lerp(Vector3.zero, path[currentPath].position, path[currentPath].curve.Evaluate(currentTimePosition / path[currentPath].time));
            }
            else
            {
                newPosition = Vector3.Lerp(path[currentPath - 1].position, path[currentPath].position, path[currentPath].curve.Evaluate(currentTimePosition / path[currentPath].time));
            }

            rb.linearVelocity = newPosition - transform.position;
            transform.position = newPosition + originalPosition;
        }

        if (rotation)
        {
            timeRotation += Time.deltaTime * timeRotationDirection;
            if (timeRotation > rotationTotalTime)
            {
                if (rotationRunback)
                {
                    timeRotationDirection *= -1;
                    timeRotation += Time.deltaTime * timeRotationDirection;
                }
                else
                {
                    timeRotation -= rotationTotalTime;
                    transform.rotation = Quaternion.Euler(originalRotation);
                }
            }
            else if (timeRotation < 0)
            {
                timeRotationDirection *= -1;
                timeRotation += Time.deltaTime * timeRotationDirection;
            }

            float currentTimeRotation = timeRotation;
            int currentAlignemnt = 0;

            for (; currentTimeRotation > alignemnt[currentAlignemnt].time; currentTimeRotation -= alignemnt[currentAlignemnt].time, currentAlignemnt++) { }

            Quaternion newRotation = Quaternion.identity;

            if (currentAlignemnt == 0)
            {
                newRotation = Quaternion.Lerp(Quaternion.Euler(originalRotation),
                    Quaternion.Euler(alignemnt[currentAlignemnt].rotation),
                    alignemnt[currentAlignemnt].curve.Evaluate(currentTimeRotation / alignemnt[currentAlignemnt].time));
            }
            else
            {
                newRotation = Quaternion.Lerp(Quaternion.Euler(alignemnt[currentAlignemnt - 1].rotation),
                    Quaternion.Euler(alignemnt[currentAlignemnt].rotation),
                    alignemnt[currentAlignemnt].curve.Evaluate(currentTimeRotation / alignemnt[currentAlignemnt].time));
            }

            rb.angularVelocity = newRotation.eulerAngles;
            transform.localRotation = newRotation;
        }

    }
}

[Serializable]
public struct Path
{
    public Vector3 position;
    public AnimationCurve curve;
    public float time;
}

[Serializable]
public struct Alignment
{
    public Vector3 rotation;
    public AnimationCurve curve;
    public float time;
}