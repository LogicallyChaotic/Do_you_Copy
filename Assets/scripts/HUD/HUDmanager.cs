using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDmanager : Singleton<HUDmanager>
{
    #region private variables
    [SerializeField] private int _totalMoves = 5;
    [SerializeField] private KeyHolder _keyHolderPrefab;
    [SerializeField] private AudioSource _buttonPress;
    private int _index = 0;
    private Queue<KeyPressedEnum> _keySequence;
    private List<KeyPressedEnum> _keySequenceList;
    private CopyCharacter _copyChar;
    #endregion

    #region unityMethods
    protected override void Awake()
    {
        base.Awake();
        _copyChar = FindObjectOfType<CopyCharacter>();
        _keySequence = new Queue<KeyPressedEnum>();
        _buttonPress = GetComponent<AudioSource>();

    }
    private void Update()
    {
        if (ConstantsManager.Instance.startGame == true)
        {
            if (_index != _totalMoves && _copyChar.isFinishedMoving)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    OnKeyPressed(KeyPressedEnum.UP);
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    OnKeyPressed(KeyPressedEnum.DOWN);
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    OnKeyPressed(KeyPressedEnum.LEFT);
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    OnKeyPressed(KeyPressedEnum.RIGHT);
                }
            }
        }
    }
    #endregion
    #region Other functions
    private void OnKeyPressed(KeyPressedEnum _keyRecentlyPressed)
    {
        if (_index < _totalMoves)
        {
            _buttonPress.Play();
            GameEvents.OnKeyPressed(_index, _keyRecentlyPressed, true);
            _keySequence.Enqueue(_keyRecentlyPressed);
            _index++;

            if (ConstantsManager.Instance.isHard)
            {
                GameEvents.OnTurnStart(_keySequence, _totalMoves);
            }
        }
        if (_index == _totalMoves && !ConstantsManager.Instance.isHard)
        {
            GameEvents.OnTurnStart(_keySequence, _totalMoves);
        }
        if (_index == _totalMoves && ConstantsManager.Instance.isHard)
        {
            StartCoroutine(c_checkWon());
        }
    }
    [ContextMenu("setKeyIndexes")]
    public void setKeyIndexes()
    {
        for (int i = 0; i < _totalMoves; i++)
        {
            KeyHolder tempKeyHolder = Instantiate(_keyHolderPrefab, this.gameObject.transform);
            tempKeyHolder.indexInList = i;
        }
    }

    public bool compareMoves()
    {
        if(_index == _totalMoves)
        {
            return true;
        }
        return false;
    }

    #endregion
    #region coroutines
    private IEnumerator c_checkWon()
    {
        var playerChar = FindObjectOfType<PlayerCharacter>();
        yield return new WaitForSeconds(1f);
        
        if (_index == _totalMoves && !_copyChar._hasWon)
        {
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(playerChar.LoseGame());
        }
    }
    #endregion
}
