using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spaceship", menuName = "SO/Body")]
public class BodySO : ScriptableObject
{
    public Sprite BodySprite;
    
    public List<GunSO> Guns = new List<GunSO>();
}
