using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	//singleton causes problems
	public static GameManager instance;

	//sets tiles to 0 set to private later
	public Tile[] SetTiles;

	//multiDim array for the tiles set to private later
	public Tile[,] allTiles = new Tile[4,4];

	//having issues with list of tile[] collumns and rows
	private List<Tile[]> collumns = new List<Tile[]> ();
	private List<Tile[]> rows = new List<Tile[]> ();
	private List<Tile> EmptySpacesList = new List<Tile> ();


	void Awake(){
		instance = this;
		//rather than calling Start() from awake 
		//I should move start's code block within Awake()
		//or divide the commands in start into other methods
		Start ();
	}

	// Use this for initialization
	void Start () {
		// stores all tile objects into SetTiles
		SetTiles = GameObject.FindObjectsOfType<Tile>();
		foreach (Tile e in SetTiles) {
			e.Number = 0;
			//fill all tiles with current tiles
			allTiles [e.rowLocation, e.colLocation] = e;
			//possibly redundant might delete
			EmptySpacesList.Add (e);
		}

		for (int i = 0; i < 4; i++) {
			//	Short version
			//Simplified version of innitializing collumns and rows lists
			collumns.Add(new Tile[]{allTiles[0,i], allTiles[1,i], allTiles[2,i], allTiles[3,i]});
			rows.Add(new Tile[]{allTiles[i,0], allTiles[i,1], allTiles[i,2], allTiles[i,3]});
		}
		//was used for debuging
		/*
		Debug.Log ("Col Length" + collumns[0].Length);
		Debug.Log ("Col count" + collumns.Count);
		Tile[] tempd = collumns [1];
		Tile[] tempd2 = collumns [1];
		Debug.Log (tempd[1].Number);
		Debug.Log (tempd[2].Number);
		*/

		TileGenerator ();
		TileGenerator ();
	}

	public void NewGameButton(){
		Start ();
		//Scene scene = SceneManager.GetActiveScene ();
		//SceneManager.LoadScene(scene.name);
	}

	//keeps
	//tile generator
	void TileGenerator(){
		//Checks the number of empty spaces
		if (EmptySpacesList.Count > 0) {
			int index = Random.Range (0, EmptySpacesList.Count);
			//testing generating 2 need to upgrade it to generate 2 or 4
			int rNum = Random.Range(0, 8);
			if (rNum == 4) {
				EmptySpacesList [index].Number = 4;
				EmptySpacesList.RemoveAt (index);
			} else {
				EmptySpacesList [index].Number = 2;
				EmptySpacesList.RemoveAt (index);
			}
		}
	}

	bool MoveIndexDown(Tile[] TileLine){
		Debug.Log (TileLine.Length);
		for (int i = 0; i < TileLine.Length-1; i++) {


			//first make all available moves
			//Moving non empty tile to empty tile
			//if TileLine[i] is empty and TileLine[i+1] is not empty then move non-empty to empty
			if (TileLine[i].Number == 0 && TileLine[i+1].Number != 0){

				TileLine [i].Number = TileLine [i + 1].Number;
				TileLine [i+1].Number = 0;
				return true;
			}

			//merging tiles
			if((TileLine[i].Number == TileLine[i+1].Number) && TileLine[i].hasMerged == false &&
				TileLine[i+1].hasMerged == false && TileLine[i].Number !=0)
			{
				TileLine [i].Number = TileLine [i].Number * 2;
				TileLine [i + 1].Number = 0;
				TileLine [i].hasMerged = true;
				ScoreTracker.instance.Score += TileLine [i].Number;
				return true;
				//TileLine [i+1].hasMerged = true;
			}
		}
		ResetHasMerged ();
		return false;
	}

	bool MoveIndexUp(Tile[] TileLine){
		Debug.Log (TileLine.Length);
		for (int i = TileLine.Length-1 ; i > 0; i--) {
			//Moving non empty tile to empty tile
			//if TileLine[i] is empty and TileLine[i-1] is not empty then move non-empty to empty
			if (TileLine[i].Number == 0 && TileLine[i-1].Number != 0){

				TileLine [i].Number = TileLine [i - 1].Number;
				TileLine [i-1].Number = 0;
				return true;
			}

			//merging tiles
			if((TileLine[i].Number == TileLine[i-1].Number) && TileLine[i].hasMerged == false &&
				TileLine[i-1].hasMerged == false && TileLine[i].Number !=0)
			{
				TileLine [i].Number = TileLine [i].Number * 2;
				TileLine [i - 1].Number = 0;
				TileLine [i].hasMerged = true;
				ScoreTracker.instance.Score += TileLine [i].Number;
				return true;
				//TileLine [i+1].hasMerged = true;
			}
		}
		ResetHasMerged ();
		return false;
	}

	private void ResetHasMerged(){
		foreach (Tile n in allTiles) {
			n.hasMerged = false;
		}
	}

	private void ResetEmpty(){
		EmptySpacesList.Clear ();
		foreach (Tile n in allTiles) {
			if (n.Number == 0) {
				EmptySpacesList.Add (n);
			}
		}
	}
	// Update is called once per frame
	/*void Update () {
		//delete this conditional after testing
		if(Input.GetKeyDown(KeyCode.G)){
			TileGenerator();
		}
	}
*/
	public void Move(Direction dir){
		Debug.Log ("In Move method collumns count " + collumns.Count);

		bool hasMoved = false;
		for (int i = 0; i < 4; i++) {
			switch (dir) {
			case Direction.Up:
				while(MoveIndexDown(collumns[i])){
					hasMoved = true;
				}
				break;
			case Direction.Down:
				while (MoveIndexUp (collumns[i])) {
					hasMoved = true;
				}
				break;
			case Direction.Left:
				while(MoveIndexDown(rows[i])){
					hasMoved = true;
				}
				break;
			case Direction.Right:
				while (MoveIndexUp (rows [i])) {
					hasMoved = true;
				}
				break;
			}
		}

		if (hasMoved) {
			ResetEmpty ();
			TileGenerator ();
		}
	}
}