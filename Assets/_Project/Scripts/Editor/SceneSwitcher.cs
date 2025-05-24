#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using System.IO;

public class SceneSwitcher
{
    private const string MENU_BASE = "Tools/SceneSwitcher/";

    [MenuItem(MENU_BASE + "0 &1")]
    private static void Load0() => LoadSceneByIndex(0);

    [MenuItem(MENU_BASE + "1 &2")]
    private static void Load1() => LoadSceneByIndex(1);

    [MenuItem(MENU_BASE + "2 &3")]
    private static void Load2() => LoadSceneByIndex(2);

    private static void LoadSceneByIndex(int index)
    {
        var scenes = EditorBuildSettings.scenes;

        if (index < 0 || index >= scenes.Length)
        {
            Debug.LogError($"Scene index {index} is out of range.");
            return;
        }

        var scene = scenes[index];
        if (!scene.enabled)
        {
            Debug.LogWarning($"Scene at index {index} is not enabled in Build Settings.");
            return;
        }

        if (!File.Exists(scene.path))
        {
            Debug.LogError($"Scene file does not exist at path: {scene.path}");
            return;
        }

        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            EditorSceneManager.OpenScene(scene.path);
            Debug.Log($"Loaded scene at index {index}: {scene.path}");
        }
    }
}
#endif
