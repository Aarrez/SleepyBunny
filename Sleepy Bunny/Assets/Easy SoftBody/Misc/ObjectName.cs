using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ObjectName : MonoBehaviour {
	public spawnSBObject _spawner;
	public string[] _Names;
	private Text _thisText;
	// Use this for initialization
	void Start () {
		_thisText = GetComponent<Text> ();
		_thisText.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		_thisText.text = _Names [_spawner._queue];
	}

	public void EnableText(){
	
		_thisText.enabled = true;
	
	}
}
