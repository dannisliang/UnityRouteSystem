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
	public class RouteSpawnSystem
	{
		private static Dictionary<Object, Route> spawnedObjects = new Dictionary<Object, Route>();

		/// <summary>
		/// Instantiate an object on the route, this is a simple extension to Unity's default Instantiate method
		/// and does little besides setting instantiating te object on the specified node and registering the object
		/// on the route.
		/// </summary>
		/// <remarks>
		/// If you use your own spawn system, all you'll have to do is call <code>Route.AddObject(Object, RouteNode)</code>
		/// to register it. 
		/// </remarks>
		/// <param name="original">The object to instantiate.</param>
		/// <param name="route">The route to instantiate the object on.</param>
		/// <param name="node">The node the object should be instantiated on.</param>
		/// <returns>The instantiated object.</returns>
		public static Object Instantiate(Object original, Route route, RouteNode node)
		{
			Object obj = Object.Instantiate(original, node.Position, node.Rotation);

			route.AddObject(obj, node);
			spawnedObjects.Add(obj, route);

			return obj;
		}

		/// <summary>
		/// Destroy the specified object, this checks if it's registered on a route and unregisters is if so.
		/// </summary>
		/// <remarks>
		/// If you use your own spawn system, all you'll have to do is call <code>Route.RemoveObject(Object)</code>
		/// to unregister it.
		/// </remarks>
		/// <param name="obj">The object to destroy.</param>
		public static void Destroy(Object obj)
		{
			Destroy(obj, 0);
		}

		public static void Destroy(Object obj, float t)
		{
			if(spawnedObjects.ContainsKey(obj))
				spawnedObjects[obj].RemoveObject(obj);

			Object.Destroy(obj, t);
		}
	}
}