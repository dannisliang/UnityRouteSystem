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
	public class RouteNodeDetector : MonoBehaviour, IRouteData
	{
		public delegate void OnRouteNodeReachedHandler(RouteNode node);
		public event OnRouteNodeReachedHandler OnNodeReached = delegate { };

		[SerializeField] private float maxDistance;

		private RouteNode lastNode;
		private Route route;

		public void Update()
		{
			foreach(RouteNode node in route.RouteNodes)
			{
				if(node == lastNode)
					continue;

				if(Vector3.Distance(transform.position, node.Position) < maxDistance)
				{
					lastNode = node;
					OnNodeReached(node);
				}
			}
		}

		public void OnAddedToRoute(Route route, RouteNode node)
		{
			this.route = route;
		}

		public void OnRemovedFromRoute(Route route)
		{
		}

		protected void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.magenta;
			Gizmos.DrawWireSphere(transform.position, maxDistance);
		}
	}
}