using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField]
    private Text _ammor;

    public void UpdatAmmor(int count)
    {
        _ammor.text = "Ammor : " + count;
    }
}
