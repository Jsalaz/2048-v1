using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public static GameManager instance;
	//sets tiles to 0 set to private later
	public Tile[] SetTiles;

	//multiDim array for the tiles set to private later
	public Tile[,] allTiles = new Tile[4,4];
	private List<Tile[]> collumns = new List<Tile[]> ();
	private List<Tile[]> rows = new List<Tile[]> ();
	private List<Tile> EmptySpacesList = new List<Tile> ();


	void Awake(){
		instance = this;
	}
	// Use this for initialization
	void Start () {

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

		/*	Long version
		collumns.Add(new Tile[]{allTiles[0,0], allTiles[1,0], allTiles[2,0], allTiles[3,0]});
		collumns.Add(new Tile[]{allTiles[0,1], allTiles[1,0], allTiles[2,0], allTiles[3,0]});
		collumns.Add(new Tile[]{allTiles[0,2], allTiles[1,0], allTiles[2,0], allTiles[3,0]});
		collumns.Add(new Tile[]{allTiles[0,3], allTiles[1,0], allTiles[2,0], allTiles[3,0]});

		rows.Add(new Tile[]{allTiles[0,0], allTiles[0,1], allTiles[0,2], allTiles[0,3]});
		rows.Add(new Tile[]{allTiles[0,0], allTiles[0,1], allTiles[0,2], allTiles[0,3]});
		rows.Add(new Tile[]{allTiles[0,0], allTiles[0,1], allTiles[0,2], allTiles[0,3]});
		rows.Add(new Tile[]{allTiles[0,0], allTiles[0,1], allTiles[0,2], allTiles[0,3]});
		*/

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
		for (int i = 0; i < TileLine.Length-1; i++) {

			//Moving non empty tile to empty tile
			//if TileLine[i] is empty and TileLine[i+1] is not empty then move non-empty to empty
			if (TileLine[i].Number == 0 && TileLine[i+1].Number != 0){

				TileLine [i].Number = TileLine [i + 1].Number;
				TileLine [i+1].Number = 0;
				return true;
			}
		}
		return false;
	}

	bool MoveIndexUp(Tile[] TileLine){
		for (int i = TileLine.Length-1 ; i < 0; i--) {

			//Moving non empty tile to empty tile
			//if TileLine[i] is empty and TileLine[i-1] is not empty then move non-empty to empty
			if (TileLine[i].Number == 0 && TileLine[i-1].Number != 0){

				TileLine [i].Number = TileLine [i - 1].Number;
				TileLine [i-1].Number = 0;
				return true;
			}
		}
		return false;
	}


	// Update is called once per frame
	void Update () {
		//delete this conditional after testing
		if(Input.GetKeyDown(KeyCode.G)){
			TileGenerator();
		}
	}

	public void Move(Direction dir){	
		Debug.Log (dir.ToString() + " Move");
	}
}