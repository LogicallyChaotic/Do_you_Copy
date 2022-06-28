using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterferenceTile : Tile
{
    public override Queue<KeyPressedEnum> SetTileUse(bool tempInUse, KeyPressedEnum key, SpriteRenderer sR)
    {
        Queue<KeyPressedEnum> tempRouteList = new Queue<KeyPressedEnum>();
        _inUse = tempInUse;
        sR.sprite = _speechSprite;

        KeyPressedEnum tempkey = KeyPressedEnum.UP;
        switch (key)
        {
            case KeyPressedEnum.LEFT:
                tempkey = KeyPressedEnum.RIGHT;
                break;
            case KeyPressedEnum.RIGHT:
                tempkey = KeyPressedEnum.LEFT;
                break;
            case KeyPressedEnum.UP:
                tempkey = KeyPressedEnum.DOWN;
                break;
            case KeyPressedEnum.DOWN:
                tempkey = KeyPressedEnum.UP;
                break;
        }

        tempRouteList.Enqueue(tempkey);
        return tempRouteList;
    }
}
