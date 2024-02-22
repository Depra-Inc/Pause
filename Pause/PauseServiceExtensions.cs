// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Nikolay Melnikov <n.melnikov@depra.org>

using System.Collections.Generic;

namespace Depra.Pause
{
	public static class PauseServiceExtensions
	{
		public static void AddRange(this IPauseService self, IEnumerable<IPauseListener> listeners)
		{
			foreach (var listener in listeners)
			{
				self.Add(listener);
			}
		}

		public static void RemoveRange(this IPauseService self, IEnumerable<IPauseListener> listeners)
		{
			foreach (var listener in listeners)
			{
				self.Remove(listener);
			}
		}
	}
}