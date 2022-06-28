using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VisualizerTog : MonoBehaviour
{
    [SerializeField] private int _finalLevelNum;
    private bool _setHelp;
    [SerializeField]private Toggle _tog;

    public void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == _finalLevelNum)
        {
            _tog.gameObject.SetActive(false);
        }
        else
        {
            _tog.gameObject.SetActive(true);
        }
    }

    public void VisualizerChange()
    {
        ConstantsManager.Instance.VisualizerON =! ConstantsManager.Instance.VisualizerON;

        if (ConstantsManager.Instance.VisualizerON)
        {
            ConstantsManager.Instance.isOverridden = true;
            ConstantsManager.Instance.isHard = true;

            if (GameManagerCheck.Instance != null)
            {
                StartCoroutine(GameManagerCheck.Instance.starAnimReset());
            }
        }
        else
        {
            ConstantsManager.Instance.isOverridden = true;
            ConstantsManager.Instance.isHard = false;
            if (GameManagerCheck.Instance != null)
            {
                StartCoroutine(GameManagerCheck.Instance.starAnimReset());
            }
        }
    }
}

