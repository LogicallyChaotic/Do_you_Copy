using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoTile : Tile
{
    public override Queue<KeyPressedEnum> SetTileUse(bool tempInUse, KeyPressedEnum key, SpriteRenderer sR)
    {
        Queue<KeyPressedEnum> tempRouteList = new Queue<KeyPressedEnum>();
        _inUse = tempInUse;
        sR.sprite = _speechSprite;
        tempRouteList.Enqueue(key);
        tempRouteList.Enqueue(key);
        return tempRouteList;
    }
}
