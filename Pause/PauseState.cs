// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Nikolay Melnikov <n.melnikov@depra.org>

namespace Depra.Pause
{
	public sealed class PauseState : IPauseState
	{
		public event PauseDelegate Paused;
		public event ResumeDelegate Resumed;

		public bool IsPaused { get; internal set; }

		void IPauseState.Pause()
		{
			if (IsPaused)
			{
				return;
			}

			IsPaused = true;
			Paused?.Invoke();
		}

		void IPauseState.Resume()
		{
			if (IsPaused == false)
			{
				return;
			}

			IsPaused = false;
			Resumed?.Invoke();
		}
	}
}