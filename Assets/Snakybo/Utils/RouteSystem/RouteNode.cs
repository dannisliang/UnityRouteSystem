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

namespace Snakybo.RouteSystem
{
	[ExecuteInEditMode]
	public class RouteNode : MonoBehaviour
	{
		public Vector3 Position { get { return transform.position; } }

		#region Unity Callbacks
		protected void Start()
		{
			if(Application.isPlaying)
			{
				Destroy(collider);
			}
			else
			{
				if(collider == null)
				{
					BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
					boxCollider.size = new Vector3(1, 0.3f, 1);
				}
			}
		}

		protected virtual void OnDrawGizmos()
		{
			Gizmos.color = Color.green;
			Gizmos.DrawCube(Position, new Vector3(1, 0.3f, 1));
		}
		#endregion
	}
}