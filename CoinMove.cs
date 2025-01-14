using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMove : MonoBehaviour
{
    public Coin coinScript;
    private Transform playerTransform;

    private bool isCollected = false;

    // Start is called before the first frame update
    void Start()
    {
        coinScript = gameObject.GetComponent<Coin>();
        playerTransform = coinScript.playerTransform;
    }

    // Update is called once per frame
    void Update()
    {
        if (coinScript.isMagnetPurchased)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, coinScript.moveSpeed * Time.deltaTime);
        }
       // transform.position = Vector3.MoveTowards(transform.position, coinScript.playerTransform.position, coinScript.moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player Bubble")
        {
            CollectCoin();
        }
    }

    private void CollectCoin()
    {
        if (!isCollected)
        {
            isCollected = true;

            CoinCounter coinCounter = FindObjectOfType<CoinCounter>();
            if (coinCounter != null)
            {
                coinCounter.IncreaseCoins(1);
            }

            Destroy(gameObject); // Destruir a moeda após coletá-la
        }
    }
}
