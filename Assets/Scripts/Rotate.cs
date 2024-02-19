using UnityEngine;

public class Rotate : MonoBehaviour
{
    private PlayerMovement playerMovementScript;

    private void Start()
    {
        GameObject player = transform.root.gameObject;

        playerMovementScript = player.GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (playerMovementScript != null)
        {
            bool isFacingRight;
            if (playerMovementScript.gameObject.CompareTag("Player1"))
            {
                isFacingRight = playerMovementScript.GetFacingRightP1();
            }
            else
            {
                isFacingRight = playerMovementScript.GetFacingRightP2();
            }
            transform.localEulerAngles = isFacingRight ? Vector3.zero : new Vector3(0, 180, 0);
        }
    }
}
