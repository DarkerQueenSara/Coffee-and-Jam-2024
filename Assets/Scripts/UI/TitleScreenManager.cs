using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class TitleScreenManager : MonoBehaviour
    {

        public Button startGameButton;
        public Button optionsButton;
        public Button exitGameButton;
        // Start is called before the first frame update
        private void Start()
        {
            startGameButton.onClick.AddListener(LoadCharacterSelect);
            optionsButton.onClick.AddListener(LoadOptions);
            exitGameButton.onClick.AddListener(ExitGame);
        }

        private static void LoadCharacterSelect()
        {
            SceneManager.LoadScene("CharacterSelectScreen");
        }
        
        private static void LoadOptions()
        {
            SceneManager.LoadScene("OptionsScreen");
        }


        private static void ExitGame()
        {
            Debug.Log("EXITED GAME");
        }
    }
}
