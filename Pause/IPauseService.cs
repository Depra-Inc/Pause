// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Nikolay Melnikov <n.melnikov@depra.org>

namespace Depra.Pause
{
    public interface IPauseService
    {
        bool IsPaused { get; }

        void Add(IPauseListener listener);

        void Remove(IPauseListener listener);
    }
}