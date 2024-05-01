using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UI
{
    public class CharacterSelector : MonoBehaviour
    {

        private InputMap _inputMap;

        
        public GameObject controllerDisconnectedScreen;
        public GameObject controllerConnectedScreen;

        private PlayerInput _playerInput;

        private void Awake()
        {
            _inputMap = new InputMap();
            //_inputMap.UI.Submit.performed += _ => OnSubmit();
            //_inputMap.UI.Cancel.performed += _ => OnCancel();
        }
        
        private void OnEnable()
        {
            _inputMap.UI.Enable();
        }
        
        private void OnDisable()
        {
            _inputMap.UI.Disable();
        }

        // Start is called before the first frame update
        private void Start()
        {
            _playerInput = GetComponent<PlayerInput>();
        }

        public void OnSubmit()
        {
            Debug.Log("SUBMIT");
            controllerDisconnectedScreen.SetActive(false);
            controllerConnectedScreen.SetActive(true);
        }
        
        public void OnCancel()
        {
            Debug.Log("Cancel");
            controllerConnectedScreen.SetActive(false);
            controllerDisconnectedScreen.SetActive(true);
        }
    
        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
