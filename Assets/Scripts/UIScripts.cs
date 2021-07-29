using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScripts:MonoBehaviour
{
    public GameObject alert = default;
    public GameObject coin = default;
    public GameObject gridCanvas = default;
    public GameObject levelCompletedPanel = default;
    public GameObject completedMessagePanel = default;
    public static UIScripts instance;
    private void Awake()
    {
        instance = this;
    }
    public void alertCloseUI()
    {
        alert.SetActive(false);
    }
    public void UpdateUI()
    {
        coin.transform.GetChild(0).GetComponent<Text>().text = SavedData.GetCoinData().ToString();
    }
}
