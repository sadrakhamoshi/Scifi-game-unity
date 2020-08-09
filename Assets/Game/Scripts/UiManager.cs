using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField]
    private Text _ammor;

    [SerializeField]
    private Image _coinImage;

    public void UpdatAmmor(int count)
    {
        _ammor.text = "Ammor : " + count;
    }

    public void showCoin(int coinCount)
    {
        if (coinCount > 0)
        {
            _coinImage.enabled = true;
        }
        else
        {
            _coinImage.enabled = false;
        }
    }
}
