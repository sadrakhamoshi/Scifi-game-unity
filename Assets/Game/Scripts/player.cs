using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class player : MonoBehaviour
{
    private CharacterController _controller;

    [SerializeField]
    private float _speed = 4f;

    [SerializeField]
    private float gravity = 9.8f;

    [SerializeField]
    private GameObject _muzzelFlash;

    [SerializeField]
    private GameObject _hitMaker;

    [SerializeField]
    private AudioSource _weaponAudio;

    [SerializeField]
    private GameObject _weapon;

    private int currentAmmo;
    private int maxAmmo = 50;

    private bool _isReloading;

    public int coinCount = 0;

    private UiManager _uiManager;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        currentAmmo = maxAmmo;
        _uiManager = GameObject.Find("Canvas").GetComponent<UiManager>();
        _uiManager.UpdatAmmor(50);
    }

    void Update()
    {
        HideCursor();
        Movement();

        if (Input.GetKeyDown(KeyCode.R)&&!_isReloading)
        {
            _isReloading = true;
            StartCoroutine(Reload());
        }

        if (Input.GetMouseButton(0) && currentAmmo > 0)
        {
            Shoot();
        }
        else
        {
            _weaponAudio.Stop();
            _muzzelFlash.SetActive(false);
        }
    }

    private void Shoot()
    {
        _muzzelFlash.SetActive(true);

        if (!_weaponAudio.isPlaying)
            _weaponAudio.Play();

        currentAmmo--;
        _uiManager.UpdatAmmor(currentAmmo);

        //screen point ->(0,screen.width)
        //Ray originRay = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        //view point -> (0,1)
        Ray originRay = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        RaycastHit hit;

        if (Physics.Raycast(originRay, out hit, Mathf.Infinity))
        {
            print(hit.transform.name);
            var tmp = Instantiate(_hitMaker, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(tmp, 0.8f);

            Destructible destructible = hit.transform.GetComponent<Destructible>();
            if (destructible != null)
            {
                destructible.DestroyCrete();    
            }
        }
    }

    private static void HideCursor()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void Movement()
    {
        Vector3 diraction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 velocity = diraction * _speed;

        velocity.y -= gravity;

        //convert local to global
        velocity = transform.transform.TransformDirection(velocity);
        _controller.Move(velocity * Time.deltaTime);
    }
    IEnumerator Reload()
    {
        yield return new WaitForSeconds(1.5f);
        currentAmmo = maxAmmo;
        _uiManager.UpdatAmmor(currentAmmo);
        _isReloading = false;
    }

    public void EnabaleWeapon()
    {
        _weapon.SetActive(true);
    }
}
