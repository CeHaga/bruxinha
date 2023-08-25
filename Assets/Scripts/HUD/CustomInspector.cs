#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UpdateScore))]
public class CustomInspector : Editor{
    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        UpdateScore panel = (UpdateScore)target;

        if(GUILayout.Button("Click Here")){
            panel.AddScore(12567897);
        }
    }
}
#endif
