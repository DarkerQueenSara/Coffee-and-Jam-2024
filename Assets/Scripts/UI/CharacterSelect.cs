using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class CharacterSelect : MonoBehaviour
    {

        private int _playerIndex;

        [SerializeField] public GameObject readyText;

        [SerializeField] private List<GameObject> characters;
        private GameObject _currentSelectedCharacter;
        
        private float _ignoreInputTime = 1.5f;
        private bool _inputEnabled;
        private bool _characterSelected;
        
        public void SetPlayerIndex(int pi)
        {
            _playerIndex = pi;
            _ignoreInputTime = Time.time + _ignoreInputTime;
        }

        // Update is called once per frame
        private void Update()
        {
            if (Time.time > _ignoreInputTime)
            {
                _inputEnabled = true;
            }
        }

        public void GoUpOrDown()
        {
            if (!_inputEnabled || _characterSelected) return;

        }

        private void GoUp()
        {
            
        }

        private void GoDown()
        {
            if (!_inputEnabled || _characterSelected) return;

        }

        public void SetCharacter()
        {
            if (!_inputEnabled) return;
            
            PlayerConfigurationManager.Instance.SetPlayerCharacter(_playerIndex, _currentSelectedCharacter);
            readyText.SetActive(true);
            _characterSelected = true;
            
            PlayerConfigurationManager.Instance.ReadyPlayer(_playerIndex);
        }

        public void CancelCharacter()
        {
            if (!_inputEnabled) return;
            readyText.SetActive(true);
            _characterSelected = false;
            PlayerConfigurationManager.Instance.UnreadyPlayer(_playerIndex);

        }
    }
}
