using UnityEngine;

public class Rotating : MonoBehaviour
{
    [SerializeField] private float speed;

    private void Update()
    {
        transform.eulerAngles += speed * Time.deltaTime * Vector3.forward;
    }
}
