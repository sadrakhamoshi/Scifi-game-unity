using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField]
    private GameObject _crete;
    
    public void DestroyCrete()
    {
        Instantiate(_crete, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
