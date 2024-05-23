using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [Header("Configuration")]
    [Range(0f, 20f)]
    [SerializeField] private float positionSpeed;
    [SerializeField] private List<Vector3> movePosition;
    [Range(0f, 20f)]
    [SerializeField] private float rotationSpeed;
    [SerializeField] private List<Quaternion> moveRotation;
    [Range(0f, 10f)]
    [SerializeField] private float timeDelay;

    Vector3 destination;
    private int currentIndex = 0;
    private int state = 0;

    void Start()
    {
        destination = movePosition[currentIndex];
    }

    void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, destination) >= 0.01f && state == 0)
        {
            Vector3 move = (destination - transform.position).normalized;
            transform.position += move * positionSpeed * Time.deltaTime;
        }

        else if (Vector3.Distance(transform.position, destination) < 0.01f && state == 0)
        {
            state = 1;
            StartCoroutine(MoveObject());
        }
    }

    IEnumerator MoveObject()
    {
        currentIndex = (currentIndex + 1) % movePosition.Count;
        destination = movePosition[currentIndex];

        yield return new WaitForSeconds(timeDelay);

        state = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.transform.SetParent(transform);
    }

    private void OnCollisionExit(Collision collision)
    {
        collision.transform.SetParent(null);
    }
}
