using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameManager Instance { get; private set; }

    public InventoryModules inventoryModules;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("More than one 'InventoryModules' by name: "+ gameObject.name);
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        LoadField();
    }
    
    [ContextMenu("Load")]
    public void LoadField()
    {
        inventoryModules = JsonUtility.FromJson<InventoryModules>(File.ReadAllText("Assets/JSONS/InventoryModules.json"));
    }
    
    [ContextMenu("Save")]
    public void SaveField()
    {
        File.WriteAllText("Assets/JSONS/InventoryModules.json", JsonUtility.ToJson(Instance));
    }
    
    [Serializable]
    public class InventoryModules
    {
        public string[] Bodies;
        public string[] Guns;
        public string[] Shields;
        public string[] Engines;
        public string[] LifeModules;
    }
}
