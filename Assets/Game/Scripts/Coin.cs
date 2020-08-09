using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private AudioClip _coinPick;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                player player = other.GetComponent<player>();
                if (player != null)
                {
                    player.coinCount++;
                    AudioSource.PlayClipAtPoint(_coinPick, Camera.main.transform.position, 1f);
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
