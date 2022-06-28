using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    #region private variables
    [SerializeField] protected Sprite _speechSprite;
    protected bool _inUse = false;
    protected SpriteRenderer _spriteRenderer;
    protected const int GRIDSIZE = 5;
    #endregion
    #region unity methods
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    #endregion
    #region other functions
    public virtual Queue<KeyPressedEnum> SetTileUse(bool tempInUse, KeyPressedEnum key, SpriteRenderer sR)
    {
        Queue<KeyPressedEnum> tempRouteList = new Queue<KeyPressedEnum>();
        _inUse = tempInUse;
        sR.sprite = _speechSprite;
        tempRouteList.Enqueue(key);
        return tempRouteList;
    }

    [ContextMenu("SnapToGrid")]
    public void snapToGrid()
    {
        float tempx = transform.position.x;
        float tempy = transform.position.y;

        tempx = (Mathf.RoundToInt(tempx / GRIDSIZE)) * GRIDSIZE;
        tempy = (Mathf.RoundToInt(tempy / GRIDSIZE)) * GRIDSIZE;

        transform.position = new Vector3(tempx, tempy, transform.position.z);
    }
    #endregion
}
