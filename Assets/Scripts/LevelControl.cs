using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControl : MonoBehaviour
{

    GameObject[] balls;
    GameObject[] players;

    [SerializeField] int maximumLives = 3;
    [HideInInspector]
    public int currentLives;

    [HideInInspector]
    public int levelState;
    [HideInInspector]
    public Vector3 respawnPosition;

    Vector3 ballPositionOffset = new Vector3(1.2f, 0.5f, 0);

    // Start is called before the first frame update
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length == 0)
            Debug.LogError("Cannot find player(s) in the scene; please debug in LevelControl.cs");

        balls = GameObject.FindGameObjectsWithTag("Ball");
        if (balls.Length != 1)
            Debug.LogError("There should be playable ball(s) in the scene; please debug in LevelControl.cs");

        respawnPosition = players[0].transform.position;
        currentLives = maximumLives;
    }

    // Update is called once per frame
    void Update()
    {
        switch (levelState)
        {
            case 0:
                // 0 - When the game is in playable state
                break;

            case 1:
                // 1 - When player or ball hit the obstacle, and proceed to respawn
                currentLives--;
                if (currentLives == 0)
                {
                    levelState = 2;

                    for (int i = 0; i < players.Length; i++)
                    {
                        balls[i].transform.position = players[i].transform.position + ballPositionOffset;
                        balls[i].GetComponent<Ball>().isThrowingBall = false;
                        balls[i].GetComponent<Ball>().throwingDirection = Vector3.zero;
                    }
                }
                else
                {
                    levelState = 0;
                    for (int i = 0; i < players.Length; i++)
                    {
                        players[i].transform.position = respawnPosition;

                        balls[i].transform.position = respawnPosition + ballPositionOffset;
                        balls[i].GetComponent<Ball>().isThrowingBall = false;
                        balls[i].GetComponent<Ball>().throwingDirection = Vector3.zero;
                    }
                }
                break;

            case 2:
                // 2 - When the lives is equal to 0,or game over
                if (Input.GetKeyDown(KeyCode.R))
                {
                    currentLives = maximumLives;
                    respawnPosition = new Vector3(0, 1.125f, 0f); ;
                    for (int i = 0; i < players.Length; i++)
                    {
                        players[i].transform.position = new Vector3(0f, 1.125f, 0f);
                    }
                    levelState = 0;
                }
                break;

            default:
                Debug.LogError("The state is unable to process; please debug in LevelControl.cs");
                break;
        }
    }
}
