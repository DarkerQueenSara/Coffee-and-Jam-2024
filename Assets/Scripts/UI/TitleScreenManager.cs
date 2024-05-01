using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreenManager : MonoBehaviour
{

    public Button startGameButton;

    public Button exitGameButton;
    // Start is called before the first frame update
    private void Start()
    {
        startGameButton.onClick.AddListener(LoadCharacterSelect);
        exitGameButton.onClick.AddListener(ExitGame);
    }

    private void LoadCharacterSelect()
    {
        Debug.Log("STARTED GAME");
    }

    private void ExitGame()
    {
        Debug.Log("EXITED GAME");
    }
}
