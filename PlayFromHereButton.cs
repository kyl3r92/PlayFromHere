using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityToolbarExtender;

[InitializeOnLoad]
public class PlayFromHereButton : MonoBehaviour
{
	static PlayFromHereButton()
	{
		ToolbarExtender.RightToolbarGUI.Add(OnToolbarGUI);
		EditorApplication.playModeStateChanged += SpawnPlayerAtSceneCam;
	}
	static void OnToolbarGUI()
	{
		GUILayout.FlexibleSpace();

		// This button spawns the player where it scene camera is currently at - caution: You need to choose "Play Unfocused" or "Play Focused" but not "Play Maximized" in Game view, otherwise it will use the main Camera or whatever.
		if (GUILayout.Button(new GUIContent("here", "Play from here"), EditorStyles.toolbarButton))
		{
			if(!Application.isPlaying)
            {
				EditorSceneManager.OpenScene("Assets/MyScenes/PlayerBasics/PlayerBasics.unity", OpenSceneMode.Additive);
				EditorPrefs.SetBool("spawnHere", true);
				EditorApplication.isPlaying = true;
			}
            else
            {
				spawnPlayerAtCam();
			}
		}

		// This button plays the game but also activates the enterPlayModeOptions, where I have domain reload and scene reload disabled - makes it faster to iterate in some cases
		// See "Project Settings" -> "Editor" -> "Enter Play Mode Options" https://docs.unity3d.com/Manual/ConfigurableEnterPlayMode.html
		if (GUILayout.Button(new GUIContent(">>", "No Reload"), EditorStyles.toolbarButton))
		{
			EditorSettings.enterPlayModeOptionsEnabled = true;
			EditorPrefs.SetBool("fastMode", true);
			EditorApplication.isPlaying = true;
		}
	}
	static void SpawnPlayerAtSceneCam(PlayModeStateChange state)
	{
		// This method is run whenever the playmode state is changed. We have to differentiate between "normal play" and "play from here" so we read and reset the Prefs boolean.
		bool spawnHere = EditorPrefs.GetBool("spawnHere");
		if (spawnHere && state == PlayModeStateChange.EnteredPlayMode)
		{
			spawnPlayerAtCam();
			EditorPrefs.SetBool("spawnHere", false);
		}

		if(EditorPrefs.GetBool("fastMode") && state == PlayModeStateChange.ExitingPlayMode) // reset enterPlayMode settings
        {
			EditorSettings.enterPlayModeOptionsEnabled = false;
			EditorPrefs.SetBool("fastMode", false);
		}
	}

	static void spawnPlayerAtCam()
    {
		Transform scene_cam = SceneView.GetAllSceneCameras()[0].transform;
		// Spawn your player here, at scene_cam.position
	}
}