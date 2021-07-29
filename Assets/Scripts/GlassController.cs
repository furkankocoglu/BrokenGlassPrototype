using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GlassController
{
    public class GlassController : MonoBehaviour
    {        
        [SerializeField]
        Transform[] referenceObjects = default;
        [SerializeField]
        Transform[] glassShards = default;
        Camera _camera;
        GameObject selectedObject = default;
        [SerializeField]
        float mouseZPos = default;
        [SerializeField]
        float minX = default;
        [SerializeField]
        float minY = default;
        [SerializeField]
        float maxX = default;
        [SerializeField]
        float maxY = default;
        List<Vector3> glassShardPos = new List<Vector3>();
        List<Quaternion> glassShardRot = new List<Quaternion>();
        string[] messages = { "Awesome", "Wonderful", "Excellent", "Fine", "Cool", "Good" };
        private void Start()
        {
            _camera = Camera.main;
            foreach (Transform item in glassShards)
            {
                glassShardPos.Add(item.position);
                glassShardRot.Add(item.rotation);
            }
        }
        private void Update()
        {
            if (!SceneCompleteChecker.instance.levelCompleted)
            {
                GlassTouch();
            }
            
        }
        private void GlassTouch()
        {
            RaycastHit hit;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            var physicsRay = Physics.Raycast(ray, out hit, Mathf.Infinity);
            if (Input.GetMouseButtonDown(0) && physicsRay)
            {
                if (hit.collider.gameObject.CompareTag("Glass") && selectedObject==null)
                {
                    foreach (var item in referenceObjects)
                    {    
                        if (item.localPosition!=hit.collider.transform.localPosition && item.name.Equals(hit.collider.gameObject.name))
                        {
                            selectedObject = hit.collider.gameObject;
                            UIScripts.instance.gridCanvas.SetActive(false);
                            SetGlassShard(false);
                        }
                    }
                    
                }
            }
            else if (Input.GetMouseButton(0) && selectedObject!=null)
            {
                // Debug.Log("Working Raycast GetMouseButton");
                selectedObject.transform.position = new Vector3(Mathf.Clamp(MousePos().x,minX,maxX),Mathf.Clamp(MousePos().y,minY,maxY),referenceObjects[0].transform.position.z);  
            }
            else if(Input.GetMouseButtonUp(0) && selectedObject!=null)
            {
                SetGlassShard(true);
                SetObjectToReferencePos();                
                UIScripts.instance.gridCanvas.SetActive(true);
                
                                
            }
        }
        void SetGlassShard(bool state)
        {
            int i = 0;
            foreach (Transform item in SceneCompleteChecker.instance.glassObjectList)
            {
                if (item != selectedObject.transform && item.position==glassShardPos[i])
                {
                    item.gameObject.SetActive(state);
                }
                i++;
            }
        }
        IEnumerator RandomMessage()
        {
            UIScripts.instance.completedMessagePanel.GetComponentInChildren<Text>().text = messages[UnityEngine.Random.Range(0, messages.Length)];
            UIScripts.instance.completedMessagePanel.SetActive(true);
            yield return new WaitForSeconds(1f);
            UIScripts.instance.completedMessagePanel.SetActive(false);
        }
        private void SetObjectToReferencePos()
        {
            int i = 0;
            foreach (var item in referenceObjects)
            {
                if (selectedObject.name.Equals(item.name))
                {                    
                    if (Vector2.Distance(selectedObject.transform.localPosition, item.localPosition) < 0.5f)
                    {                       
                        selectedObject.transform.localPosition = item.localPosition;
                        selectedObject.transform.rotation = item.localRotation;
                        if (SceneCompleteChecker.instance.completedObjectCount<referenceObjects.Length)
                        {
                            StartCoroutine(RandomMessage());
                        }
                        
                    }
                    else
                    {
                        selectedObject.transform.position = glassShardPos[i];
                        selectedObject.transform.rotation = glassShardRot[i];
                    }
                }
                i++;
            }
            selectedObject = null;
        }
        private Vector3 MousePos()
        {
            return _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,mouseZPos));
        }
        
    }
}

