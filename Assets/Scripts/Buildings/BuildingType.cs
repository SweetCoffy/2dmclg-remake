using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace Game.Buildings
{
    [CreateAssetMenu(fileName = "New Building", menuName = "Building Type")]
    public class BuildingType : ScriptableObject
    {
        public GameObject m_Prefab;
        public Building prefabBuilding { get; private set; }
        public bool CanReplace(BuildingType other)
        {
            return false;
        }
        void OnValidate()
        {
            if (m_Prefab == null)
            {
                Debug.LogError($"{GetType()} {name} must have a valid Prefab!");
                return;
            }
            prefabBuilding = m_Prefab.GetComponent<Building>();
            if (prefabBuilding == null)
            {
                Debug.LogError($"{GetType()} {name} must have a Prefab with a Building component!");
                return;
            }
            if (prefabBuilding.Type != this)
            {
                prefabBuilding.Type = this;
#if UNITY_EDITOR
                EditorUtility.SetDirty(prefabBuilding);
#endif
            }
        }
    }
}