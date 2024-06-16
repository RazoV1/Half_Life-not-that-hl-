using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Station : MonoBehaviour
{
    private GameObject Instantiator;
    
    public SpaceshipReader Spaceship;
    public Coordinates LocalCoordinates;

    [SerializeField] private GameObject ListModules;

    [ContextMenu("Load")]
    public void LoadField()
    {
        Spaceship = JsonUtility.FromJson<SpaceshipReader>(File.ReadAllText("Assets/Player.json"));
        LocalCoordinates = JsonUtility.FromJson<Coordinates>(File.ReadAllText("Assets/Resources/JSONS_Body/" + Spaceship.Body + ".json"));
    }
    

    
    [ContextMenu("Save")]
    public void SaveField()
    {
        File.WriteAllText("Assets/Player.json", JsonUtility.ToJson(Spaceship));
    }
    
    [System.Serializable]
    public class SpaceshipReader
    {
        public string Body;

        public string[] Guns;
    }
    
    [System.Serializable]
    public class Coordinates
    {
        public CoordinatesVector[] Guns;
        public CoordinatesVector Engines;
    }
    
    [System.Serializable]
    public class CoordinatesVector
    {
        public float x;
        public float y;
        public float z;
    }
    

    private void Start()
    {
        LoadField();

        Instantiator = Resources.Load<GameObject>("Instantiator");
        
        //Load Body
        Sprite bodySprite = Resources.Load<Sprite>("Body/" + Spaceship.Body);

        GameObject body = Instantiate(Instantiator);
        body.name = "Body";
        body.transform.position = Vector3.zero;
        body.GetComponent<SpriteRenderer>().sprite = bodySprite;
        body.GetComponent<SpriteRenderer>().sortingOrder = 10;
        
        //Load Guns
        
        for (int i = 0; i < Spaceship.Guns.Length; i++)
        {
            if(Spaceship.Guns[i] == "None") continue;
            
            Sprite gunSprite = Resources.Load<Sprite>("Gun/" + Spaceship.Guns[i]);
            
            
            GameObject gun = Instantiate(Instantiator);
            gun.name = Spaceship.Guns[i] + "_" + i.ToString();
            gun.transform.parent = body.transform;
            gun.GetComponent<SpriteRenderer>().sprite = gunSprite;
            gun.GetComponent<SpriteRenderer>().sortingOrder = 20;
            
            gun.transform.position = new Vector3(LocalCoordinates.Guns[i].x, LocalCoordinates.Guns[i].y, 0);
        }
    }

    // 0 - Guns
    // 1 - Shields
    public void OpenModulesMenu(int index)
    {
        
    }

}


