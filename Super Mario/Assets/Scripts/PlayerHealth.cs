using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int numberOfLives = 3;
    private Vector3 spawnPosition;
    private bool hasDied;

    public Vector3 SpawnPosition
    {
        get
        {
            return spawnPosition;
        }

        set
        {
            spawnPosition = value;
        }
    }

    // Use this for initialization
    void Start()
    {
        hasDied = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y < -5)
        {
            hasDied = true;
        }
        if (hasDied)
        {
            ControlDeath();

            hasDied = false;
        }
    }
    void ControlDeath()
    {
        if (numberOfLives > 0)
        {
            StartCoroutine("Respawn");
            numberOfLives -= 1;
        }
        if (numberOfLives == 0)
            StartCoroutine("Die");
    }

    IEnumerator Respawn()
    {
        new WaitForSeconds(1);
        yield return transform.position = SpawnPosition;
    }

    IEnumerator Die()
    {
        print("GameOver");
        yield return null;
    }
}
