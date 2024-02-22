// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Nikolay Melnikov <n.melnikov@depra.org>

namespace Depra.Pause
{
    public interface IPauseService
    {
        bool Paused { get; }

        void Add(IPauseInput input);

        void Add(IPauseListener listener);

        void Remove(IPauseInput input);

        void Remove(IPauseListener listener);
    }
}