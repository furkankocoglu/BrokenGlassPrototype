using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SceneCompleteChecker : MonoBehaviour
{
    [SerializeField]
    GameObject confetiPrefab = default;
    [SerializeField]
    GameObject lightPrefab = default;
    [SerializeField]
    GameObject completedObject = default;
    public int completedObjectCount = default;
    [SerializeField]
    List<Transform> referenceObjectList = new List<Transform>();
    public List<Transform> glassObjectList = new List<Transform>();
    [SerializeField]
    List<Transform> completedObjectWireList = new List<Transform>();
    public bool levelCompleted = false;
    bool wireOpened = false;
    public static SceneCompleteChecker instance = default;
    private void Awake()
    {
        instance = this;   
    }
    private void Start()
    {
        //ArrayToList(GameObject.FindGameObjectsWithTag("Glass"),glassObjectList);
        //ArrayToList(GameObject.FindGameObjectWithTag("MainObject").GetComponentsInChildren<Transform>(),referenceObjectList);
        //ArrayToList(completedObject.GetComponentsInChildren<Transform>(),completedObjectWireList);
        //referenceObjectList.Remove(GameObject.FindWithTag("MainObject").transform);
        //completedObjectWireList.Remove(completedObject.transform);
        EqualizeList(referenceObjectList,glassObjectList);
        EqualizeList(referenceObjectList, completedObjectWireList);
    }
    private void Update()
    {
       CountCompltededObjects();
    }    
    public void OpenWire()
    {    
        if (!completedObject.activeInHierarchy)
        {            
            if (SavedData.GetCoinData() >= 1 && !wireOpened && !levelCompleted)
            {
                completedObject.SetActive(true);
                SavedData.SetCoinData(SavedData.GetCoinData() - 1);
                UIScripts.instance.UpdateUI();
                wireOpened = true;
            }
            else if(!wireOpened && !levelCompleted)
            {
                UIScripts.instance.alert.SetActive(true);
            }
        }        

    }
    void CountCompltededObjects()
    {        
        completedObjectCount = 0;
        for (int i = 0; i < referenceObjectList.Count; i++)
        {
            if (glassObjectList[i].transform.localPosition == referenceObjectList[i].localPosition)
            {
                if (completedObject.activeInHierarchy)
                {
                    completedObjectWireList[i].gameObject.SetActive(false);
                }
                completedObjectCount++;
            }
        }
        if (completedObjectCount == referenceObjectList.Count && !levelCompleted)
        {
            levelCompleted = true;
            Instantiate(confetiPrefab, new Vector3(0f,1f,-8f) ,confetiPrefab.transform.rotation);
            Instantiate(lightPrefab, new Vector3(0f, 1f, -7f),lightPrefab.transform.rotation);
            SavedData.SetCoinData(SavedData.GetCoinData()+1);
            UIScripts.instance.UpdateUI();
            UIScripts.instance.gridCanvas.SetActive(false);
            UIScripts.instance.completedMessagePanel.SetActive(false);
            UIScripts.instance.levelCompletedPanel.SetActive(true);
            
        }        
        
    }
    /// <summary>
    /// Girilen GameObject dizisini girilen Transform listesine ekler
    /// </summary>
    /// <param name="array"></param>
    /// <param name="list"></param>
    void ArrayToList(GameObject[] array,List<Transform> list)
    {
        foreach (var item in array)
        {
            list.Add(item.transform);
        }
    }
    /// <summary>
    /// Girilen Transform dizisini girilen Transform listesine ekler
    /// </summary>
    /// <param name="array"></param>
    /// <param name="list"></param>
    void ArrayToList(Transform[] array, List<Transform> list)
    {
        foreach (var item in array)
        {
            list.Add(item);
        }
    }
    /// <summary>
    /// Ayný isimleri içeren listeyi liste1'e göre sýralar
    /// </summary>
    /// <param name="list1"></param>
    /// <param name="list2"></param>
    void EqualizeList(List<Transform> list1,List<Transform> list2)
    {
        for (int i = 0; i < list1.Count; i++)
        {
            for (int j = 0; j < list2.Count; j++)
            {
                if (list1[i].name == list2[j].name)
                {
                    Transform empty = list2[i];
                    list2[i] = list2[j];
                    list2[j] = empty;
                    break;
                }
            }
        }
    }
}
