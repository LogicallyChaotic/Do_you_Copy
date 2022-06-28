using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField]private int _indexInList;
    public int indexInList
    {
        get
        {
            return _indexInList;
        }
        set
        {
            _indexInList = value;
        }
    }
}
