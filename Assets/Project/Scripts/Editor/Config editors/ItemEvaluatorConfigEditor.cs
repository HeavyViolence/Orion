using Orion.Gameplay.Items;

using UnityEditor;
using UnityEngine;

namespace Orion.Editors
{
    [CustomEditor(typeof(ItemEvaluatorConfig))]
    public sealed class ItemEvaluatorConfigEditor : Editor
    {
        private ItemEvaluatorConfig _target;

        private SerializedProperty _tierColorCodes;
        private SerializedProperty _powerDistribution;
        private SerializedProperty _spawnProbability;

        private void OnEnable()
        {
            _target = (ItemEvaluatorConfig)target;

            _tierColorCodes = serializedObject.FindProperty("_tierColorCodes");
            _powerDistribution = serializedObject.FindProperty("_powerDistribution");
            _spawnProbability = serializedObject.FindProperty("_spawnProbability");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(_tierColorCodes);
            EditorGUILayout.PropertyField(_powerDistribution);
            EditorGUILayout.PropertyField(_spawnProbability);
            EditorGUILayout.Space();

            if (GUILayout.Button("Log cache"))
            {
                _target.BuildCache();

                foreach (var entry in _target.GetCurrentCache())
                {
                    Debug.Log($"Tier = {entry.Key}, {entry.Value}");
                }
            }

            if (GUILayout.Button("Log probable tier"))
            {
                Debug.Log($"Probable tier = {_target.GetProbableTier()}");
            }

            if (GUILayout.Button("Log random tier"))
            {
                Debug.Log($"Random tier = {_target.GetRandomTier()}");
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}