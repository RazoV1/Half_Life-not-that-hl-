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
                    selfButton.interactable = false;
                    break;
                }
                else if (Station.Instance.Spaceship.Guns[SlotID] == "None")
                {
                    transform.GetChild(0).gameObject.SetActive(false);
                    selfButton.interactable = true;
                    break;
                }
                transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<GunSO>("SO/Guns/" + Station.Instance.Spaceship.Guns[SlotID]).Icon;

                selfButton.interactable = true;
                
                break;
            
            case Station.ModuleType.Shield:
                if (Station.Instance.Spaceship.Shields.Length <= SlotID)
                {
                    selfButton.interactable = false;
                    break;
                }
                else if (Station.Instance.Spaceship.Shields[SlotID] == "None")
                {
                    transform.GetChild(0).gameObject.SetActive(false);
                    selfButton.interactable = true;
                    break;
                }
                transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<ShieldSO>("SO/Shields/" + Station.Instance.Spaceship.Shields[SlotID]).Icon;
                
                selfButton.interactable = true;
                
                break;
            
            case Station.ModuleType.Engine:
                if (Station.Instance.Spaceship.Engines.Length <= SlotID)
                {
                    selfButton.interactable = false;
                    break;
                }
                else if (Station.Instance.Spaceship.Engines[SlotID] == "None")
                {
                    transform.GetChild(0).gameObject.SetActive(false);
                    selfButton.interactable = true;
                    break;
                }
                transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<EngineSO>("SO/Engines/" + Station.Instance.Spaceship.Engines[SlotID]).Icon;
                
                selfButton.interactable = true;
                
                break;
            
            case Station.ModuleType.LifeModule:
                if (Station.Instance.Spaceship.LifeModules.Length <= SlotID)
                {
                    selfButton.interactable = false;
                    break;
                }
                else if (Station.Instance.Spaceship.LifeModules[SlotID] == "None")
                {
                    transform.GetChild(0).gameObject.SetActive(false);
                    selfButton.interactable = true;
                    break;
                }
                transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<LifeModuleSO>("SO/LifeModules/" + Station.Instance.Spaceship.LifeModules[SlotID]).Icon;
                
                selfButton.interactable = true;
                
                break;
        }
    }

    public void RestoreSlot()
    {
        switch (selfType)
        {
            case Station.ModuleType.Gun:
                Station.Instance.Spaceship.Guns[SlotID] = "None";
                break;
            
            case Station.ModuleType.Shield:
                Station.Instance.Spaceship.Shields[SlotID] = "None";
                break;
            
            case Station.ModuleType.Engine:
                Station.Instance.Spaceship.Engines[SlotID] = "None";
                break;
            
            case Station.ModuleType.LifeModule:
                Station.Instance.Spaceship.LifeModules[SlotID] = "None";
                break;
        }
        Station.Instance.SaveField();
        Station.Instance.DrawSpaceship();
        Station.Instance.UpdateSlots();
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
                
                case Station.ModuleType.Shield:
                    Station.Instance.Spaceship.Shields[SlotID] = DraggableItem.Instance.bodyPartSO.name;
                    break;
                
                case Station.ModuleType.Engine:
                    Station.Instance.Spaceship.Engines[SlotID] = DraggableItem.Instance.bodyPartSO.name;
                    break;
                
                case Station.ModuleType.LifeModule:
                    Station.Instance.Spaceship.LifeModules[SlotID] = DraggableItem.Instance.bodyPartSO.name;
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

            Station.Instance.UpdateSlots();
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
