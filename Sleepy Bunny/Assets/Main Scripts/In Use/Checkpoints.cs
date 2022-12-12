using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CheckPoint", menuName = "Create Checkpoint", order = 1)]
public class Checkpoints : ScriptableObject
{
    private bool isActive;

    public Vector3 position;
}