using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public Transform playerTransform;
    public float moveSpeed = 17f;
    public bool isMagnetPurchased = false;
    CoinMove coinMoveScript;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        coinMoveScript = gameObject.GetComponent<CoinMove>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin Detector" && coinMoveScript != null && coinMoveScript.enabled == false && coinMoveScript.coinScript.isMagnetPurchased)
        {
            coinMoveScript.enabled = true;
        }
    }
}
