using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Ball : MonoBehaviour
{

    [Header("Setting")]
    [SerializeField] float ballSpeed;
    [SerializeField] Vector3 ballPositionOffset;

    [System.NonSerialized]
    public bool isThrowingBall;
    [System.NonSerialized]
    public Vector3 throwingDirection;

    LevelControl levelControl;
    GameObject mainCharacter;

    // Start is called before the first frame update
    void Start()
    {
        mainCharacter = GameObject.FindGameObjectsWithTag("Player")[0];
        levelControl = GameObject.Find("Level Manager").GetComponent<LevelControl>();

        isThrowingBall = false;
        throwingDirection = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (levelControl.levelState == 0)
        {
            if (Input.GetMouseButtonDown(0) && !isThrowingBall)
            {
                Ray rayCast = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(rayCast, out hit))
                {
                    if (hit.collider.gameObject.tag != "Wall")
                    {
                        StartCoroutine(ThrowingBall(rayCast));
                    }
                }
            }

#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.Q) && isThrowingBall)
            {
                transform.position = mainCharacter.transform.position + ballPositionOffset;
                isThrowingBall = false;
            }
            if (Input.GetKeyDown(KeyCode.E) && isThrowingBall)
            {
                mainCharacter.transform.position = transform.position - ballPositionOffset + new Vector3(0, 0.05f, 0);
                isThrowingBall = false;
            }
#endif
        }
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
            throwingDirection = Vector3.Reflect(throwingDirection, other.contacts[0].normal);
            throwingDirection.Normalize();
        }
    }

    IEnumerator ThrowingBall(Ray ray)
    {
        yield return new WaitForSeconds(0.25f);

        float t = (transform.position.y - ray.origin.y) / ray.direction.y;
        Vector3 targetPosition = ray.origin + ray.direction * t;

        throwingDirection = targetPosition - transform.position;
        throwingDirection.Normalize();

        transform.position = mainCharacter.transform.position + new Vector3(throwingDirection.x, transform.position.y - mainCharacter.transform.position.y, throwingDirection.z);

        isThrowingBall = true;
    }
}
