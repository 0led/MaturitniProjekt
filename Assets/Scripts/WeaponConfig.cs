using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class WeaponConfig : ScriptableObject
{
    public float damage;
    public float range;
    public float fireRate;
    // Můžete přidat další vlastnosti jako velikost zásobníku, doba přebíjení atd.
}