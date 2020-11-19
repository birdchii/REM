using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private KeyType keyType;
    [SerializeField] private int keyCost = 5;

    public enum KeyType
    {
        Red,
        Green,
        Blue,
        Orange,
        Teal,
        White,
        Black
    }

    public KeyType GetKeyType()
    {
        return keyType;
    }

    public int GetKeyCost()
    {
        return keyCost;
    }
}
