using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    [Header("Player Interaction")]
    [SerializeField] bool playerNotPass;

    [Header("Ball Interaction")]
    [SerializeField] bool ballNotPass;
    [SerializeField] bool ballStop;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball" && ballNotPass)
        {
            Destroy(other.gameObject);
        }

        else if (other.gameObject.tag == "Ball" && ballStop)
        {
            Ball ball = other.gameObject.GetComponent<Ball>();
            ball.throwingDirection = Vector3.zero;
        }

        else if (other.gameObject.tag == "Player" && playerNotPass)
        {
            Destroy(other.gameObject);
        }
    }
}
