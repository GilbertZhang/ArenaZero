using UnityEngine;
using System.Collections;

public class ApplicationManager : MonoBehaviour
{
    public void Quit () 
	{
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}

    public void onclickquickmatch() {
        Application.LoadLevel("RenderTexture");
    }

    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }


}
