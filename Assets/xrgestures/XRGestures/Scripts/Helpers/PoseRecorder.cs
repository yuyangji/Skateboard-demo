using NaughtyAttributes;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace XR_Gestures
{
    public class PoseRecorder : MonoBehaviour
    {
        [SerializeField] string containerName;

        [SerializeField] List<AlignedInDirection> directions;

        [SerializeField] XRAvatar avatar;
        [Button("Create")]
        public void CreateMyAsset()
        {
            if (directions == null)
            {
                return;
            }

            SetDirections();

            PoseSO asset = ScriptableObject.CreateInstance<PoseSO>();
            asset.SetFunctions(directions);
            string name = UnityEditor.AssetDatabase.GenerateUniqueAssetPath("Assets/XRGestures/ScriptableObjects/" + containerName + ".asset");
            AssetDatabase.CreateAsset(asset, name);
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();

            Selection.activeObject = asset;
        }
        void SetDirections()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add(DataKeyConstants.Avatar, avatar);
            directions.ForEach(f =>
            {
                f.Initialise(data);
                f.direction = f.GetCurrentDir();
            });
        }

    }

}