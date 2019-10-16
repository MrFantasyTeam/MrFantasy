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

    public PathTypes PathType; // indicates the type of path
    public int movementDirection = 1; // 1 clockwise/forward || -1 counter clockwise/backwards
    public int movingTo = 0; // used to identify point in Path sequence which is moving towards
    public Transform[] PathSequence; // array of all points in the path

    #endregion

    #region Main Methods

    // draws the line the object is following
    public void OnDrawGizmos()
    {
        if (PathSequence == null || PathSequence.Length < 2)
        {
            return;
        }

        for (int i = 0; i < PathSequence.Length; i++)
        {
            Gizmos.DrawLine(PathSequence[i].position, PathSequence[i+1].position);
        }

        if (PathType == PathTypes.loop)
        {
            Gizmos.DrawLine(PathSequence[PathSequence.Length].position, PathSequence[0].position);
        }
    }

    #endregion

    #region Coroutines

    public IEnumerator<Transform> GetNextPathPoint()
    {
        if (PathSequence == null || PathSequence.Length < 1) 
        {
            yield break;
        }

        while (true)
        {
            yield return PathSequence[movingTo];
            
            if (PathSequence.Length == 1)
            {
                continue;
            }

            if (PathType == PathTypes.linear)
            {
                if (movingTo <= 0)
                {
                    movementDirection = 1;
                }
                else if (movingTo >= PathSequence.Length - 1)
                {
                    movementDirection = -1;
                }
            } 
            movingTo += movementDirection;

            if (PathType == PathTypes.loop) 
            {
                // 
                if (movingTo >= PathSequence.Length)
                {
                    movingTo = 0;
                }

                if (movingTo < 0)
                {
                    movingTo = PathSequence.Length;
                }
            }
        }
    }

    #endregion
}
