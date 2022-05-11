using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace XR_Gestures
{

	[CustomEditor(typeof(AnimationController))]
	public class AnimationControllerEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();

			AnimationController controller = (AnimationController)target;

            List<string> states = controller.GetStates();
            if (states == null || states.Count == 0) return;


            foreach (string clip in states)
            {
                if (clip == null) continue;
                if (GUILayout.Button("Play " + clip))
                {
                    controller.PlayAnimation(clip);
                }
            }




        }
    }
}