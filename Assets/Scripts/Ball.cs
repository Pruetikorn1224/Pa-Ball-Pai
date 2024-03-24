using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    [Header("Setting")]
    [SerializeField] float ballSpeed;
    [SerializeField] Vector3 ballPositionOffset;

    [System.NonSerialized]
    public bool isThrowingBall;
    [System.NonSerialized]
    public Vector3 throwingDirection;

    GameObject mainCharacter;

    // Start is called before the first frame update
    void Start()
    {
        mainCharacter = GameObject.Find("Player");

        isThrowingBall = false;
        throwingDirection = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isThrowingBall)
        {
            Ray rayCast = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(rayCast, out hit))
            {
                Vector3 targetPosition = hit.point;
                targetPosition.y = transform.position.y;
                throwingDirection = targetPosition - transform.position;
                throwingDirection.Normalize();

                isThrowingBall = true;
            }
        }

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.E) && isThrowingBall)
        {
            transform.position = mainCharacter.transform.position + ballPositionOffset;
            isThrowingBall = false;
        }
        if (Input.GetKeyDown(KeyCode.Q) && isThrowingBall)
        {
            mainCharacter.transform.position = transform.position;
            transform.position = mainCharacter.transform.position + ballPositionOffset;
            isThrowingBall = false;
        }
#endif
    }

    private void FixedUpdate()
    {
        if (isThrowingBall)
        {
            transform.Translate(throwingDirection * Time.deltaTime * ballSpeed);
        }
        else
        {
            throwingDirection = Vector3.zero;

            transform.position = mainCharacter.transform.position + ballPositionOffset;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Wall")
        {

        }

        else if (other.gameObject.tag == "Enemy")
        {

        }
    }
}
