using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnPoint", menuName = "SpawnPoint")]
public class SpawnPoints : ScriptableObject
{
    [SerializeField] private int _spawnID;
}