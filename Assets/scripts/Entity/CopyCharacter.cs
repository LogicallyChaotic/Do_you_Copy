using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyCharacter : Base_char
{
    private PlayerCharacter _playerChar;
    public bool isFinishedMoving = true;
    private void Awake()
    {
        _playerChar = FindObjectOfType<PlayerCharacter>();
    }
    protected override void OnEnable()
    {
        GameEvents.OnTurnContinue += StartTurn;
    }
    protected override void OnDisable()
    {
        GameEvents.OnTurnContinue -= StartTurn;
    }

    protected void StartTurn(Queue<KeyPressedEnum> sequence)
    {
        StartCoroutine(isMoving(sequence));
    }

    private IEnumerator isMoving(Queue<KeyPressedEnum> sequence)
    {
        isFinishedMoving = false;
        yield return StartCoroutine(c_startTurn(sequence));
        isFinishedMoving = true;

        if (!ConstantsManager.Instance.isHard)
        {
            yield return new WaitForSeconds(0.01f);
            isFinishedMoving = false;
       }
    }

    protected override void MidEndTurn(KeyPressedEnum tempkey)
    {
        base.MidEndTurn(tempkey);
    }
    public override bool EndTurn()
    {
        RaycastHit2D tempHit = Physics2D.Raycast(_groundCheck.transform.position, Vector2.zero, Mathf.Infinity, _tileLayer);
        if (!tempHit.collider)
        {
            StartCoroutine(LoseGame());
        }
        else if (tempHit.collider.CompareTag("Final") && GameManagerCheck.Instance.Finished && HUDmanager.Instance.compareMoves() && !_isDead)
        {
            _playerChar.checkEnd();
        }
        return true;
    }
}
