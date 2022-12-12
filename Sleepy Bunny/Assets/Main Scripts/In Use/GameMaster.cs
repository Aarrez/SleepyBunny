using System.Collections;
using System.Collections.Generic;
using PlayerStM.BaseStates;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(InputScript))]
public class GameMaster : MonoBehaviour
{
    public EventsScript ev;

    private static GameMaster instance;

    public Vector3 CurrentCheckpointPosition;

    private Transform _playerTransform;

    public bool pillowOnFloor = false;

    public bool boxOnFloor = false;

    public bool pillowOnDoor = false;

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

    // Update is called once per frame
    private void Update()
    {
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