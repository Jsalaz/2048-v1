using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tile : MonoBehaviour {

	//number with setter and
	private int number;
	public int Number{
		get{
			return number;
		}
		set{
			number = value;
			if (number == 0) {
				HideTile ();
			} else {
				TileNumChecker (number);
				ShowTile ();
			}
		}
	}
		
	private Text _TileText;
	private Image _TileImage;

	public int rowLocation;
	public int colLocation;

	//gets current number and image from tile
	void Awake(){
		_TileText = GetComponentInChildren<Text> ();
		_TileImage = transform.GetComponent<Image> ();
		//_TileImage = transform.Find ("GameTileX").GetComponent<Image> ();
	}

	void ApplyTile(int index){
		//changes tile number
		_TileText.text = TileHolder.instance.Tiles [index].tileNumber.ToString();
		//changes tile color
		_TileImage.color = TileHolder.instance.Tiles [index].tileColor;
		//changes tile text color
		_TileText.color = TileHolder.instance.Tiles [index].tileTextColor;
	}

	//checks for the number in game backend in order to apply the proper tile on game frontend
	void TileNumChecker(int num){

		switch(num){

		case 2:
			ApplyTile (0);
			break;
		case 4:
			ApplyTile (1);
			break;
		case 8:
			ApplyTile (2);
			break;
		case 16:
			ApplyTile (3);
			break;
		case 32:
			ApplyTile (4);
			break;
		case 64:
			ApplyTile (5);
			break;
		case 128:
			ApplyTile (6);
			break;
		case 256:
			ApplyTile (7);
			break;
		case 512:
			ApplyTile (8);
			break;
		case 1024:
			ApplyTile (9);
			break;
		case 2048:
			ApplyTile (10);
			break;
		default:
			ApplyTile (11);	
			break;
		}
	}

	private void ShowTile(){
		_TileImage.enabled = true;
		_TileText.enabled = true;
	}

	private void HideTile(){
		_TileText.enabled = false;
		_TileImage.enabled = false;
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
