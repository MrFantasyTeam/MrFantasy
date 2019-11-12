using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPath : MonoBehaviour
{
    #region Enums

    public enum PathTypes
    {
        linear, loop
    }

    #endregion

    #region Public Variables

    public PathTypes pathType; // indicates the type of path
    public int movementDirection = 1; // 1 clockwise/forward || -1 counter clockwise/backwards
    public int movingTo = 0; // used to identify point in Path sequence which is moving towards
    public Transform[] pathSequence; // array of all points in the path

    #endregion

    #region Main Methods

    // draws the line the object is following
    public void OnDrawGizmos()
    {
        if (pathSequence == null || pathSequence.Length < 2)
        {
            return;
        }

        for (int i = 0; i < pathSequence.Length - 1; i++)
        {
            Gizmos.DrawLine(pathSequence[i].position, pathSequence[i+1].position);
        }

        if (pathType == PathTypes.loop)
        {
            Gizmos.DrawLine(pathSequence[pathSequence.Length-1].position, pathSequence[0].position);
        }
    }

    #endregion

    #region Coroutines

    public IEnumerator<Transform> GetNextPathPoint()
    {
        if (pathSequence == null || pathSequence.Length < 1) 
        {
            yield break;
        }

        while (true)
        {
            yield return pathSequence[movingTo];
            
            if (pathSequence.Length == 1)
            {
                continue;
            }

            if (pathType == PathTypes.linear)
            {
                if (movingTo <= 0)
                {
                    movementDirection = 1;
                }
                else if (movingTo >= pathSequence.Length - 1)
                {
                    movementDirection = -1;
                }
            } 
            movingTo += movementDirection;

            if (pathType == PathTypes.loop) 
            {
                // 
                if (movingTo >= pathSequence.Length)
                {
                    movingTo = 0;
                }

                if (movingTo < 0)
                {
                    movingTo = pathSequence.Length;
                }
            }
        }
    }

    #endregion
}
