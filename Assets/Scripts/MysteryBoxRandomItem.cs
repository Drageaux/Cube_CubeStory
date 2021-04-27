using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MysteryBoxRandomItem : MonoBehaviour
{
    public string getRandomItem(string[] list)
    {
        int randomIndex = getRandomIndex(0, list.Length);
        return list[randomIndex];
    }

    public int getRandomIndex(int start, int end)
    {
        return new System.Random().Next(start, end);
    }
}
