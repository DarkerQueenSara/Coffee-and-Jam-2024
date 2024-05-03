using System.Collections.Generic;
using Cars;
using UI;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject AIPrefab;
    public int defaultValue = 2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        List<Vector2> startingPositions = new List<Vector2> {new Vector2(15, 7), new Vector2(-15, 7), new Vector2(15, -7), new Vector2(-15, -7)};
        int i = 0;
        if (PlayerConfigurationManager.Instance != null)
        {
            List<PlayerConfiguration> players = PlayerConfigurationManager.Instance.PlayerConfigs;
            foreach (PlayerConfiguration player in players)
            {
                GameObject car = Instantiate(player.PlayerCharacter, startingPositions[i], Quaternion.identity);
                car.GetComponent<Car>().playerIndex = player.PlayerIndex;
                i++;
            }
        }
        else i = defaultValue;
        while (i < 4) {
             Instantiate(AIPrefab, startingPositions[i], Quaternion.identity);
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
