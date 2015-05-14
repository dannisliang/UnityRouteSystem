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

using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Snakybo.RouteSystem
{
	[CustomEditor(typeof(Route))]
	public class RouteEditor : Editor
	{
		private ReorderableList reorderableList;

		private SerializedProperty prop_routeNodes;
		private SerializedProperty prop_loop;

		private Route route;

		#region Unity Callbacks
		protected virtual void OnEnable()
		{
			prop_routeNodes = serializedObject.FindProperty("routeNodes");
			prop_loop = serializedObject.FindProperty("loop");

			reorderableList = new ReorderableList(serializedObject, prop_routeNodes, true, true, false, true);
			reorderableList.drawHeaderCallback += DrawHeaderCallback;
			reorderableList.drawElementCallback += DrawElementCallback;

			route = target as Route;
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			Draw();

			serializedObject.ApplyModifiedProperties();
		}

		protected void OnSceneGUI()
		{
			if(Application.isPlaying)
				return;

			if(target == route)
			{
				Event evt = Event.current;

				if(evt.type == EventType.MouseUp && evt.button == 1)
				{
					if(evt.shift)
					{
						RemoveRouteNode();
					}
					else
					{
						AddRouteNode();
					}
				}	
			}
		}
		#endregion

		#region Reorderable List Callbacks
		private void DrawHeaderCallback(Rect rect)
		{
			EditorGUI.LabelField(rect, "Nodes");
		}

		private void DrawElementCallback(Rect rect, int index, bool isActive, bool isFocused)
		{
			SerializedProperty element = reorderableList.serializedProperty.GetArrayElementAtIndex(index);

			rect.y += 2;

			EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), element, GUIContent.none);
		}
		#endregion

		private void Draw()
		{
			EditorGUILayout.HelpBox("Right click on a RouteNode in the scene to add it to the route.", MessageType.Info);
			EditorGUILayout.HelpBox("Shift + right click on a RouteNode in the scene to remove it from the route.", MessageType.Info);			
            EditorGUILayout.Separator();

			reorderableList.DoLayoutList();

			EditorGUILayout.Separator();
			EditorGUILayout.PropertyField(prop_loop);
		}

		private void AddRouteNode()
		{
			RouteNode routeNode = GetRouteNode();

			if(routeNode != null)
			{
				SerializedProperty prop = reorderableList.serializedProperty;

				if(prop.arraySize > 0 && prop.GetArrayElementAtIndex(prop.arraySize - 1).objectReferenceValue == routeNode)
					return;

				route.AddRouteNode(routeNode);
			}
		}

		private void RemoveRouteNode()
		{
			RouteNode routeNode = GetRouteNode();

			if(routeNode != null)
				route.RemoveRouteNode(routeNode);
		}

		private RouteNode GetRouteNode()
		{
			RaycastHit hit;

			Vector2 mouse = Event.current.mousePosition;
			mouse.y = Screen.height - mouse.y - 35f;

			Ray ray = Camera.current.ScreenPointToRay(mouse);

			if(Physics.Raycast(ray, out hit))
				return hit.collider.GetComponent<RouteNode>();

			return null;
		}
	}
}