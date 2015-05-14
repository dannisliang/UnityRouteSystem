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

public class SimpleSpawnSystem : MonoBehaviour {
	[SerializeField] private GameObject prefab;

	public void SpawnObject()
	{
		RouteNode routeNode = GetRouteNode();

		Instantiate(prefab, routeNode.Position, Quaternion.identity);
	}

	private RouteNode GetRouteNode()
	{
		List<Route> routes = new List<Route>(RouteManager.Routes);

		return routes[Random.Range(0, routes.Count)].GetRandom();
	}
}
