using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpeedBoost : MonoBehaviour
{
    public float speedBoostAmount = 3.0f;
    public float boostDuration = 5.0f;
    private GameObject potentialPicker; // Hráč, který má možnost sebrat power-up

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player1") || collision.CompareTag("Player2"))
        {
            potentialPicker = collision.gameObject; // Uložíme si potenciálního hráče
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == potentialPicker)
        {
            potentialPicker = null; // Vynulujeme potenciálního hráče, pokud opustí collider
        }
    }

    private void Update()
    {
        if (potentialPicker != null)
        {
            // Podmínka pro sebrání power-upu příslušným tlačítkem pro každého hráče
            if ((potentialPicker.CompareTag("Player1") && Input.GetKeyDown(KeyCode.S)) || 
                (potentialPicker.CompareTag("Player2") && Input.GetKeyDown(KeyCode.DownArrow)))
            {
                PlayerMovement playerMovement = potentialPicker.GetComponent<PlayerMovement>();
                if (playerMovement != null && !playerMovement.IsSpeedBoosted)
                {
                    // Zde spustíme coroutine a nastavíme IsSpeedBoosted na true v metodě ApplySpeedBoost
                    playerMovement.ApplySpeedBoost(speedBoostAmount, boostDuration);
                }
                Destroy(gameObject); // Zničíme power-up objekt
            }
        }
    }

    // Zde přidáme metodu ApplySpeedBoost, která byla původně v PlayerMovement
    private IEnumerator ApplySpeedBoost(PlayerMovement playerMovement)
    {
        playerMovement.IsSpeedBoosted = true;
        float originalSpeed = playerMovement.moveSpeed;
        playerMovement.moveSpeed += speedBoostAmount;

        yield return new WaitForSeconds(boostDuration);

        if (playerMovement != null)
        {
            playerMovement.moveSpeed = originalSpeed;
            playerMovement.IsSpeedBoosted = false;
        }
    }
    
}



    /*
    public float speedBoostAmount = 3.0f;
    public float boostDuration = 5.0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
         PlayerMovement playerMovement = collision.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.ApplySpeedBoost(speedBoostAmount, boostDuration);
            Destroy(gameObject); // Zničíme power-up objekt ihned po sebrání
        }
    }

    private IEnumerator ApplySpeedBoost(PlayerMovement playerMovement)
    {
        playerMovement.IsSpeedBoosted = true;
        float originalSpeed = playerMovement.moveSpeed;
        playerMovement.moveSpeed += speedBoostAmount;

        yield return new WaitForSeconds(boostDuration);

        if (playerMovement != null && playerMovement.gameObject.activeInHierarchy)
        {
            playerMovement.moveSpeed = originalSpeed;
            playerMovement.IsSpeedBoosted = false;
        }

        // Coroutine skončila, takže ji odstraníme z hráče
        playerMovement.SpeedBoostCoroutine = null;
    }
}
*/






/*

    public float speedBoostAmount = 3.0f;
    public float boostDuration = 5.0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("Player1") || collision.CompareTag("Player2")))
        {
            PlayerMovement playerMovement = collision.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                StartCoroutine(ApplySpeedBoost(playerMovement));
            }
            //Destroy(gameObject); // Zničíme power-up objekt ihned po sebrání
        }
    }

    private IEnumerator ApplySpeedBoost(PlayerMovement playerMovement)
    {
        float originalSpeed = playerMovement.moveSpeed; // Uložíme původní rychlost
        Debug.Log("Boost applied, original speed: " + originalSpeed);
        playerMovement.moveSpeed += speedBoostAmount; // Zvýšíme rychlost pohybu
        Debug.Log("Speed after boost: " + playerMovement.moveSpeed);

        yield return new WaitForSeconds(boostDuration); // Počkáme určenou dobu

        if (playerMovement != null) // Kontrola, zda hráč ještě existuje
        {
            playerMovement.moveSpeed = originalSpeed; // Obnovíme rychlost na původní hodnotu
            Debug.Log("Speed restored to original: " + playerMovement.moveSpeed);
        }
    }
}

  /*
  
    public float speedBoostAmount = 3.0f;
    public float boostDuration = 5.0f;
    private static bool boostActive = false; // Statická proměnná pro sledování aktivního boostu

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("Player1") || collision.CompareTag("Player2")) && !boostActive)
        {
            boostActive = true;
            PlayerMovement playerMovement = collision.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                StartCoroutine(ApplySpeedBoost(playerMovement));
            }
        }
    }

    private IEnumerator ApplySpeedBoost(PlayerMovement playerMovement)
    {
        float originalSpeed = playerMovement.moveSpeed; // Uložíme původní rychlost
        playerMovement.moveSpeed += speedBoostAmount; // Zvýšíme rychlost pohybu

        yield return new WaitForSeconds(boostDuration); // Počkáme určenou dobu

        if (playerMovement != null)
        {
            playerMovement.moveSpeed = originalSpeed; // Obnovíme rychlost na původní hodnotu
        }
        boostActive = false; // Resetujeme stav aktivního boostu
        Destroy(gameObject); // Zničíme objekt power-upu
    }
}
   /*
    public float speedBoostAmount = 3.0f;
    public float boostDuration = 5.0f;
    private PlayerMovement playerMovement; // Přidejte proměnnou pro ukládání odkazu na PlayerMovement
    private bool boostActive = false; // Přidejte proměnnou pro sledování, zda je boost aktivní

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("Player1") || collision.CompareTag("Player2")) && !boostActive)
        {
            playerMovement = collision.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                StartCoroutine(ApplySpeedBoost());
            }
        }
    }

    private IEnumerator ApplySpeedBoost()
    {
        if (boostActive)
        {
            yield break; // Pokud je již boost aktivní, ukončíme tuto instanci coroutine
        }

        boostActive = true;
        float originalSpeed = playerMovement.moveSpeed; // Uložíme původní rychlost
        playerMovement.moveSpeed += speedBoostAmount; // Zvýšíme rychlost pohybu

        yield return new WaitForSeconds(boostDuration); // Počkáme určenou dobu

        playerMovement.moveSpeed = originalSpeed; // Obnovíme rychlost na původní hodnotu
        boostActive = false; // Nastavíme, že boost již není aktivní
        Destroy(gameObject); // Zničíme objekt power-upu
    }
}



    /*
    public float speedBoostAmount = 3.0f;
    public float boostDuration = 5.0f;
    private bool isBoostActive = false; // Přidali jsme novou proměnnou pro kontrolu stavu

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("Player1") || collision.CompareTag("Player2")) && !isBoostActive)
        {
            PlayerMovement playerMovement = collision.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                StartCoroutine(ApplySpeedBoost(playerMovement));
            }
        }
    }

    private IEnumerator ApplySpeedBoost(PlayerMovement playerMovement)
    {
        if (isBoostActive)
        {
            yield break; // Ukončíme coroutine, pokud už je aktuálně aktivní
        }

        isBoostActive = true;
        float originalSpeed = playerMovement.moveSpeed;
        playerMovement.moveSpeed += speedBoostAmount;

        yield return new WaitForSeconds(boostDuration);

        if (playerMovement != null)
        {
            playerMovement.moveSpeed = originalSpeed; // Obnovíme původní rychlost
        }

        isBoostActive = false;
    }
}

   /*
   public float speedBoostAmount = 3.0f;
    public float boostDuration = 5.0f;
    private static bool isBoostActive = false;
    private static float originalSpeed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement playerMovement = collision.GetComponent<PlayerMovement>();
        if (playerMovement != null && (collision.CompareTag("Player1") || collision.CompareTag("Player2")))
        {
            if (!isBoostActive)
            {
                originalSpeed = playerMovement.moveSpeed; // Uložíme původní rychlost pouze pokud boost ještě nebyl aktivován
                isBoostActive = true;
            }
            StartCoroutine(ApplySpeedBoost(playerMovement));
            Destroy(gameObject); // Odstraní power-up objekt ihned po sebrání
        }
    }

    private IEnumerator ApplySpeedBoost(PlayerMovement playerMovement)
    {
        playerMovement.moveSpeed += speedBoostAmount;

        yield return new WaitForSeconds(boostDuration);

        // Zkontrolujeme, jestli nebyl mezitím aktivován jiný boost
        if (isBoostActive)
        {
            playerMovement.moveSpeed = originalSpeed; // Vrátíme rychlost na původní hodnotu
            isBoostActive = false; // Resetujeme stav boostu
        }
    }
}

   /*
    public float speedBoostAmount = 3.0f;  // O kolik se zvýší rychlost pohybu
    public float boostDuration = 5.0f;  // Doba trvání zvýšené rychlosti
    //private GameObject potentialPicker;  // Hráč, který má možnost sebrat power-up

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player1") || collision.CompareTag("Player2"))
        {
            PlayerMovement playerMovement = collision.GetComponent<PlayerMovement>();

            if (playerMovement != null)
            {
                StartCoroutine(ApplySpeedBoost(playerMovement));
                Destroy(gameObject);  // Odstraní power-up objekt ihned po sebrání
            }
        }
    }

    private IEnumerator ApplySpeedBoost(PlayerMovement playerMovement)
    {
        float originalSpeed = playerMovement.moveSpeed;  // Uložíme původní rychlost
        Debug.Log("Applying Speed Boost. Original speed: " + originalSpeed);
        playerMovement.moveSpeed += speedBoostAmount;  // Zvýšíme rychlost pohybu
        Debug.Log("Speed Boosted to: " + playerMovement.moveSpeed);

        yield return new WaitForSeconds(boostDuration);  // Počkáme určenou dobu

        playerMovement.moveSpeed = originalSpeed;  // Obnovíme rychlost na původní hodnotu
         Debug.Log("Speed Restored to: " + playerMovement.moveSpeed);
    }
}

/*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player1") || collision.CompareTag("Player2"))
        {
            potentialPicker = collision.gameObject;  // Uložíme si potenciálního hráče
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == potentialPicker)
        {
            potentialPicker = null;  // Vynulujeme potenciálního hráče, pokud opustí collider
        }
    }

    private void Update()
    {
        if (potentialPicker != null && 
            ((potentialPicker.CompareTag("Player1") && Input.GetKeyDown(KeyCode.S)) || 
            (potentialPicker.CompareTag("Player2") && Input.GetKeyDown(KeyCode.DownArrow))))
        {
            PlayerMovement playerMovement = potentialPicker.GetComponent<PlayerMovement>();

            if (playerMovement != null)
            {
                StartCoroutine(ApplySpeedBoost(playerMovement));
            }

            Destroy(gameObject);  // Odstraní power-up objekt
        }
    }

    private IEnumerator ApplySpeedBoost(PlayerMovement playerMovement)
    {
        float originalSpeed = playerMovement.moveSpeed;  // Uložíme původní rychlost
        playerMovement.moveSpeed += speedBoostAmount;  // Zvýšíme rychlost pohybu

        yield return new WaitForSeconds(boostDuration);  // Počkáme určenou dobu

        playerMovement.moveSpeed = originalSpeed;  // Obnovíme rychlost na původní hodnotu
    }
}


    /*
    public float speedBoostAmount = 20.0f;  // Hodnota, o kterou se zvýší rychlost pohybu
    public float boostDuration = 3.0f;  // Doba trvání zvýšené rychlosti
    private GameObject potentialPicker;  // Hráč, který má možnost sebrat power-up

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player1") || collision.CompareTag("Player2"))
        {
            potentialPicker = collision.gameObject;  // Uložíme si potenciálního hráče
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == potentialPicker)
        {
            potentialPicker = null;  // Vynulujeme potenciálního hráče, pokud opustí collider
        }
    }

    private void Update()
    {
        if (potentialPicker != null && 
            ((potentialPicker.CompareTag("Player1") && Input.GetKeyDown(KeyCode.S)) || 
            (potentialPicker.CompareTag("Player2") && Input.GetKeyDown(KeyCode.DownArrow))))
        {
            PlayerMovement playerMovement = potentialPicker.GetComponent<PlayerMovement>();

            if (playerMovement != null)
            {
                StartCoroutine(ApplySpeedBoost(playerMovement));
            }

            Destroy(gameObject);  // Odstraní power-up objekt
        }
    }

    private IEnumerator ApplySpeedBoost(PlayerMovement playerMovement)
    {
        float originalSpeed = playerMovement.moveSpeed;  // Uložíme původní rychlost
        playerMovement.moveSpeed += speedBoostAmount;  // Zvýšíme rychlost pohybu

        yield return new WaitForSeconds(boostDuration);  // Počkáme určenou dobu

        playerMovement.moveSpeed = originalSpeed;  // Obnovíme rychlost na původní hodnotu
    }

}
    
    /*
    
    public float speedBoostAmount = 10f; // Množství, kterým se zvýší rychlost hráče
    public float boostDuration = 3f; // Doba trvání efektu ve vteřinách
    private GameObject potentialPicker; // Hráč, který má možnost sebrat power-up
    private float originalSpeed; // Uložíme původní rychlost hráče
    private bool boostActive = false; // Kontrola, zda je speed boost aktivní

     public float speedBoostAmount = 5.0f;  // Hodnota, o kterou se zvýší rychlost pohybu
    public float boostDuration = 5.0f;  // Doba trvání zvýšené rychlosti

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player1") || collision.CompareTag("Player2"))
        {
            PlayerMovement playerMovement = collision.GetComponent<PlayerMovement>();

            if (playerMovement != null)
            {
                StartCoroutine(ApplySpeedBoost(playerMovement));
            }

            Destroy(gameObject);  // Odstraní power-up objekt ihned po sebrání
        }
    }

    private IEnumerator ApplySpeedBoost(PlayerMovement playerMovement)
    {
        float originalSpeed = playerMovement.moveSpeed;  // Uložíme původní rychlost
        playerMovement.moveSpeed += speedBoostAmount;  // Zvýšíme rychlost pohybu

        yield return new WaitForSeconds(boostDuration);  // Počkáme určenou dobu

        playerMovement.moveSpeed = originalSpeed;  // Obnovíme rychlost na původní hodnotu
    }
}


     
     /*
     void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player1") || collision.gameObject.CompareTag("Player2"))
        {
            potentialPicker = collision.gameObject; // Uložíme si potenciálního hráče, který může power-up sebrat
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == potentialPicker)
        {
            potentialPicker = null; // Vynulujeme potenciálního hráče, pokud opustí collider
        }
    }

    void Update()
    {
        if (potentialPicker != null && !boostActive) // Kontrola, zda je nějaký hráč v dosahu a zda boost není aktivní
        {
            // Podmínka pro sebrání power-upu příslušným tlačítkem pro každého hráče
            if ((potentialPicker.CompareTag("Player1") && Input.GetKeyDown(KeyCode.S)) || 
                (potentialPicker.CompareTag("Player2") && Input.GetKeyDown(KeyCode.DownArrow)))
            {
                PlayerMovement playerMovement = potentialPicker.GetComponent<PlayerMovement>();
                if (playerMovement != null)
                {
                    originalSpeed = playerMovement.moveSpeed; // Uložíme původní rychlost před aplikací boostu
                    StartCoroutine(ApplySpeedBoost(playerMovement)); // Aplikujeme speed boost
                }
            }
        }
    }

    private IEnumerator ApplySpeedBoost(PlayerMovement playerMovement)
    {
        boostActive = true; // Označíme, že boost je aktivní
        playerMovement.moveSpeed += speedBoostAmount; // Zvýšíme rychlost pohybu

        yield return new WaitForSeconds(boostDuration); // Počkáme určenou dobu

        playerMovement.moveSpeed = originalSpeed; // Obnovíme rychlost na původní hodnotu
        boostActive = false; // Označíme, že boost již není aktivní
        Destroy(gameObject); // Zničí power-up po použití
    }
}



/*

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player1") || collision.gameObject.CompareTag("Player2"))
        {
            potentialPicker = collision.gameObject; // Uložíme si potenciálního hráče, který může power-up sebrat
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == potentialPicker)
        {
            potentialPicker = null; // Vynulujeme potenciálního hráče, pokud opustí collider
        }
    }

    void Update()
    {
        if (potentialPicker != null) // Kontrola, zda je nějaký hráč v dosahu power-upu
        {
            // Podmínka pro sebrání power-upu příslušným tlačítkem pro každého hráče
            if ((potentialPicker.CompareTag("Player1") && Input.GetKeyDown(KeyCode.S)) || 
                (potentialPicker.CompareTag("Player2") && Input.GetKeyDown(KeyCode.DownArrow)))
            {
                PlayerMovement playerMovement = potentialPicker.GetComponent<PlayerMovement>();
                if (playerMovement != null)
                {
                    StartCoroutine(ApplySpeedBoost(playerMovement)); // Aplikujeme speed boost
                }
            }
        }
    }

    private IEnumerator ApplySpeedBoost(PlayerMovement playerMovement)
    {
        float originalSpeed = playerMovement.moveSpeed; // Uložíme původní rychlost
        playerMovement.moveSpeed += speedBoostAmount; // Zvýšíme rychlost pohybu

        yield return new WaitForSeconds(boostDuration); // Počkáme určenou dobu

        playerMovement.moveSpeed = originalSpeed; // Obnovíme rychlost na původní hodnotu
        Destroy(gameObject); // Zničí power-up po použití
    }
}


    /*
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player1") || collision.gameObject.CompareTag("Player2"))
        {
            PlayerMovement playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
            if (playerMovement != null && !playerMovement.IsSpeedBoosted)
            {
                StartCoroutine(ApplySpeedBoost(playerMovement));
            }
            gameObject.SetActive(false); // Deaktivujte nebo zničte Power-Up objekt
        }
    }

    private IEnumerator ApplySpeedBoost(PlayerMovement playerMovement)
    {
        float originalSpeed = playerMovement.moveSpeed; // Uložíme původní rychlost
        playerMovement.IsSpeedBoosted = true;
        playerMovement.moveSpeed += speedBoostAmount; // Zvýšíme rychlost pohybu

        yield return new WaitForSeconds(boostDuration); // Počkáme určenou dobu

        playerMovement.moveSpeed = originalSpeed; // Obnovíme rychlost na původní hodnotu
        playerMovement.IsSpeedBoosted = false;
    }

}
    //private GameObject potentialPicker; // Hráč, který má možnost sebrat power-up

   /*
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player1") || collision.gameObject.CompareTag("Player2"))
        {
            potentialPicker = collision.gameObject; // Uložíme si potenciálního hráče, který může power-up sebrat
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == potentialPicker)
        {
            potentialPicker = null; // Vynulujeme potenciálního hráče, pokud opustí collider
        }
    }

    void Update()
    {
        if (potentialPicker != null) // Kontrola, zda je nějaký hráč v dosahu power-upu
        {
            // Podmínka pro sebrání power-upu příslušným tlačítkem pro každého hráče
            if ((potentialPicker.CompareTag("Player1") && Input.GetKeyDown(KeyCode.S)) || 
                (potentialPicker.CompareTag("Player2") && Input.GetKeyDown(KeyCode.DownArrow)))
            {
                StartCoroutine(ApplySpeedBoost(potentialPicker));
                Destroy(gameObject);
            }
        }
    }

    IEnumerator ApplySpeedBoost(GameObject player)
    {
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();

        if (playerMovement != null)
        {
            playerMovement.moveSpeed += speedBoostAmount; // Zvýšíme rychlost hráče

            yield return new WaitForSeconds(boostDuration); // Počkáme určený čas

            playerMovement.moveSpeed -= speedBoostAmount; // Vrátíme rychlost hráče na původní hodnotu
            //Destroy(gameObject); // Zničíme power-up
        }
    }
}
*/
