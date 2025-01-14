using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Enum for different present effects
public enum PresentEffect { Magnet, Life, Flash, Shield, DoubleShot, TripleShot, Invincibility }

public class LojaScript : MonoBehaviour
{
    // UI buttons for different power-ups
    public GameObject btnDouble;
    public GameObject btnTriple;
    public GameObject btnIman;
    public GameObject btnVida;
    public GameObject btnBomba;
    public GameObject btnEscudo;
    public GameObject btnPresente;
    public GameObject btnFlash;

    // Icons for activated power-ups
    public GameObject btnImanIcon;
    public GameObject btnEscudoIcon;
    public GameObject btnBombaIcon;

    private int bombaCompradaCount = 0; // Counter to control bomb purchases
    private GameManager gameManager;

    // References to other scripts
    private Magnet magnetScript;
    private CoinMove coinMoveScript;
    private Coin CoinScript;

    // Magnet power-up properties
    public float magnetDuration = 2f; // Duration of magnet effect in seconds
    private bool isMagnetActive = false; // Indicates if magnet is active
    private float magnetTimer = 0f; // Timer for magnet effect

    public Text coinsText;
    private PresentEffect currentPresentEffect;

    private void Start()
    {
        // Initialize references to other scripts
        gameManager = FindObjectOfType<GameManager>();
        magnetScript = GameObject.FindGameObjectWithTag("Magnet").GetComponent<Magnet>();
        coinMoveScript = GameObject.FindGameObjectWithTag("Coin").GetComponent<CoinMove>();
        CoinScript = GameObject.FindGameObjectWithTag("Coin").GetComponent<Coin>();
    }

    private void Update()
    {
        // Handle magnet timer
        if (isMagnetActive)
        {
            magnetTimer -= Time.deltaTime;

            if (magnetTimer <= 0f)
            {
                isMagnetActive = false;
            }
        }

        // Check for magnet activation input
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!isMagnetActive)
            {
                ComprarIman();
            }
        }
    }

    // Purchase double shot power-up
    public void ComprarDouble()
    {
        if (GameController2.COINS >= 4)
        {
            GameController2.FIRE_MODE = FireMode.Double;
            GameController2.COINS -= 4; // Power-up price

            // Change damage of FireMode.Double to 2
            foreach (var bulletPrefab in Player.instance.bulletPrefab)
            {
                bulletPrefab.GetComponent<Bullet>().damage = 2;
            }

            btnDouble.SetActive(false);
        }
    }

    // Purchase triple shot power-up
    public void ComprarTriple()
    {
        if (GameController2.COINS >= 5)
        {
            GameController2.FIRE_MODE = FireMode.Triple;
            GameController2.COINS -= 5; // Power-up price

            // Change damage of FireMode.Triple to 3
            foreach (var bulletPrefab in Player.instance.bulletPrefab)
            {
                bulletPrefab.GetComponent<Bullet>().damage = 3;
            }

            btnTriple.SetActive(false);
        }
    }

    // Purchase magnet power-up
    public void ComprarIman()
    {
        if (GameController2.COINS >= 8)
        {
            GameController2.COINS -= 8; // Power-up price
            GameController2.POSSUI_IMAN = true; // Indicates player has purchased magnet
        }
    }

    // Purchase extra life
    public void ComprarVida()
    {
        if (GameController2.COINS >= 8)
        {
            if (GameController2.PLAYER_HEALTH < 6)
            {
                GameController2.COINS -= 8; // Power-up price   
                GameController2.PLAYER_HEALTH += 1; // Increase player health

                // Update health visualization (activate corresponding heart)
                Player.instance.hearts[GameController2.PLAYER_HEALTH - 1].SetActive(true);
            }

            btnVida.SetActive(false);
        }
    }

    // Purchase bomb power-up
    public void ComprarBomba()
    {
        if (GameController2.COINS >= 10 && bombaCompradaCount < 4)
        {
            btnBombaIcon.SetActive(true);
            GameController2.COINS -= 10; // Power-up price
            bombaCompradaCount++;

            if (bombaCompradaCount >= 4)
            {
                btnBomba.SetActive(false);
            }
        }
    }

    // Purchase shield power-up
    public void ComprarEscudo()
    {
        if (GameController2.COINS >= 15)
        {
            GameController2.COINS -= 15; // Power-up price
            btnEscudo.SetActive(false);
        }
    }

    // Purchase random present power-up
    public void ComprarPresente()
    {
        if (GameController2.COINS >= 12)
        {
            GameController2.COINS -= 12; // Power-up price

            // Randomly choose a present effect
            currentPresentEffect = (PresentEffect)Random.Range(0, (int)PresentEffect.Invincibility + 1);

            // Activate corresponding effect
            switch (currentPresentEffect)
            {
                case PresentEffect.Magnet:
                    isMagnetActive = true;
                    magnetTimer = magnetDuration;
                    break;
                case PresentEffect.Shield:
                    ComprarEscudo();
                    break;
                case PresentEffect.DoubleShot:
                    ComprarDouble();
                    break;
                case PresentEffect.TripleShot:
                    ComprarTriple();
                    break;
                case PresentEffect.Invincibility:
                    GameObject player = GameObject.FindGameObjectWithTag("Player");
                    if (player != null)
                    {
                        Invincibility invincibilityScript = player.GetComponent<Invincibility>();
                        if (invincibilityScript != null)
                        {
                            invincibilityScript.ActivateInvincibility(player, 5f); // Adjust duration as needed
                        }
                    }
                    break;
                case PresentEffect.Life:
                    // Implement life effect
                    break;
                case PresentEffect.Flash:
                    // Implement flash effect
                    break;
            }

            btnPresente.SetActive(false);
        }
    }

    // Purchase flash power-up
    public void ComprarFlash()
    {
        if (GameController2.COINS >= 10)
        {
            GameController2.COINS -= 10; // Power-up price
            coinsText.text = GameController2.COINS.ToString();
            btnFlash.SetActive(false);

            // Increase bullet speed (commented out)
            /*foreach (GameObject bulletObj in bulletPrefab)
            {
                Bullet bullet = bulletObj.GetComponent<Bullet>();
                bullet.speed *= 2;
            }*/
        }
    }

    // Return to appropriate scene based on game state
    public void Back()
    {
        if (GameController2.IS_WINNING)
        {
            SceneManager.LoadScene("Win");
        }
        else
        {
            SceneManager.LoadScene("Defeat");
        }
    }
}
