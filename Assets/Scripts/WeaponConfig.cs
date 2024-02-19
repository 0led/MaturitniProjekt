using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class WeaponConfig : ScriptableObject
{
    public float damage;
    public float range;
    public float speed;
}