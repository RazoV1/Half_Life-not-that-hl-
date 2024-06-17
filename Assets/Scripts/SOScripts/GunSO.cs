using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "SO/Gun")]
public class GunSO : BodyPartSO
{
    public Sprite BulletSprite;
    
    public int Damage;
    public int AttackSpeed;
    
}
