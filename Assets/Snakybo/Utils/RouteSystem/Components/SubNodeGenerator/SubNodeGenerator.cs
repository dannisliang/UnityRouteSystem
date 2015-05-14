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

namespace Snakybo.RouteSystem
{
	public class SubNodeGenerator : RouteComponent
	{
		[SerializeField] private float interval;

		public float Interval
		{
			get { return interval; }
		}

		#region Unity Callbacks
		protected override void Awake()
		{
			base.Awake();

			Generate();
		}
		#endregion

		#region API
		public void Generate()
		{
			RemoveExistingSubNodes();

			List<RouteNode> routeNodes = new List<RouteNode>(route.RouteNodes);
			
			for(int i = 0; i < routeNodes.Count - 1; i++)
				GenerateFromTo(routeNodes[i], routeNodes[i + 1]);

			if(route.Loop)
				GenerateFromTo(routeNodes[routeNodes.Count - 1], routeNodes[0]);
		}
		#endregion

		private void RemoveExistingSubNodes()
		{
			List<RouteNode> routeNodes = new List<RouteNode>(route.RouteNodes);

			foreach(RouteNode routeNode in routeNodes)
			{
				if(routeNode.GetType() != typeof(RouteSubNode))
					continue;

				if(!route.RemoveRouteNode(routeNode))
				{
					Debug.LogWarning("Unable to remove RouteNode!", routeNode);
					continue;
				}

				Destroy(routeNode);
			}
		}

		private void GenerateFromTo(RouteNode from, RouteNode to)
		{
			RouteNode lastRouteNode = from;

			Vector3 direction = (to.Position- from.Position).normalized;
			Vector3 lastPosition = from.Position;

			float distance = Vector3.Distance(from.Position, to.Position);

			int num = Mathf.FloorToInt(distance / interval);

			for (int i = 0; i < num; i++)
			{
				lastPosition += direction * interval;

				if(Vector3.Distance(lastPosition, to.Position) < interval)
					break;

				CreateSubNode(from.transform, lastPosition, ref lastRouteNode);
			}
		}

		private void CreateSubNode(Transform parent, Vector3 position, ref RouteNode after)
		{
			GameObject obj = new GameObject();
			obj.transform.SetParent(parent);
			obj.transform.position = position;
			obj.name = "Route Sub Node";

			RouteSubNode routeSubNode = obj.AddComponent<RouteSubNode>();
			route.AddRouteNodeAfter(routeSubNode, after);

			after = routeSubNode;
		}
	}

	public static partial class RouteExtensions
	{
		public static RouteNode GetNextRealNode(this Route route, RouteNode from)
		{
			RouteNode result = route.GetNext(from);

			while(result.GetType() == typeof(RouteSubNode))
			{
				from = result;
				result = route.GetNext(from);
			}

			return result;
		}
	}
}
