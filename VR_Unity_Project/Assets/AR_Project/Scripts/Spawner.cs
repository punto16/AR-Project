using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<WaveObject> waves = new List<WaveObject>();
    public bool isWaitingForNextWave;
    public bool wavesFinish;
    public int currentWave;
    public Transform endPosintion;

    public TextMeshProUGUI counterText;
    public GameObject buttonNextWave;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckCounterAndShowButton();
        CheckCounterForNextWave();
    }

    private void CheckCounterForNextWave()
    {
        if(isWaitingForNextWave && !wavesFinish)
        {
            waves[currentWave].counterToNextWave += 1 * Time.deltaTime;
            counterText.text = waves[currentWave].counterToNextWave.ToString("00");
            if (waves[currentWave].counterToNextWave >= waves[currentWave].timeForNextWave)
            {
                ChangeWave();
            }
        }
    }

    public void ChangeWave()
    {
        if (wavesFinish)
            return;
        currentWave++;
        StartCoroutine(ProcesWave());
    }

    public void StartGame()
    {
        StartCoroutine(ProcesWave());
    }

    private IEnumerator ProcesWave()
    {
        if(wavesFinish)
            yield break;
        isWaitingForNextWave = false;
        for (int i = 0; i < waves[currentWave].spawners.Count; i++)
        {
            StartCoroutine(ProcesSpawns(waves[currentWave].spawners[i]));
        }
        while (!CheckCurrentWaveCompletion())
        {
            yield return null;
        }
        isWaitingForNextWave = true;
        if (currentWave >= waves.Count - 1)
        {
            //NIVEL TERMINADO
            wavesFinish = true;
        }
    }

    private IEnumerator ProcesSpawns(Spawn spawn)
    {
        for (int i = 0; i < spawn.enemyNum; i++)
        {
            GameObject enemyGo1 = Instantiate(waves[currentWave].enemy, spawn.spawnPos.position, spawn.spawnPos.rotation);
            enemyGo1.GetComponent<Enemy>().waypoint = endPosintion;

            yield return new WaitForSeconds(spawn.timerPerCreation);
        }
        spawn.completed = true;
    }

    private void CheckCounterAndShowButton()
    {
        if (!wavesFinish)
        {
            buttonNextWave.SetActive(isWaitingForNextWave);
            counterText.gameObject.SetActive(isWaitingForNextWave);
        }
    }

    private bool CheckCurrentWaveCompletion()
    {
        for (int i = 0; i < waves[currentWave].spawners.Count; i++)
        {
            if (!waves[currentWave].spawners[i].completed)
                return false;
        }
        return true;
    }

    [System.Serializable]
    public class WaveObject
    {
        public float timeForNextWave = 10;
        [HideInInspector] public float counterToNextWave = 0;
        public GameObject enemy;
        public List<Spawn> spawners = new List<Spawn>();
    }

    [System.Serializable]
    public class Spawn
    {
        public Transform spawnPos;
        public int enemyNum = 10;
        public float timerPerCreation = 1;
        [HideInInspector] public bool completed = false;
    }
}
