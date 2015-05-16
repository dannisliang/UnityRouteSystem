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
	public class RouteManager : MonoBehaviour
	{
		public delegate void OnRouteRegisteredHandler(Route route);
		public static event OnRouteRegisteredHandler OnRouteRegistered = delegate {};

		public delegate void OnRouteUnregisteredHandler(Route route);
		public static event OnRouteUnregisteredHandler OnRouteUnregistered = delegate {};

		private static List<Route> routes;

		public static IEnumerable<Route> Routes {
			get {
				if(routes == null)
					routes = new List<Route>();

				return routes;
			}
		}

		#region Internal API
		internal static void RegisterRoute(Route route)
		{
			if(routes == null)
				routes = new List<Route>();

			if(!routes.Contains(route))
			{
				routes.Add(route);
				OnRouteRegistered(route);
			}
		}

		internal static void UnregisterRoute(Route route)
		{
			if(routes == null)
				routes = new List<Route>();

			if(routes.Remove(route))
				OnRouteUnregistered(route);
		}
		#endregion
	}
}