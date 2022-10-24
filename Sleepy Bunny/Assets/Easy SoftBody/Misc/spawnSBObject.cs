using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class spawnSBObject : MonoBehaviour {
	public GameObject[] _Objects;
	private GameObject _currentObject;
	public Button _spawnButton;
	public int _queue;
	private bool _end;
	// Use this for initialization
	void Start () {
		_queue = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (_queue == _Objects.Length -1) {
		
			_end = true;
			_spawnButton.gameObject.SetActive (false);
		}

		if (Input.GetKeyDown (KeyCode.Escape)) {
		
			Application.Quit ();
		
		}

	}

	public void Spawn(){
	
		if (_currentObject == null ) {
			_Objects [_queue].SetActive (true);
			_currentObject = _Objects [_queue];

		}

		else {
		
			_currentObject.SetActive (false);
			_currentObject = null;
			_queue += 1;
			Spawn ();
		
		}
	
	}

}
