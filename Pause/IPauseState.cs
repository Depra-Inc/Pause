// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Nikolay Melnikov <n.melnikov@depra.org>

namespace Depra.Pause
{
	public interface IPauseState
	{
		event PauseDelegate Paused;
		event ResumeDelegate Resumed;

		bool IsPaused { get; }

		void Pause();
		void Resume();
	}
}