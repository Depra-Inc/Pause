// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Nikolay Melnikov <n.melnikov@depra.org>

using System;
using System.Collections.Generic;

namespace Depra.Pause
{
	public sealed class PauseService : IPauseService, IDisposable
	{
		private readonly IPauseInput _input;
		private readonly List<IPauseListener> _listeners = new();

		public PauseService(IPauseInput input, params IPauseListener[] listeners)
		{
			Array.ForEach(listeners, Add);

			_input = input;
			_input.Pause += Pause;
			_input.Resume += Resume;
		}

		public void Dispose()
		{
			_input.Pause -= Pause;
			_input.Resume -= Resume;
		}

		public bool IsPaused { get; internal set; }

		public void Add(IPauseListener listener)
		{
			if (_listeners.Contains(listener) == false)
			{
				_listeners.Add(listener);
			}
		}

		private void Pause()
		{
			if (IsPaused)
			{
				return;
			}

			IsPaused = true;
			foreach (var control in _listeners)
			{
				control.Pause();
			}
		}

		private void Resume()
		{
			if (IsPaused == false)
			{
				return;
			}

			IsPaused = false;
			foreach (var listener in _listeners)
			{
				listener.Resume();
			}
		}

		void IPauseService.Remove(IPauseListener listener)
		{
			if (_listeners.Contains(listener))
			{
				_listeners.Remove(listener);
			}
		}
	}
}