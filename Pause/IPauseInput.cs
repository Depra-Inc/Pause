// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Nikolay Melnikov <n.melnikov@depra.org>

using System;

namespace Depra.Pause
{
	public interface IPauseInput
	{
		event Action Pause;

		event Action Resume;
	}
}