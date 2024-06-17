using System.Collections.Generic;
using UnityEngine;

public class BodyPartSO : ScriptableObject
{
    public Station.ModuleType selfType;

    public Sprite BodySprite;
    public Sprite Icon;

    public string Name;
    public string Info;
    
    public int Hp;
}
