using UnityEditor;
using UnityEngine;

namespace Sabanishi.ActionTutorial.Editor
{
    [CustomEditor(typeof(Player))]
    public class PlayerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var player = target as Player;
            if (GUILayout.Button("Collision"))
            {
                player.FixedUpdate();
            }
        }
    }
}