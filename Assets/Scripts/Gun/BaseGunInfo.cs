using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(menuName = "Gun Info")]
public class BaseGunInfo : ScriptableObject
{
    public int bullets, totalBullets, damage;
    public float reloadTime, attackRange, intervalBetweenShots;
    public LayerMask targetMask;
}
