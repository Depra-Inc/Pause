// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Nikolay Melnikov <n.melnikov@depra.org>

namespace Depra.Pause.UnitTests;

public sealed class PauseServiceTests
{
	[Fact]
	public void PauseInput_ShouldBePassedToListener()
	{
		// Arrange:
		var input = Substitute.For<IPauseInput>();
		var listener = Substitute.For<IPauseListener>();
		_ = new PauseService(input, listener);

		// Act:
		input.Pause += Raise.Event<Action>();

		// Assert:
		listener.Received(1).Pause();
	}

	[Fact]
	public void PauseInput_ShouldBePassedToListeners()
	{
		// Arrange:
		const int LISTENER_COUNT = 3;
		var input = Substitute.For<IPauseInput>();
		var listeners = new IPauseListener[LISTENER_COUNT];
		for (var index = 0; index < LISTENER_COUNT; index++)
		{
			listeners[index] = Substitute.For<IPauseListener>();
		}

		_ = new PauseService(input, listeners);

		// Act:
		input.Pause += Raise.Event<Action>();

		// Assert:
		foreach (var listener in listeners)
		{
			listener.Received(1).Pause();
		}
	}

	[Fact]
	public void ResumeInput_ShouldBePassedToListener()
	{
		// Arrange:
		var input = Substitute.For<IPauseInput>();
		var listener = Substitute.For<IPauseListener>();
		var service = new PauseService(input, listener);
		service.IsPaused = true;

		// Act:
		input.Resume += Raise.Event<Action>();

		// Assert:
		listener.Received(1).Resume();
	}

	[Fact]
	public void ResumeInput_ShouldBePassedToListeners()
	{
		// Arrange:
		const int LISTENER_COUNT = 3;
		var input = Substitute.For<IPauseInput>();
		var listeners = new IPauseListener[LISTENER_COUNT];
		for (var index = 0; index < LISTENER_COUNT; index++)
		{
			listeners[index] = Substitute.For<IPauseListener>();
		}

		var service = new PauseService(input, listeners);
		service.IsPaused = true;

		// Act:
		input.Resume += Raise.Event<Action>();

		// Assert:
		foreach (var listener in listeners)
		{
			listener.Received(1).Resume();
		}
	}
}