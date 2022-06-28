using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvents
{
    public static Action<int, object, bool> OnKeyPressed = (keyHoldertoFill, keyPressed, filled) => { };
    public static Action<Queue<KeyPressedEnum>, int> OnTurnStart = (sequenceOfKeys,totalMoves) => { };
    public static Action<Queue<KeyPressedEnum>> OnTurnContinue = (routeTotake) => { };
}
