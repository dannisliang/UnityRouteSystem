// This file is part of Snakybo's Route System.
//
// Snakybo's Route System is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// Snakybo's Route System is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with Snakybo's Route System. If not, see<http://www.gnu.org/licenses/>.

using UnityEngine;
using UnityEditor;

namespace Snakybo.RouteSystem
{
	[CustomEditor(typeof(RouteTracker))]
	public class RouteTrackerEditor : Editor
	{
		private SerializedProperty prop_spawnedObjects;

		protected void OnEnable()
		{
			prop_spawnedObjects = serializedObject.FindProperty("spawnedObjects");
		}

		public override void OnInspectorGUI()
		{
			if(!Application.isPlaying)
				return;

			serializedObject.Update();
			
			EditorGUILayout.LabelField("Active objects: " + prop_spawnedObjects.arraySize);

			prop_spawnedObjects.isExpanded = EditorGUILayout.Foldout(prop_spawnedObjects.isExpanded, "Spawned Objects");

			if(prop_spawnedObjects.isExpanded)
			{
				EditorGUI.indentLevel++;

				for(int i = 0; i < prop_spawnedObjects.arraySize; i++)
				{
					SerializedProperty element = prop_spawnedObjects.GetArrayElementAtIndex(i);

					if(element.objectReferenceValue == null)
					{
						prop_spawnedObjects.DeleteArrayElementAtIndex(i);
						continue;
					}

					GUI.enabled = false;
					EditorGUILayout.PropertyField(element);
					GUI.enabled = true;
				}

				EditorGUI.indentLevel--;
			}

			serializedObject.ApplyModifiedProperties();
		}
	}
}