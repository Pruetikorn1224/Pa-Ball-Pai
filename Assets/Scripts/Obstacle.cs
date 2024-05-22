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

    LevelControl levelControl;

    private void Start()
    {
        levelControl = GameObject.Find("Level Manager").GetComponent<LevelControl>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball" && ballNotPass && levelControl.levelState == 0)
        {
            levelControl.levelState = 1;
        }

        else if (other.gameObject.tag == "Ball" && ballStop && levelControl.levelState == 0)
        {
            Ball ball = other.gameObject.GetComponent<Ball>();
            ball.throwingDirection = Vector3.zero;
            ball.isThrowingBall = false;
        }

        else if (other.gameObject.tag == "Player" && playerNotPass && levelControl.levelState == 0)
        {
            levelControl.levelState = 1;
        }
    }
}
