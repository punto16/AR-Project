using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Spawner spawner;

    public List<GameObject> waifus;
    int totalHealth;
    int health;

    public GameObject buttonStartGame;
    public Spawner waveManager;

    // Start is called before the first frame update
    void Start()
    {
        totalHealth = waifus.Count - 1;
        health = totalHealth;
        buttonStartGame.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            SceneManager.LoadScene("LoseMenuScene");
        }
        else if ((spawner.wavesFinish) && (!GameObject.FindGameObjectWithTag("Enemy")))
        {
            SceneManager.LoadScene("WinMenuScene");
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            waifus[health].SetActive(false);
            health--;
        }
    }

    public void StartGame()
    {
        buttonStartGame.SetActive(false);
        waveManager.StartGame();
    }
}
