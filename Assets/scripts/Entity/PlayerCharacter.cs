using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Base_char
{
    [SerializeField] protected AudioSource _winSound;
    [SerializeField] private SpriteRenderer _SpeechspriteRenderer;
    private CopyCharacter _copyChar;
    private int _movesLeft;
    private void Awake()
    {
        _SpeechspriteRenderer.gameObject.SetActive(false);
        _copyChar = FindObjectOfType<CopyCharacter>();
    }
    protected override void StartTurn(Queue<KeyPressedEnum> sequence, int totalMoves)
    {
        StartCoroutine(c_startTurn(sequence));
        if (ConstantsManager.Instance.isHard)
        {
            _copyChar.isFinishedMoving = false;
        }
        _SpeechspriteRenderer.gameObject.SetActive(true);
    }
    protected override IEnumerator c_startTurn(Queue<KeyPressedEnum> sequence)
    {
        int tempTotalMoves = sequence.Count;
        for (int i = 0; i < tempTotalMoves; i++)
        {
            KeyPressedEnum tempKey = sequence.Dequeue();
            _movesLeft = sequence.Count;
            switch (tempKey)
            {
                case KeyPressedEnum.LEFT:
                    _raycastVector = new Vector3(-HORIZONTALMOVEMENT, 0, 0);
                    break;
                case KeyPressedEnum.RIGHT:
                    _raycastVector = new Vector3(HORIZONTALMOVEMENT, 0, 0);
                    break;
                case KeyPressedEnum.UP:
                    _raycastVector = new Vector3(0, VERTICLEMOVEMENT, 0);
                    break;
                case KeyPressedEnum.DOWN:
                    _raycastVector = new Vector3(0, -VERTICLEMOVEMENT, 0);
                    break;
                default:
                    break;
            }


            if (!_copyChar._isDead)
            {
                if (!ConstantsManager.Instance.isHard)
                {
                    yield return new WaitUntil(() => _copyChar.isFinishedMoving == true);
                }

                _nextSpaceTrigger.transform.localPosition = _raycastVector;
                RaycastHit2D tempHit = Physics2D.Raycast(_nextSpaceTrigger.transform.position, Vector2.zero, Mathf.Infinity, _tileLayer);

                if (tempHit.collider != null)
                {
                    _nextLoc = new Vector3(0, 0, 0);
                    this.transform.parent = tempHit.collider.transform;
                }
                else
                {
                    _nextLoc = _raycastVector;
                }
                _anim.SetTrigger("Walk");
                yield return new WaitForSeconds(0.05f);
                MidTurn();
                yield return new WaitForSeconds(_waitTimeBeforeEndTurn);
                MidEndTurn(tempKey);
                yield return new WaitForSeconds(_waitTimeBeforeEndTurn);
                EndTurn();


                if (sequence.Count == 0)
                {
                    yield return new WaitUntil(_copyChar.EndTurn);

                    if (!_copyChar._hasWon)
                    {
                        if (!ConstantsManager.Instance.isHard)
                        {
                            StartCoroutine(LoseGame());
                        }
                    }
                }
            }
        }
    }
    protected override void MidEndTurn(KeyPressedEnum tempkey)
    {
        base.MidEndTurn(tempkey);
        RaycastHit2D tempHit = Physics2D.Raycast(_groundCheck.transform.position, Vector2.zero, Mathf.Infinity, _tileLayer);
        if (tempHit.collider)
        {
            Queue<KeyPressedEnum> routeTaken = tempHit.collider.GetComponent<Tile>().SetTileUse(true, tempkey, _SpeechspriteRenderer);
            GameEvents.OnTurnContinue(routeTaken);

            if (tempHit.collider.GetComponent<PauseTile>())
            {
                StartCoroutine(changeToMoving());
            }
        }
    }

    private IEnumerator changeToMoving()
    {
        yield return new WaitForSeconds(0.2f);
        _copyChar.isFinishedMoving = true;
    }
    public void checkEnd()
    {
        RaycastHit2D tempHit = Physics2D.Raycast(_groundCheck.transform.position, Vector2.zero, Mathf.Infinity, _tileLayer);

        if (tempHit.collider.CompareTag("Final") && GameManagerCheck.Instance.Finished && HUDmanager.Instance.compareMoves() && !_isDead)
        {
            if(!ConstantsManager.Instance.isHard)
            {
                if(_movesLeft > 0)
                {
                    return;
                }
            }
            _copyChar._hasWon = true;
            StartCoroutine(_copyChar.WinGame());
            StartCoroutine(WinGame());
        }
    }
    public override IEnumerator WinGame()
    {
        yield return new WaitForSeconds(0.6f);
        if (!_isDead && !_copyChar._isDead)
        {
            _winSound.Play();
            StartCoroutine(base.WinGame());
            GameManagerCheck.Instance.FinishGame();
        }
    }
}