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
using System.Collections.Generic;
using Snakybo.RouteSystem;
using UnityEditor;

[CustomEditor(typeof(SimpleSpawnSystem))]
public class SimpleSpawnSystemEditor : Editor {
	public override void OnInspectorGUI()
	{
		EditorGUILayout.HelpBox("During play mode press the \"Spawn\" button to spawn an object at a random RouteNode", MessageType.Info);
		EditorGUILayout.Separator();

		DrawDefaultInspector();

		if(Application.isPlaying)
		{
			EditorGUILayout.Separator();

			if(GUILayout.Button("Spawn"))
				(target as SimpleSpawnSystem).SpawnObject();
		}
	}
}
