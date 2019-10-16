using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    #region Enums

    public enum MovementType
    {
        MoveTowards, LerpTowards
    }

    #endregion
    
    #region Public Variables

    public MovementType Type = MovementType.MoveTowards;
    public MovementPath MyPath;
    public float speed = 1;
    public float maxDistanceToGoal = .1f; // how close does it have to be to the point to be considered at point
    
    #endregion

    #region Private Variables

    private IEnumerator<Transform> pointInPath; // used to reference point returned from MyPath.GetNextPathPoint

    #endregion
    
    #region Main Methods

    public void Start()
    {
        if (MyPath == null)
        {
            Debug.LogError("Movement Path cannot be null, there must be a path to follow.", gameObject);
            return;
        }

        pointInPath = MyPath.GetNextPathPoint();
        pointInPath.MoveNext();
        if (pointInPath.Current == null)
        {
            Debug.LogError("A path must have points in it to follow", gameObject);
            return;
        }

        transform.position = pointInPath.Current.position;
    }

    public void FixedUpdate()
    {
        if (pointInPath == null || pointInPath.Current == null)
        {
            return;
        }

        if (Type.Equals(MovementType.MoveTowards))
        {
            transform.position = Vector3.MoveTowards(
                transform.position, 
                pointInPath.Current.position,
                Time.deltaTime * speed);
        } else if (Type.Equals(MovementType.LerpTowards))
        {
            transform.position = Vector3.Lerp(
                transform.position, 
                pointInPath.Current.position, 
                Time.deltaTime * speed);
        }

        var distanceSquared = (transform.position - pointInPath.Current.position).sqrMagnitude;
        if (distanceSquared < maxDistanceToGoal * maxDistanceToGoal)
        {
            pointInPath.MoveNext();
        }
    }

    #endregion
}
