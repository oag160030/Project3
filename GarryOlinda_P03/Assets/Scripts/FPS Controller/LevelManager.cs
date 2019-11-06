using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject player = null;
    [SerializeField] Camera playerCamera = null;
    [SerializeField] GameObject playerPanel = null;
    [SerializeField] Camera gameOverCamera = null;
    [SerializeField] GameObject gameOverPanel = null;
    [SerializeField] GameObject enemy = null;
    //PlayerHealth _playerHealthInput = null;

    [SerializeField] AudioClip playerDeathSound = null;

    private void Awake()
    {
        //_playerHealthInput = player.GetComponent<PlayerHealth>();
        playerCamera.enabled = true;
        gameOverCamera.enabled = false;
        gameOverPanel.SetActive(false);
    }

    private void OnEnable()
    {
        //_playerHealthInput.PlayerDeathInput += OnDeath;
    }

    private void OnDisable()
    {
        //_playerHealthInput.PlayerDeathInput -= OnDeath;
    }

    void Update()
    {
        // 'Escape' exits the program
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Exit Program");
            Application.Quit();
        }
    }

    void OnDeath()
    {
        Debug.Log("Player Died");
        AudioSource.PlayClipAtPoint(playerDeathSound, gameObject.transform.position);
        playerCamera.enabled = !playerCamera.enabled;
        player.SetActive(false);
        enemy.SetActive(false);
        Destroy(enemy);
        playerPanel.SetActive(false);
        gameOverCamera.enabled = !gameOverCamera.enabled;
        gameOverPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }
}
