using System;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnCharacterSelect : MonoBehaviour
{

    public GameObject characterSelectPrefab;
    public PlayerInput playerInput;
    private void Awake()
    {
        GameObject rootMenu = GameObject.Find("CharacterSelectHolder");
        if (rootMenu != null)
        {
            GameObject menu = Instantiate(characterSelectPrefab, rootMenu.transform);
            //playerInput.uiInputModule = menu.GetComponentInChildren<InputSystemUIInputModule>();
            menu.GetComponent<CharacterSelect>().SetPlayerIndex(playerInput);
        }
    }
}
