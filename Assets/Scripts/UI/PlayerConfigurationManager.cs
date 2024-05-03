using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace UI
{
    public class PlayerConfigurationManager : MonoBehaviour
    {
        private List<PlayerConfiguration> _playerConfigs;
        public List<PlayerConfiguration> PlayerConfigs => _playerConfigs;
        [SerializeField] private int maxPlayers = 4;

        public static PlayerConfigurationManager Instance { get; private set; }

        [SerializeField] private GameObject readyText;
        [SerializeField] private GameObject characterHolder;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.Log("SINGLETON - Trying to create another instance of singleton!");
                Destroy(Instance);
            } else {
               Instance = this;  
               DontDestroyOnLoad(Instance);
               _playerConfigs = new List<PlayerConfiguration>();
            }
        }

        private void Start()
        {
            readyText.SetActive(false);
        }

        public void ResetPlayers()
        {
            _playerConfigs = new List<PlayerConfiguration>();
        }

        public void HandlePlayerJoin(PlayerInput pi)
        {
            Debug.Log("Player joined " + pi.playerIndex);
            pi.transform.SetParent(characterHolder.transform);
            pi.transform.localScale = Vector3.one;
            if (!_playerConfigs.Any(p => p.PlayerIndex == pi.playerIndex))
            {
                _playerConfigs.Add(new PlayerConfiguration(pi));
            }
        }
        
        public void SetPlayerCharacter(int index, GameObject character)
        {
            _playerConfigs[index].PlayerCharacter = character;
        }

        public void ReadyPlayer(int index)
        {
            _playerConfigs[index].IsReady = true;
            Debug.Log("Player " + index + " is ready!");
            if (_playerConfigs.All(p => p.IsReady))
            {
                readyText.SetActive(true);
            }
        }

        public void UnreadyPlayer(int index)
        {
            _playerConfigs[index].IsReady = false;
            Debug.Log("Player " + index + " is not ready!");
            readyText.SetActive(false);
        }
        
        public void StartMatch()
        {
            if (_playerConfigs.All(p => p.IsReady))
            {
                SceneManager.LoadScene("SampleScene");
            }
        }
    }

    public class PlayerConfiguration
    {
        public PlayerConfiguration(PlayerInput pi)
        {
            PlayerIndex = pi.playerIndex;
            Input = pi;
        }
        
        public PlayerInput Input { get; set; }
        public int PlayerIndex { get; set; }
        public bool IsReady { get; set; }
        public GameObject PlayerCharacter { get; set; }
    }
}
