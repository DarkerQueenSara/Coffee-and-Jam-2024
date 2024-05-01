using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XInput;

namespace UI
{
    public class CharacterSelectHolder : MonoBehaviour
    {

        private PlayerInput _playerInput;

        // Start is called before the first frame update
        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            _playerInput.onControlsChanged += EliminateDuplicates;
        }

        private static void EliminateDuplicates(PlayerInput gamepad)
        {
            foreach (var item in Gamepad.all)
            {
                Debug.Log(item);
            }
            
            foreach (var item in Gamepad.all.OfType<XInputController>())
            {
                Debug.Log(
                    $"A copy of XInput was active at almost the same time. Disabling XInput device. `{gamepad}`; `{item}`");
                InputSystem.DisableDevice(item);
            }
        }
    }
}