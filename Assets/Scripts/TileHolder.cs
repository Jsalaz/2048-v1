﻿using UnityEngine;
using System.Collections;

//just a holder class for tile
[System.Serializable]
public class TileType{
	public int tileNumber;
	public Color32 tileColor;
	public Color32 tileTextColor;

}

public class TileHolder : MonoBehaviour {

	public static TileHolder instance;
	public TileType[] Tiles;

	void Awake(){
		instance = this;
	}



}
