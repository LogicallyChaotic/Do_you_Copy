using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class KeyHolder : MonoBehaviour
{
    #region private variables
    [SerializeField] private KeyInformationHolder _keyInformation;
    [SerializeField] private int _indexInList;
    private Image _keyHolderImage;
    private Animator _anim;
    private Sprite _unusedSprite;
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
    #endregion
    #region unity Methods
    private void OnEnable()
    {
        GameEvents.OnKeyPressed += KeyHasBeenPressed;

    }
    private void OnDisable()
    {
        GameEvents.OnKeyPressed -= KeyHasBeenPressed;
    }

    private void Awake()
    {
        _keyHolderImage = GetComponent<Image>();
        _anim = GetComponent<Animator>();
        _unusedSprite = _keyHolderImage.sprite;
    }
    #endregion
    #region other Functions
    private void KeyHasBeenPressed(int _currentKeyHolder, object keyPressed, bool isUsed)
    {

        if (_currentKeyHolder != indexInList)
        {
            return;
        } 

        if(keyPressed == null)
        {
            _anim.SetTrigger("Pressed");
            _keyHolderImage.sprite = _unusedSprite;
            return;
        }

        KeyPressedEnum tempkeyPressedUnboxed = (KeyPressedEnum)keyPressed;
        KeyInformationHolder.KeySet tempKeyPressed = _keyInformation.keyPressInformation.Find((x) => x.keyPressedEnum == tempkeyPressedUnboxed);
        _anim.SetTrigger("Pressed");
        _keyHolderImage.sprite = tempKeyPressed.keySprite;

    }
    #endregion
}