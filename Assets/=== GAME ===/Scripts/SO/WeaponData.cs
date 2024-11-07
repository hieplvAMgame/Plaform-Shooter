using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Data", menuName = "SO/Create Weapon")]
public class WeaponData: ScriptableObject
{
    public int id;
    public GameObject projectile;
    public float fireRate;
    public int damage;
}