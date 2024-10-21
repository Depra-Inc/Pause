// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Nikolay Melnikov <n.melnikov@depra.org>

using System.Collections.Generic;

namespace Depra.Pause
{
	public sealed class PauseInput
	{
		private readonly IPauseState _state;
		private readonly List<IPauseInputSource> _inputs = new();

		public PauseInput(IPauseState state) => _state = state;

		public PauseInput(IPauseState state, IEnumerable<IPauseInputSource> inputs) : this(state)
		{
			foreach (var input in inputs)
			{
				Add(input);
			}
		}

		public void Dispose()
		{
			if (_state != null)
			{
				foreach (var input in _inputs)
				{
					input.PauseTriggered -= _state.Pause;
					input.ResumeTriggered -= _state.Resume;
				}
			}

			_inputs.Clear();
		}

		public void Add(IPauseInputSource input)
		{
			if (_inputs.Contains(input))
			{
				return;
			}

			_inputs.Add(input);
			input.PauseTriggered += _state.Pause;
			input.ResumeTriggered += _state.Resume;
		}

		public void AddRange(IEnumerable<IPauseInputSource> inputs)
		{
			foreach (var listener in inputs)
			{
				Add(listener);
			}
		}

		public void Remove(IPauseInputSource input)
		{
			if (_inputs.Contains(input) == false)
			{
				return;
			}

			_inputs.Remove(input);
			input.PauseTriggered -= _state.Pause;
			input.ResumeTriggered -= _state.Resume;
		}

		public void RemoveRange(IEnumerable<IPauseInputSource> inputs)
		{
			foreach (var listener in inputs)
			{
				Remove(listener);
			}
		}
	}
}