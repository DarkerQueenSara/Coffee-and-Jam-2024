using UI;
using UnityEngine;

public class Disable : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        PlayerConfigurationManager.Instance.enabled = false;
        Debug.Log("Disabled PlayerConfigurationManager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
