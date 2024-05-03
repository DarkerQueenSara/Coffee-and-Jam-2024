using System.Collections.Generic;
using Cars;
using PlayerControls;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MatchManager : MonoBehaviour
{
    public List<Transform> transformSpawns;
    public List<GameObject> aiCars;
    public static MatchManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("SINGLETON - Trying to create another instance of singleton!");
            Destroy(Instance);
        } else {
            Instance = this;  
            List<PlayerConfiguration> playerConfigs = PlayerConfigurationManager.Instance.PlayerConfigs;
            int instantiatedCars = 0;
            for (int i = 0; i < playerConfigs.Count; i++)
            {
                GameObject spawnedCar = Instantiate(playerConfigs[i].PlayerCharacter, transformSpawns[i].position, transformSpawns[i].rotation);
                PlayerCarInput carComponent = spawnedCar.GetComponent<PlayerCarInput>();
                carComponent.InitializeCar(playerConfigs[i].Input);
                instantiatedCars++;
            }
            for (int i = instantiatedCars; i < 4; i++)
            {
                Instantiate(aiCars[Random.Range(0, aiCars.Count)], transformSpawns[i].position, transformSpawns[i].rotation);
            }
        }
    }

    private void Update() {
        int alive = GameObject.FindGameObjectsWithTag("Player").Length + GameObject.FindGameObjectsWithTag("AI").Length;
        if (alive <= 1) {
            EndMatch();
        }
    }

    public void EndMatch()
    {
        SceneManager.LoadScene("CharacterSelectScreen");
    }
}
