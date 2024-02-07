using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponPlayer : MonoBehaviour
{
   /*
   // Zničte stávající zbraň, pokud nějaká je
    foreach (Transform child in weaponHolder.transform)
    {
        Destroy(child.gameObject);
    }

    // Vytvořte novou instanci zbraně a umístěte ji do weaponHolder
    Instantiate(weaponPrefab, weaponHolder.transform.position, Quaternion.identity, weaponHolder.transform);
   */
   /*
    public Transform gunSprite; // Reference na GunSprite GameObject

    public void EquipWeapon(Sprite newWeaponSprite)
{
    gunSprite.GetComponent<SpriteRenderer>().sprite = newWeaponSprite;
}
    */

    
    public Transform weaponHolder; // Místo, kde bude zbraň umístěna (např. ruka hráče)

    public void EquipWeapon(GameObject weaponPrefab, bool facingRightP1)
    {
         // Zničte stávající zbraň, pokud nějaká je
        foreach (Transform child in weaponHolder) {
            Destroy(child.gameObject);
        }

        // Vytvořte novou instanci zbraně a umístěte ji do weaponHolder
        
        //float weaponDirection = facingRightP1 ? 0.25f : -0.25f;
        GameObject weaponInstance = Instantiate(weaponPrefab, weaponHolder.position, weaponHolder.rotation, weaponHolder);
        //weaponInstance.transform.localScale = new Vector3(weaponDirection, 0.25f, 0.25f);
        Vector3 originalScale = weaponInstance.transform.localScale;
        float scaleX = facingRightP1 ? Mathf.Abs(originalScale.x) : -Mathf.Abs(originalScale.x);
        weaponInstance.transform.localScale = new Vector3(scaleX, originalScale.y, originalScale.z);

        // Nastavte zde další vlastnosti zbraně, jako je rychlost střelby, poškození atd.

        // Případně zde můžete nastavit další vlastnosti zbraně, jako je poškození, rychlost střelby atd.

         // Otočení zbraně, pokud je hráč otočený doprava (předpokládá, že defaultní orientace zbraně je doleva)
        /*
        if (transform.localScale.x > 0)
        {
        // Otočení zbraně o 180 stupňů kolem osy Y, aby směřovala doprava
        weaponInstance.transform.Rotate(0, 180f, 0);
        */
       
        }
    }

