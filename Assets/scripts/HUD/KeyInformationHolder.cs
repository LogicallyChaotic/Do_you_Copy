using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new KeyInfoHolder", menuName = "ScriptableObjects/KeyInfoHolder")]
public class KeyInformationHolder : ScriptableObject
{
    [System.Serializable]
    public class KeySet
    {
        public KeyPressedEnum keyPressedEnum;
        public KeyCode pressedKeycode;
        public Sprite keySprite;
    }

    public List<KeySet> keyPressInformation;
}
public enum KeyPressedEnum
{
    LEFT,
    RIGHT,
    UP,
    DOWN
};

