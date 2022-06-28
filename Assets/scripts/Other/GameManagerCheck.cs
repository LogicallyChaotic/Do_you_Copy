using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerCheck : Singleton<GameManagerCheck>
{
    public List<FinalTile> finalTiles;
    [SerializeField] private bool isLastPuzzle = false;
    private PlayerCharacter _playerChar;
    private CopyCharacter _copyChar;
    public bool Finished;
    [SerializeField] private Animator _starAnim;
    [SerializeField] private bool _firstLevel;
    protected override void Awake()
    {
        base.Awake();
        FinalTile[] tempArrayfinalTiles = FindObjectsOfType<FinalTile>();
        foreach(FinalTile tile in tempArrayfinalTiles)
        {
            finalTiles.Add(tile);
        }
        _playerChar = FindObjectOfType<PlayerCharacter>();
        _copyChar = FindObjectOfType<CopyCharacter>();

    }

    public void Update()
    {

        if(!ConstantsManager.Instance.startGame)
        {
            _starAnim.speed = 0;
        }
        else
        {
            _starAnim.speed = 1;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(starAnimReset());
        }

        if (finalTiles.Count > 0)
        {
            for (int i = 0; i < finalTiles.Count; i++)
            {
                if (finalTiles[i].isEntityDone == true)
                {
                    continue;
                }
                else
                {
                    return;
                }
            }
        }
        Finished = true;
           
        
    }
    public void FinishGame()
    {
        if (!isLastPuzzle)
        {
            if (!_firstLevel)
            {
                StartCoroutine(starAnimNext());
            }
            else
            {
                StartCoroutine(starAnimMainMenu("Mainmenu"));
            }
        }
        else
        {
            StartCoroutine(starAnimMainMenu("EndScene"));
        }
    }
    public IEnumerator starAnim()
    {
        yield return new WaitForSeconds(1.5f);
        _starAnim.SetTrigger("Win");
        yield return new WaitForSeconds(0.4f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public IEnumerator starAnimReset()
    {
        yield return new WaitForSeconds(0.2f);
        _starAnim.SetTrigger("Win");
        yield return new WaitForSeconds(0.4f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public IEnumerator starAnimNext()
    {
        yield return new WaitForSeconds(1.5f);
        _starAnim.SetTrigger("Win");
        yield return new WaitForSeconds(0.4f);
        ConstantsManager.Instance.resetGame();
        setplayerpref();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public IEnumerator starAnimMainMenu(string name)
    {
        yield return new WaitForSeconds(1.5f);
        _starAnim.SetTrigger("Win");
        ConstantsManager.Instance.resetGame();
        yield return new WaitForSeconds(0.4f);
        SceneManager.LoadScene(name);
    }

    public void setplayerpref()
    {

        if (PlayerPrefs.GetInt("level") < SceneManager.GetActiveScene().buildIndex + 1 )
        {
            PlayerPrefs.SetInt("level", SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
