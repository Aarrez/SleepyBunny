using System.Collections;
using System.Collections.Generic;
using PlayerStM.BaseStates;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(InputScript))]
public class GameMaster : MonoBehaviour
{
    private static GameMaster instance;

    [HideInInspector] public Vector3 CurrentCheckpointPosition;

    private Transform _playerTransform;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
        _playerTransform = FindObjectOfType<PlayerStateMachine>().transform;
    }

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void PlayerDied()
    {
        _playerTransform.position = CurrentCheckpointPosition;
    }

    public void EndGame()
    {
        SceneManager.LoadScene("UI Scene");
    }

    public void Restart()
    {
        SceneManager.LoadScene("Level1");
    }
}