using System;
using System.Collections;
using System.Collections.Generic;
using Enemies;
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

    public EnemiesGeneralBehaviour enemyBehaviour;
    public MovementType type = MovementType.MoveTowards;
    public MovementPath myPath;
    public float speed = 1;
    public float maxDistanceToGoal = .1f; // how close does it have to be to the point to be considered at point

    #endregion

    #region Private Variables

    private IEnumerator<Transform> pointInPath; // used to reference point returned from myPath.GetNextPathPoint

    #endregion
    
    #region Main Methods

    public void Start()
    {
        enemyBehaviour = GetComponent<EnemiesGeneralBehaviour>();
        if (myPath == null)
        {
            Debug.LogError("Movement Path cannot be null, there must be a path to follow.", gameObject);
            return;
        }

        pointInPath = myPath.GetNextPathPoint();
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

        if (type.Equals(MovementType.MoveTowards))
        {
            transform.position = Vector3.MoveTowards(
                transform.position, 
                pointInPath.Current.position,
                Time.deltaTime * speed);
        } else if (type.Equals(MovementType.LerpTowards))
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
        FlipOrientation();
    }

    private void FlipOrientation()
    {
        if (pointInPath.Current.position.x > transform.position.x)
        {
            if (enemyBehaviour.facingRight)
                enemyBehaviour.Flip();
        } 
        else
        if (!enemyBehaviour.facingRight)
            enemyBehaviour.Flip();
    }

    #endregion
}
