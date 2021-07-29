using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SavedData
{
    public static string firstStartKey = "FirstStart";
    public static string coinKey = "Coin";

    public static int GetFirstStartData()
    {
        return PlayerPrefs.GetInt(firstStartKey);
    }
    public static void SetFirstStartData(int data)
    {
        PlayerPrefs.SetInt(firstStartKey, data);
    }
    public static int GetCoinData()
    {
        return PlayerPrefs.GetInt(coinKey);
    }
    public static void SetCoinData(int data)
    {
        PlayerPrefs.SetInt(coinKey,data);
    }
}
