using UnityEditor;
using UnityEngine;

public class MissingScriptCleaner : MonoBehaviour
{
    [MenuItem("Tools/Clean Missing Scripts")]
    private static void CleanMissingScripts()
    {
        int removedCount = 0;

        // Get all GameObjects in the scene
        GameObject[] allGameObjects = GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject gameObject in allGameObjects)
        {
            // Get all components on the GameObject
            Component[] components = gameObject.GetComponents<Component>();

            SerializedObject serializedObject = new SerializedObject(gameObject);
            SerializedProperty prop = serializedObject.FindProperty("m_Component");

            int propertyIndex = 0;

            for (int i = 0; i < components.Length; i++)
            {
                if (components[i] == null)
                {
                    // Missing script found, remove it
                    prop.DeleteArrayElementAtIndex(propertyIndex);
                    removedCount++;
                }
                else
                {
                    propertyIndex++;
                }
            }

            serializedObject.ApplyModifiedProperties();
        }

        Debug.Log($"Removed {removedCount} missing scripts from the scene.");
    }
}