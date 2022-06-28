using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantsManager : Singleton<ConstantsManager>
{
    public bool startGame;
    public bool isHard = false;
    public bool isSet;
    public bool isOverridden = false;
    public bool VisualizerON = false;
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this.gameObject);
        startGame = true;
    }
    public void resetGame()
    {
        startGame = true;
    }
}
