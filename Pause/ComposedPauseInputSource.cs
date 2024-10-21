// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Nikolay Melnikov <n.melnikov@depra.org>

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Depra.Pause
{
	public sealed class ComposedPauseInputSource : IPauseInputSource, IDisposable
	{
		private readonly IEnumerable<IPauseInputSource> _inputs;

		public event Action PauseTriggered;
		public event Action ResumeTriggered;

		public ComposedPauseInputSource(IEnumerable<IPauseInputSource> inputs)
		{
			_inputs = inputs;
			foreach (var input in _inputs)
			{
				input.PauseTriggered += InvokePauseTriggered;
				input.ResumeTriggered += InvokeResumeTriggered;
			}
		}

		public void Dispose()
		{
			foreach (var input in _inputs)
			{
				input.PauseTriggered -= InvokePauseTriggered;
				input.ResumeTriggered -= InvokeResumeTriggered;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void InvokePauseTriggered() => PauseTriggered?.Invoke();

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void InvokeResumeTriggered() => ResumeTriggered?.Invoke();
	}
}