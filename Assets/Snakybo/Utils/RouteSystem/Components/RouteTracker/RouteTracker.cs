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
	public class RouteTracker : RouteComponent
	{
		[SerializeField] private List<Object> spawnedObjects;

		protected override void Awake()
		{
			base.Awake();

			spawnedObjects = new List<Object>();
		}

		protected void OnEnable()
		{
			route.OnObjectAdded += OnObjectAdded;
			route.OnObjectRemoved += OnObjectRemoved;
		}

		protected void OnDisable()
		{
			route.OnObjectAdded -= OnObjectAdded;
			route.OnObjectRemoved -= OnObjectRemoved;
		}

		private void OnObjectAdded(Object obj)
		{
			spawnedObjects.Add(obj);
        }

		private void OnObjectRemoved(Object obj)
		{
			spawnedObjects.Remove(obj);
		}
	}
}