using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Base_char : MonoBehaviour
{
    [Header("movement variables")]
    [HideInInspector] public bool _hasWon;
    [HideInInspector] public bool _isDead;
    [SerializeField] private float _moveSpeed = 0.2f;
    [SerializeField] protected GameObject _groundCheck;
    [SerializeField] protected GameObject _nextSpaceTrigger;
    [SerializeField] protected LayerMask _tileLayer;
    [SerializeField] protected Animator _anim;
    [SerializeField] protected float _waitTimeBeforeEndTurn = 0.3f;
    [Header("particle effects")]
    [SerializeField] protected ParticleSystem _loseParticles;
    [SerializeField] protected ParticleSystem _winParticles;
    [Header("sound effects")]
    [SerializeField] protected AudioSource _walkSound;
    [SerializeField] protected AudioSource _loseSound;

    protected bool _isMoveable;
    protected Vector3 _nextLoc;
    protected Vector3 _velocty = Vector3.zero;
    protected Vector3 _raycastVector;
    protected const float HORIZONTALMOVEMENT = 1.9f;
    protected const float VERTICLEMOVEMENT = 1.6f;
    protected const float MOVEMENTMULTIPLIER = 3;

    protected virtual void OnEnable()
    {
        GameEvents.OnTurnStart += StartTurn;
    }
    protected virtual void OnDisable()
    {
        GameEvents.OnTurnStart -= StartTurn;
    }

    protected virtual void Start()
    {
        RaycastHit2D tempHit = Physics2D.Raycast(_nextSpaceTrigger.transform.position, Vector2.zero, Mathf.Infinity, _tileLayer);
        if (tempHit.collider != null)
        {
            this.transform.parent = tempHit.collider.transform;
            this.transform.localPosition = Vector3.zero;
        }
    }
    private void Update()
    {
        if (_isMoveable)
        {
            transform.localPosition = Vector3.SmoothDamp(transform.localPosition, _nextLoc, ref _velocty, _moveSpeed);
        }
    }
    protected virtual void StartTurn(Queue<KeyPressedEnum> sequence, int totalMoves)
    {
        StartCoroutine(c_startTurn(sequence));
    }

    protected virtual IEnumerator c_startTurn(Queue<KeyPressedEnum> sequence)
    {
        int tempTotalMoves = sequence.Count;
        for (int i = 0; i < tempTotalMoves; i++)
        {
            KeyPressedEnum tempKey = sequence.Dequeue();
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

            _nextSpaceTrigger.transform.localPosition = _raycastVector;
            RaycastHit2D tempHit = Physics2D.Raycast(_nextSpaceTrigger.transform.position, Vector2.zero, Mathf.Infinity, _tileLayer);

            if (tempHit.collider != null)
            {
                _nextLoc = new Vector3(0,0,0);
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
        }
    }

    protected virtual void MidTurn()
    {
        _isMoveable = true;
        _walkSound.Play();
    }
    protected virtual void MidEndTurn(KeyPressedEnum tempkey)
    {
        _isMoveable = false;
        RaycastHit2D tempHit = Physics2D.Raycast(_groundCheck.transform.position, Vector2.zero, Mathf.Infinity, _tileLayer);
        if (!tempHit.collider)
        {
            StartCoroutine(LoseGame());
        }
    }
    public virtual bool EndTurn() {
        return false;
    
    }
    public virtual IEnumerator LoseGame()
    {
        _isDead = true;
        yield return new WaitForSeconds(_waitTimeBeforeEndTurn);
        _loseParticles.Play();
        _anim.SetTrigger("Lose");
        StopAllCoroutines();
        StartCoroutine(GameManagerCheck.Instance.starAnim());
        _loseSound.Play();
    }

    public virtual IEnumerator WinGame()
    {
        _hasWon = true;
        _winParticles.Play();
        _anim.SetTrigger("Win");
        yield return new WaitForSeconds(0.2f);
    }
}
