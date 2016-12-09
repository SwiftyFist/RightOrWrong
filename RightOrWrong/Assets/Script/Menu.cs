using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Menu : MonoBehaviour
{
    [SerializeField] private Texture2D startImage;
    [SerializeField] private GUIStyle buttonStyle;

    IEnumerator SceneChange()
    {
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene("BackGround", LoadSceneMode.Single);
    }

    void OnGUI ()
    {
        float positionX = Screen.width / 2;
        float positionY = Screen.height / 2;
        GUILayout.BeginArea(new Rect(positionX , positionY - Screen.height/16, Screen.width/3, Screen.height/3));
        if (GUILayout.Button(startImage, buttonStyle))
        {
            GetComponent<AudioSource>().Play();
            StartCoroutine(SceneChange());
        }
        GUILayout.EndArea();
    }

}
