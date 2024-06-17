using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UI;

public class CellModule : MonoBehaviour
{
    public Sprite sprite;

    public Station.ModuleType selfType;
    
    public BodyPartSO bodyPartSO;
    
    private GameObject moduleDragger;


    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate{AddItemToDraggable();});
    }

    public void AddItemToDraggable()
    {
        print("1");
        //throw new NotImplementedException();

        DraggableItem.Instance.selfType = selfType;
        DraggableItem.Instance.bodyPartSO = bodyPartSO;

        DraggableItem.Instance.currentItem = Instantiate(Station.Instance.Instantiator);
        DraggableItem.Instance.currentItem.GetComponent<SpriteRenderer>().sprite = sprite;
        DraggableItem.Instance.currentItem.GetComponent<SpriteRenderer>().sortingOrder = 140;
        DraggableItem.Instance.currentItem.GetComponent<SpriteRenderer>().rayTracingMode = RayTracingMode.Off;

        ModuleSlot[] slots = Station.Instance.ModuleSlots;
        foreach (var slot in slots)
        {
            if (slot.selfType == selfType && slot.GetComponent<Button>().interactable)
            {
                slot.transform.GetChild(1).gameObject.SetActive(true);
            }
        }

    }



}
