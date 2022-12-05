using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public EventsScript ev;

    private static GameMaster instance;

    public Vector3 lastCheckPointPos;

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

    public void EndGame()
    {
        SceneManager.LoadScene("UI Scene");
    }

    public void Restart()
    {
        SceneManager.LoadScene("Level1");
    }
}