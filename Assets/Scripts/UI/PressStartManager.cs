using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class PressStartManager : MonoBehaviour
    {
        public void LoadGame(){
            SceneManager.LoadScene("MainMenu");
        }
    }
}
