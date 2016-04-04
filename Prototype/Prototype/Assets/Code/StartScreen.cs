using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    public string FirstLevel;
    public string InstructionsScreen;

    public void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            GameManager.Instance.Reset();
            SceneManager.LoadScene(FirstLevel);
        }

        if (Input.GetKey(KeyCode.X))
        {
            SceneManager.LoadScene(InstructionsScreen);
        }
    }

}
