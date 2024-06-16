using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spaceship", menuName = "SO/Spaceship")]
public class SpaceshipSO : ScriptableObject
{
    public Sprite BodySprite;
    
    public List<GunSO> Guns = new List<GunSO>();
}
