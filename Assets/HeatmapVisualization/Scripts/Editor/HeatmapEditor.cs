using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace HeatmapVisualization
{
	[CustomEditor(typeof(Heatmap))]
	public class HeatmapEditor : Editor
	{
		#region Globals
		private new Heatmap target;
		private bool foldoutReferences = false;
		private bool foldoutGenerationSettings = true;
		private bool foldoutRenderingSettings = true;

		#region Target Properties
		private SerializedProperty gaussianComputeShader;
		private SerializedProperty resolution;
		private SerializedProperty cutoffMethod;
		private SerializedProperty cutoffPercentage;
		private SerializedProperty gaussStandardDeviation;
		private SerializedProperty colormap1;
		private SerializedProperty colormap2;
		private SerializedProperty colormap3;
		private SerializedProperty colormap4;
		private SerializedProperty colormap5;
		private SerializedProperty renderOnTop;
		private SerializedProperty textureFilterMode;
		private SerializedProperty Temperature;
		public SerializedProperty mqttReceiver;
		#endregion
		#endregion
        

		#region Functions
		private void OnEnable()
		{
			target = (Heatmap)base.target;

			//get serialized properties
			gaussianComputeShader = serializedObject.FindProperty("gaussianComputeShader");
			resolution = serializedObject.FindProperty("resolution");
			cutoffMethod = serializedObject.FindProperty("cutoffMethod");
			cutoffPercentage = serializedObject.FindProperty("cutoffPercentage");
			gaussStandardDeviation = serializedObject.FindProperty("gaussStandardDeviation");
			colormap1 = serializedObject.FindProperty("colormap1");
			colormap2 = serializedObject.FindProperty("colormap2");
			colormap3 = serializedObject.FindProperty("colormap3");
			colormap4 = serializedObject.FindProperty("colormap4");
			colormap5 = serializedObject.FindProperty("colormap5");
			renderOnTop = serializedObject.FindProperty("renderOnTop");
			textureFilterMode = serializedObject.FindProperty("textureFilterMode");
			Temperature = serializedObject.FindProperty("Temperature");
			mqttReceiver = serializedObject.FindProperty("mqttReceiver");

			//get foldout flags
			foldoutGenerationSettings = EditorPrefs.GetBool("HeatmapEditor-foldoutGenerationSettings", foldoutGenerationSettings);
			foldoutRenderingSettings = EditorPrefs.GetBool("HeatmapEditor-foldoutRenderingSettings", foldoutRenderingSettings);
		}


		private void OnDestroy()
		{
			//save foldout flags
			EditorPrefs.SetBool("HeatmapEditor-foldoutGenerationSettings", foldoutGenerationSettings);
			EditorPrefs.SetBool("HeatmapEditor-foldoutRenderingSettings", foldoutRenderingSettings);
		}


		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			foldoutReferences = EditorGUILayout.BeginFoldoutHeaderGroup(foldoutReferences, "Prefab References");
			if (foldoutReferences)
			{
				EditorGUILayout.PropertyField(gaussianComputeShader);
			}
			EditorGUILayout.EndFoldoutHeaderGroup();

			foldoutGenerationSettings = EditorGUILayout.BeginFoldoutHeaderGroup(foldoutGenerationSettings, "Generation Settings");
			if (foldoutGenerationSettings)
			{
				EditorGUILayout.PropertyField(resolution);
				EditorGUILayout.PropertyField(cutoffMethod);
				EditorGUILayout.PropertyField(cutoffPercentage);
				EditorGUILayout.PropertyField(gaussStandardDeviation);
				EditorGUILayout.PropertyField(Temperature);
				EditorGUILayout.PropertyField(mqttReceiver);
			}
			EditorGUILayout.EndFoldoutHeaderGroup();


			foldoutRenderingSettings = EditorGUILayout.BeginFoldoutHeaderGroup(foldoutRenderingSettings, "Rendering Settings");
			if (foldoutRenderingSettings)
			{
				EditorGUI.BeginChangeCheck();
				EditorGUILayout.PropertyField(colormap1);
				if (EditorGUI.EndChangeCheck())
				{
					serializedObject.ApplyModifiedProperties();
					target.SetColormap();
				}
				EditorGUI.BeginChangeCheck();
				EditorGUILayout.PropertyField(colormap2);
				if (EditorGUI.EndChangeCheck())
				{
					serializedObject.ApplyModifiedProperties();
					target.SetColormap();
				}
				EditorGUI.BeginChangeCheck();
				EditorGUILayout.PropertyField(colormap3);
				if (EditorGUI.EndChangeCheck())
				{
					serializedObject.ApplyModifiedProperties();
					target.SetColormap();
				}
				EditorGUI.BeginChangeCheck();
				EditorGUILayout.PropertyField(colormap4);
				if (EditorGUI.EndChangeCheck())
				{
					serializedObject.ApplyModifiedProperties();
					target.SetColormap();
				}
				EditorGUI.BeginChangeCheck();
				EditorGUILayout.PropertyField(colormap5);
				if (EditorGUI.EndChangeCheck())
				{
					serializedObject.ApplyModifiedProperties();
					target.SetColormap();
				}
				EditorGUI.BeginChangeCheck();
				EditorGUILayout.PropertyField(renderOnTop);
				if (EditorGUI.EndChangeCheck())
				{
					serializedObject.ApplyModifiedProperties();
					target.SetRenderOnTop();
				}
				EditorGUI.BeginChangeCheck();
				EditorGUILayout.PropertyField(textureFilterMode);
				if (EditorGUI.EndChangeCheck())
				{
					serializedObject.ApplyModifiedProperties();
					target.SetTextureFilterMode();
				}
			}
			EditorGUILayout.EndFoldoutHeaderGroup();

			

			serializedObject.ApplyModifiedProperties();
		}


		[DrawGizmo(GizmoType.InSelectionHierarchy)]
		static void DrawGizmos(Heatmap target, GizmoType gizmoType)
		{
			Gizmos.color = Color.white;
			Gizmos.DrawWireCube(target.BoundsFromTransform.center, target.BoundsFromTransform.size);
		}
		#endregion
	}
}