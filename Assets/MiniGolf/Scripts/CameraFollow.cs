using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow instance;

    [SerializeField] private ActiveVectors activeVectors;   //class which decide axis allowed to follow

    private GameObject followTarget;
    private Vector3 offset;
    private Vector3 currentPos;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void SetTarget(GameObject target)
    {
        followTarget = target;
        offset = followTarget.transform.position - transform.position;
        currentPos = transform.position;
    }


    private void LateUpdate()
    {
        if (followTarget)
        {
            if (activeVectors.x)
            {
                currentPos.x = followTarget.transform.position.x - offset.x;
            }
            if (activeVectors.y)
            {
                currentPos.y = followTarget.transform.position.y - offset.y;
            }
            if (activeVectors.z)                                        //if z axis is allowed
            {                                                           //set the changePos z
                currentPos.z = followTarget.transform.position.z - offset.z;
            }
            transform.position = currentPos;                             //set the transform of camera
        }
    }
}

[System.Serializable]
public class ActiveVectors
{
    public bool x, y, z;
}
