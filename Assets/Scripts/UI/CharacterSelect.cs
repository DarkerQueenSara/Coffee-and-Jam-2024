using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UI
{
    public class CharacterSelect : MonoBehaviour
    {

        private int _playerIndex;

        [SerializeField] public GameObject readyText;

        [SerializeField] private List<GameObject> characters;
        [SerializeField] private List<GameObject> prefabs;
        private int _currentSelectedCharacter;
        
        private float _ignoreInputTime = 1.5f;
        private bool _inputEnabled;
        private bool _characterSelected;

        private void Start()
        {
            _currentSelectedCharacter = 0;
            Invoke(nameof(EnableInput), _ignoreInputTime);
        }

        public void SetPlayerIndex(int pi)
        {
            Debug.Log("Setting player index " + pi);
            _playerIndex = pi;
        }

        private void EnableInput()
        {
            _inputEnabled = true;
        }

        public void GoUpOrDown(InputAction.CallbackContext ctx)
        {
            if (!_inputEnabled || _characterSelected) return;
            if (ctx.ReadValue<Vector2>().y > 0) GoUp();
            if (ctx.ReadValue<Vector2>().y < 0) GoDown();
        }

        private void GoUp()
        {
            _currentSelectedCharacter++;
            if (_currentSelectedCharacter > characters.Count) _currentSelectedCharacter = 0;

            for (int i = 0; i < characters.Count; i++)
            { 
                characters[i].SetActive(i == _currentSelectedCharacter);
            }
        }

        private void GoDown()
        {
            _currentSelectedCharacter--;
            if (_currentSelectedCharacter < 0) _currentSelectedCharacter = characters.Count - 1;

            for (int i = 0; i < characters.Count; i++)
            { 
                characters[i].SetActive(i == _currentSelectedCharacter);
            }
        }

        public void StartMatch()
        {
            PlayerConfigurationManager.Instance.StartMatch();
        }

        public void SetCharacter()
        {
            if (!_inputEnabled) return;
            
            PlayerConfigurationManager.Instance.SetPlayerCharacter(_playerIndex, prefabs[_currentSelectedCharacter]);
            readyText.SetActive(true);
            _characterSelected = true;
            
            PlayerConfigurationManager.Instance.ReadyPlayer(_playerIndex);
        }

        public void CancelCharacter()
        {
            if (!_inputEnabled) return;
            PlayerConfigurationManager.Instance.SetPlayerCharacter(_playerIndex, null);
            readyText.SetActive(false);
            _characterSelected = false;
            PlayerConfigurationManager.Instance.UnreadyPlayer(_playerIndex);

        }
    }
}
