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
            //span ai cars, if not 4 cars spawned
        }
    }

    public void EndMatch()
    {
        SceneManager.LoadScene("CharacterSelectScreen");
    }
}
