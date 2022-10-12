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

    void Awake()
    {
        //shoeBox = GameObject.Find("Pushable shoe box");
        //floorPillow = GameObject.Find("PILLOW");
        //Door = GameObject.Find("Door");
        //floor = GameObject.Find("Cube.002");

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

    // Update is called once per frame
    void Update()
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
