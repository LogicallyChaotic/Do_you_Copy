using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    [SerializeField] private TMP_Text _levelName;
    [SerializeField] private List<Button> _levelBtns;

    public void Start()
    {
        if (_levelName != null)
        {
            _levelName.text = "Level " + SceneManager.GetActiveScene().buildIndex;
        }


        if (!PlayerPrefs.HasKey("level"))
        {
            PlayerPrefs.SetInt("level", 1);
        }
    }

    public void Update()
    {

        if (_levelBtns.Count > 0)
        {
            for (int i = 0; i < PlayerPrefs.GetInt("level"); i++)
            {
                _levelBtns[i].interactable = true;
            }
        }

    }


    public void LevelChange(int index)
    {
        StartCoroutine(c_levelChange(index));
    }

    public IEnumerator c_levelChange(int index)
    {
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene(index);
    }
}
