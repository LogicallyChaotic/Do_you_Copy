using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalTile : Tile
{
    [SerializeField] private LayerMask _playerLayers;
    public bool isEntityDone;
    public void Update()
    {
        Collider2D tempTile = Physics2D.OverlapBox(transform.position, new Vector2(10,10), 0, _playerLayers);
        isEntityDone = tempTile ? true : false;
    }
    public override Queue<KeyPressedEnum> SetTileUse(bool tempInUse, KeyPressedEnum key, SpriteRenderer sR)
    {
        Queue<KeyPressedEnum> tempRouteList = new Queue<KeyPressedEnum>();
        _inUse = tempInUse;
        sR.sprite = _speechSprite;
        tempRouteList.Enqueue(key);
        return tempRouteList;
    }
}
