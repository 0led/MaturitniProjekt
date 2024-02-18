using UnityEngine;

public class Rotate : MonoBehaviour
{
    private PlayerMovement playerMovementScript;

    private void Start()
    {
        // Najděte hráče v hierarchii, ke kterému je tento objekt přiřazen
        GameObject player = transform.root.gameObject; // Získá nejvyššího rodiče v hierarchii

        // Získání skriptu PlayerMovement z přiřazeného hráče
        playerMovementScript = player.GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (playerMovementScript != null)
        {
            // Rozlišení mezi Player1 a Player2 pomocí tagu nebo nějakého jiného identifikátoru
            bool isFacingRight;
            if (playerMovementScript.gameObject.CompareTag("Player1"))
            {
                isFacingRight = playerMovementScript.GetFacingRightP1();
            }
            else // Předpokládá se, že jde o Player2
            {
                isFacingRight = playerMovementScript.GetFacingRightP2();
            }

            // Nastavení lokální rotace objektu na základě orientace hráče
            transform.localEulerAngles = isFacingRight ? Vector3.zero : new Vector3(0, 180, 0);
        }
    }
}
