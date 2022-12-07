using System;
using System.Collections;
using System.Collections.Generic;
using Runtime.Blocks;
using Runtime.Grid;
using UnityEngine;
using Utilities;

[RequireComponent(typeof(BaseBlock))]
public class MovementDebugger : MonoBehaviour
{
    public bool moveForward;
    public bool moveBackward;
    public bool moveLeft;
    public bool moveRight;
    public bool moveUp;
    public bool moveDown;

    private void Update()
    {
        Check(ref moveForward, Vector3Int.forward);
        Check(ref moveBackward, Vector3Int.back);
        Check(ref moveLeft, Vector3Int.left);
        Check(ref moveRight, Vector3Int.right);
        Check(ref moveUp, Vector3Int.up);
        Check(ref moveDown, Vector3Int.down);
    }

    private void Check(ref bool dirBool, Vector3Int resultingDirection)
    {
        if (!dirBool) return;
        dirBool = false;
        GridManager.Instance.Move(GetComponent<BaseBlock>(),transform.position.ToVector3Int() + resultingDirection);
    }
}
