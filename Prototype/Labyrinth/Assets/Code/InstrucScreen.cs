using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class InstrucScreen : MonoBehaviour
{

    public void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            GameManager.Instance.Reset();
            SceneManager.LoadScene("StartScreen");
        }
    }
}
