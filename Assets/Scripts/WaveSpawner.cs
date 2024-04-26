using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
[System.Serializable]
public class Wave
{   //Wave info
    public string waveName;
    public int numOfEnemy;
    public GameObject[] typeofEnemy;    
    public float spawnDelay;
}
public class WaveSpawner : MonoBehaviour
{
    [SerializeField] Wave[] waves;
    public Transform[] spawnPoints;
    public Transform waveSpawn;
    public Text waveName;
    public Animator anim;
    public Animator lockedDoorAnim;
    public GameObject[] reward;
    public GameObject triggerWave;

    private Wave currentWave;
    private int currentWaveNum;
    private bool canSpawn = true;
    private bool canAnimate = false;
    private float nextSpawnTime;
    private void Start() {
        anim.SetBool("IsRewarded",false);
    }
    private void Update() {
        //Start first wave
        currentWave = waves[currentWaveNum];
        SpawnWave();
        //Check to start next wave
        int totalEnemies = waveSpawn.GetComponentsInChildren<Animator>().GetLength(0);
        if(totalEnemies == 0){//all enemy die
            if(currentWaveNum +1 != waves.Length){//check whenever is the last wave
                if(canAnimate){//check wavewarning
                    waveName.text = waves[currentWaveNum + 1].waveName;
                    anim.SetTrigger("WaveComplete");
                    canAnimate = false;
                }
            }
            else {
                //finish wave
                anim.SetTrigger("WaveDone");
                lockedDoorAnim.SetBool("IsOpen",true);
            }
        }
        
    }
    void SpawnWave(){
        if(canSpawn && nextSpawnTime < Time.time){
            //spawn random enenemy at random position
            GameObject randomEnemy = currentWave.typeofEnemy[Random.Range(0,currentWave.typeofEnemy.Length)];
            Transform randomPoint = spawnPoints[Random.Range(0,spawnPoints.Length)];
            Instantiate(randomEnemy,randomPoint.position,Quaternion.identity,waveSpawn);
            currentWave.numOfEnemy --;
            nextSpawnTime = Time.time + currentWave.spawnDelay;
            if(currentWave.numOfEnemy == 0){
                canSpawn = false;
                canAnimate = true;
            }
        }
        
    }
    void SpawnNextWave(){
        currentWaveNum++;
        canSpawn = true;
    }
    void WaveReward(){
        anim.SetBool("IsRewarded",true);
        for(int i = 0; i < reward.Length; i++) {
            reward[i].SetActive(true);
        }
        Destroy(triggerWave);
    }
}

