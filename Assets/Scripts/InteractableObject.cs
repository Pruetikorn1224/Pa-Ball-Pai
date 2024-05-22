using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [Header("Interactable Object")]
    [SerializeField] List<GameObject> objectsDisappear;
    [SerializeField] List<GameObject> objectsAppear;

    [Header("Story")]
    [SerializeField] List<GameObject> navigationUi;

    LevelControl levelControl;

    // Start is called before the first frame update
    void Start()
    {
        levelControl = GameObject.Find("Level Manager").GetComponent<LevelControl>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && levelControl.levelState == 0)
        {
            int count = (objectsDisappear.Count > objectsAppear.Count) ? objectsDisappear.Count : objectsAppear.Count;
            for (int i = 0; i < count; i++)
            {
                if (i < objectsDisappear.Count)
                    objectsDisappear[i].gameObject.SetActive(false);
                if (i < objectsAppear.Count)
                    objectsAppear[i].gameObject.SetActive(true);
            }
        }
    }
}
