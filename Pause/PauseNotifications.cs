// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Nikolay Melnikov <n.melnikov@depra.org>

using System;
using System.Collections.Generic;

namespace Depra.Pause
{
	public sealed class PauseNotifications : IDisposable
	{
		private readonly IPauseState _state;
		private readonly List<IPauseListener> _listeners = new();

		public PauseNotifications(IPauseState state, IEnumerable<IPauseListener> listeners)
		{
			_state = state;
			_state.Paused += OnPause;
			_state.Resumed += OnResume;
			AddRange(listeners);
		}

		public void Dispose()
		{
			if (_state != null)
			{
				_state.Paused -= OnPause;
				_state.Resumed -= OnResume;
			}

			Reset();
		}

		public void Add(IPauseListener listener)
		{
			if (_listeners.Contains(listener) == false)
			{
				_listeners.Add(listener);
			}
		}

		public void AddRange(IEnumerable<IPauseListener> listeners)
		{
			foreach (var listener in listeners)
			{
				Add(listener);
			}
		}

		public void Remove(IPauseListener listener)
		{
			if (_listeners.Contains(listener))
			{
				_listeners.Remove(listener);
			}
		}

		public void RemoveRange(IEnumerable<IPauseListener> listeners)
		{
			foreach (var listener in listeners)
			{
				Remove(listener);
			}
		}

		public void Reset() => _listeners.Clear();

		internal void OnPause()
		{
			foreach (var listener in _listeners)
			{
				listener.OnPause();
			}
		}

		internal void OnResume()
		{
			foreach (var listener in _listeners)
			{
				listener.OnResume();
			}
		}
	}
}