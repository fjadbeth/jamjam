using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayAgain : MonoBehaviour
{
    private void Awake()
    {
        Screen.SetResolution(1920, 1080, false);
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(StartGame);
    }

    void StartGame()
    {
        SceneManager.LoadScene("IzgeScene");
    }

}
