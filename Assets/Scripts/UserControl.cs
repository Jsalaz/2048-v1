using UnityEngine;
using System.Collections;

//possible enums to define control direction
public enum Direction{
	Up, Down, Left, Right
}

public class UserControl : MonoBehaviour {
	
	public static UserControl instance;

	//awake
	void Awake(){
		instance = this;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			GameManager.instance.Move (Direction.Up);
			//GameManager.instance.Move (Direction.Up);
		}else
		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			GameManager.instance.Move (Direction.Down);
		}else
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			GameManager.instance.Move (Direction.Right);
		}else		
			if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			GameManager.instance.Move (Direction.Left);
		}
	}
}
