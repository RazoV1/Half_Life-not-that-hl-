using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ModuleSlot : MonoBehaviour, IPointerClickHandler
{
    public Station.ModuleType selfType;
    public int SlotID;

    public bool mouseOn;

    private Button selfButton;
    
    private void Start()
    {
        selfButton = GetComponent<Button>();
        selfButton.onClick.AddListener(delegate{GetItemFromDraggable();});
        selfButton.onClick.AddListener(delegate{Station.Instance.OpenModulesMenu(selfType);});
    }

    public void DrawSlot()
    {
        switch (selfType)
        {
            case Station.ModuleType.Gun:
                if (Station.Instance.Spaceship.Guns.Length <= SlotID)
                {
                    GetComponent<Button>().interactable = false;
                    break;
                }
                else if (Station.Instance.Spaceship.Guns[SlotID] == "None")
                {
                    transform.GetChild(0).gameObject.SetActive(false);
                    break;
                }
                transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<GunSO>("SO/Guns/" + Station.Instance.Spaceship.Guns[SlotID]).Icon;
                
                break;
        }
    }

    public void RestoreSlot()
    {
        switch (selfType)
        {
            case Station.ModuleType.Gun:
                Station.Instance.Spaceship.Guns[SlotID] = "None";
                Station.Instance.SaveField();
                Station.Instance.DrawSpaceship();
                Station.Instance.UpdateSlots();
                break;
        }
    }
    
    private void GetItemFromDraggable()
    {
        if (DraggableItem.Instance.currentItem != null)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(0).GetComponent<Image>().sprite =
                DraggableItem.Instance.currentItem.GetComponent<SpriteRenderer>().sprite;

            selfType = DraggableItem.Instance.selfType;

            switch (selfType)
            {
                case Station.ModuleType.Gun:
                    Station.Instance.Spaceship.Guns[SlotID] = DraggableItem.Instance.bodyPartSO.name;
                    break;
            }
            Station.Instance.SaveField();
            Station.Instance.DrawSpaceship();
            
            ModuleSlot[] slots = GameObject.FindObjectsOfType<ModuleSlot>();
            foreach (var slot in slots)
            {
                slot.transform.GetChild(1).gameObject.SetActive(false);
            }
            
            DraggableItem.Instance.Restore();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            RestoreSlot();
        }
    }
}
