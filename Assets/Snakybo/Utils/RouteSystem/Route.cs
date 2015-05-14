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

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Snakybo.RouteSystem
{
	public class Route : MonoBehaviour
	{
		[SerializeField] private List<RouteNode> routeNodes;
		[SerializeField] private bool loop;

		public IEnumerable<RouteNode> RouteNodes {
			get { return routeNodes; }
		}

		public int Size {
			get { return routeNodes.Count; }
		}

		public bool Loop {
			get { return loop; }
		}

		#region Unity Callbacks
		protected void OnEnable()
		{
			RouteManager.RegisterRoute(this);
		}

		protected void OnDisable()
		{
			RouteManager.UnregisterRoute(this);
		}

		protected void OnDrawGizmos()
		{
			Gizmos.color = Color.green;
			DrawGizmos();
		}

		protected void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.cyan;
			DrawGizmos();
		}
		#endregion

		#region API
		public bool AddRouteNode(RouteNode routeNode)
		{
			if(Contains(routeNode))
				return false;

			routeNodes.Add(routeNode);
			return true;
		}

		public bool AddRouteNodeAfter(RouteNode routeNode, RouteNode after)
		{
			if(Contains(routeNode))
				return false;

			if(!Contains(after))
				return false;

			int index = IndexOf(after) + 1;
			routeNodes.Insert(index, routeNode);

			return true;
		}

		public bool RemoveRouteNode(RouteNode routeNode)
		{
			return routeNodes.Remove(routeNode);
		}

		public RouteNode GetNext(RouteNode from)
		{
			int index = IndexOf(from);

			if(index + 1 >= routeNodes.Count)
			{
				if(!loop)
					return null;

				return routeNodes[0];
			}

			return routeNodes[index + 1];
		}

		public RouteNode GetFirst()
		{
			if(routeNodes.Count > 0)
				return routeNodes[0];

			return null;
		}

		public RouteNode GetLast()
		{
			if(routeNodes.Count > 0)
				return routeNodes[routeNodes.Count - 1];

			return null;
		}

		public RouteNode GetNearest(Vector3 from)
		{
			float distance = float.PositiveInfinity;
			RouteNode nearest = null;

			foreach(RouteNode routeNode in routeNodes)
			{
				float dist = Vector3.Distance(from, routeNode.Position);

				if(dist < distance)
				{
					distance = dist;
					nearest = routeNode;
				}
			}

			return nearest;
		}

		public RouteNode GetRandom()
		{
			return routeNodes[UnityEngine.Random.Range(0, routeNodes.Count)];
		}
		
		public bool Contains(RouteNode routeNode)
		{
			return routeNodes.Contains(routeNode);
		}
		#endregion

		private void DrawGizmos()
		{
			if(routeNodes == null || routeNodes.Count == 0)
				return;

			for(int i = 0; i < routeNodes.Count; i++)
			{
				RouteNode currentNode = routeNodes[i];
				RouteNode nextNode;

				if(i + 1 >= routeNodes.Count)
				{
					if(!loop)
						break;

					nextNode = routeNodes[0];
				}
				else
				{
					nextNode = routeNodes[i + 1];
				}

				if(currentNode != null && nextNode != null)
					Gizmos.DrawLine(currentNode.Position, nextNode.Position);
			}
		}

		private int IndexOf(RouteNode node)
		{
			for(int i = 0; i < routeNodes.Count; i++)
				if(routeNodes[i] == node)
					return i;

			throw new ArgumentException("The node " + node + " isn't available in this route.");
		}
	}
}