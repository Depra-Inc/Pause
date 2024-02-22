// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Nikolay Melnikov <n.melnikov@depra.org>

using System;
using System.Collections.Generic;

namespace Depra.Pause
{
	public sealed class PauseService : IPauseService, IDisposable
	{
		private readonly List<IPauseInput> _inputs = new();
		private readonly List<IPauseListener> _listeners = new();

		public PauseService(List<IPauseInput> inputs, List<IPauseListener> listeners)
		{
			inputs.ForEach(Add);
			listeners.ForEach(Add);
		}

		public void Dispose()
		{
			foreach (var input in _inputs)
			{
				input.Pause -= Pause;
				input.Resume -= Resume;
			}

			_inputs.Clear();
		}

		public bool Paused { get; internal set; }

		public void Add(IPauseInput input)
		{
			if (_inputs.Contains(input))
			{
				return;
			}

			_inputs.Add(input);
			input.Pause += Pause;
			input.Resume += Resume;
		}

		public void Add(IPauseListener listener)
		{
			if (_listeners.Contains(listener) == false)
			{
				_listeners.Add(listener);
			}
		}

		private void Pause()
		{
			if (Paused)
			{
				return;
			}

			Paused = true;
			foreach (var listener in _listeners)
			{
				listener.Pause();
			}
		}

		private void Resume()
		{
			if (Paused == false)
			{
				return;
			}

			Paused = false;
			foreach (var listener in _listeners)
			{
				listener.Resume();
			}
		}

		void IPauseService.Remove(IPauseInput input)
		{
			if (_inputs.Contains(input) == false)
			{
				return;
			}

			_inputs.Add(input);
			input.Pause -= Pause;
			input.Resume -= Resume;
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