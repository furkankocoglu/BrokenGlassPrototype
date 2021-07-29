using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        if (!PlayerPrefs.HasKey(SavedData.firstStartKey))
        {
            FirstStart();
        }
        UIScripts.instance.UpdateUI();
    }
    void FirstStart()
    {
        SavedData.SetFirstStartData(1);
        SavedData.SetCoinData(0);        
    }
   
}
