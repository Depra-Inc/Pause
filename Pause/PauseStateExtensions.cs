// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Nikolay Melnikov <n.melnikov@depra.org>

namespace Depra.Pause
{
	public static class PauseStateExtensions
	{
		public static void Toggle(this IPauseState state)
		{
			if (state.IsPaused)
			{
				state.Resume();
			}
			else
			{
				state.Pause();
			}
		}
	}
}