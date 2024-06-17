using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableItem : MonoBehaviour
{
    
    public static DraggableItem Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public GameObject currentItem;
    
    public Station.ModuleType selfType;
    
    public BodyPartSO bodyPartSO;

    private void Update()
    {
        if (currentItem != null)
        {
            currentItem.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
            currentItem.transform.position = new Vector3(currentItem.transform.position.x, currentItem.transform.position.y, 0f);
        }
    }

    public void Restore()
    {
        Destroy(currentItem);
        currentItem = null;
        bodyPartSO = null;
    }
}
