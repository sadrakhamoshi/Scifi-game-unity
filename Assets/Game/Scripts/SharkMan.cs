using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkMan : MonoBehaviour
{
    [SerializeField]
    private AudioClip _weaponBuy;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                player player = other.GetComponent<player>();
                if (player != null)
                {
                    if (player.coinCount > 0)
                    {
                        player.coinCount--;
                        AudioSource.PlayClipAtPoint(_weaponBuy, Camera.main.transform.position, 1f);
                        UiManager ui = GameObject.Find("Canvas").GetComponent<UiManager>();
                        if (ui != null)
                        {
                            ui.showCoin(player.coinCount);
                        }
                    }
                    else
                    {
                        print("Siktir");
                    }
                }
            }
        }
    }
}
