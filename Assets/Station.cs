using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.UI;

public class Station : MonoBehaviour
{
    private GameObject Instantiator;
    
    public SpaceshipReader Spaceship;
    public Coordinates LocalCoordinates;

    [SerializeField] private GameObject ListModules;
    [SerializeField] private GameObject CellEmpty;
    [SerializeField] private GameObject CellFile;

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
        Sprite bodySprite = Resources.Load<BodySO>("SO/Bodies/" + Spaceship.Body).BodySprite;

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
        for(int j = 1; j < ListModules.transform.childCount; j++)
        {
            Destroy(ListModules.transform.GetChild(j).gameObject);
        }
        switch (index)
        {
            case 0:
                string[] gunStrings = GameManager.Instance.inventoryModules.Guns;
                List<GunSO> gunSOs = new List<GunSO>();
                
                for (int i = 0; i < gunStrings.Length; i++)
                {
                    gunSOs.Add(Resources.Load<GunSO>("SO/Guns/" + gunStrings[i]));
                }

                int count = 0;
                
                foreach (var gun in gunSOs)
                {
                    GameObject module = Instantiate(CellFile);
                    
                    module.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = gun.Icon;
                    module.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = gun.Name;
                    module.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = gun.Info;
                    
                    module.transform.SetParent(ListModules.transform);

                    module.GetComponent<RectTransform>().localScale = Vector3.one;


                    count++;
                }

                for (int k = count; k < 6; k++)
                {
                    GameObject emptyCell = Instantiate(CellEmpty);
                    emptyCell.transform.SetParent(ListModules.transform);
                    emptyCell.GetComponent<RectTransform>().localScale = Vector3.one;
                }

                ListModules.SetActive(true);
                break;
                
        }
    }

}


