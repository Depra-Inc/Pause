// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Nikolay Melnikov <n.melnikov@depra.org>

namespace Depra.Pause.UnitTests;

public sealed class PauseNotificationsTests
{
	[Fact]
	public void PauseInput_ShouldBePassedToListener()
	{
		// Arrange:
		var input = Substitute.For<IPauseInputSource>();
		var listener = Substitute.For<IPauseListener>();
		var state = new PauseState();
		var notifications = new PauseNotifications(state, [listener]);

		// Act:
		input.PauseTriggered += Raise.Event<Action>();

		// Assert:
		listener.Received(1).OnPause();
	}

	[Fact]
	public void PauseInput_ShouldBePassedToListeners()
	{
		// Arrange:
		const int LISTENER_COUNT = 3;
		var input = Substitute.For<IPauseInputSource>();
		var listeners = new List<IPauseListener>(LISTENER_COUNT);
		for (var index = 0; index < LISTENER_COUNT; index++)
		{
			listeners.Add(Substitute.For<IPauseListener>());
		}

		var state = new PauseState();
		var notifications = new PauseNotifications(state, listeners);

		// Act:
		input.PauseTriggered += Raise.Event<Action>();

		// Assert:
		foreach (var listener in listeners)
		{
			listener.Received(1).OnPause();
		}
	}

	[Fact]
	public void ResumeInput_ShouldBePassedToListener()
	{
		// Arrange:
		var input = Substitute.For<IPauseInputSource>();
		var listener = Substitute.For<IPauseListener>();
		var state = new PauseState();
		var notifications = new PauseNotifications(state, [listener]);
		state.IsPaused = true;

		// Act:
		input.ResumeTriggered += Raise.Event<Action>();

		// Assert:
		listener.Received(1).OnResume();
	}

	[Fact]
	public void ResumeInput_ShouldBePassedToListeners()
	{
		// Arrange:
		const int LISTENER_COUNT = 3;
		var input = Substitute.For<IPauseInputSource>();
		var listeners = new List<IPauseListener>(LISTENER_COUNT);
		for (var index = 0; index < LISTENER_COUNT; index++)
		{
			listeners.Add(Substitute.For<IPauseListener>());
		}

		var state = new PauseState();
		var notifications = new PauseNotifications(state, listeners);
		state.IsPaused = true;

		// Act:
		input.ResumeTriggered += Raise.Event<Action>();

		// Assert:
		foreach (var listener in listeners)
		{
			listener.Received(1).OnResume();
		}
	}
}