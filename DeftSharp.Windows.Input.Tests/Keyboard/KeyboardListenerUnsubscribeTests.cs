namespace DeftSharp.Windows.Input.Tests.Keyboard;

public sealed class KeyboardListenerUnsubscribeTests
{
    private async void RunListenerTest(Action<KeyboardListener> onTest)
    {
        var keyboardListener = new KeyboardListener();
        await Task.Run(() => onTest(keyboardListener));

        Assert.False(keyboardListener.IsListening,
            "The Unregister function is not called after unsubscribing from all events.");
    }

    [Fact]
    public void KeyboardListener_SubscribeUnsubscribe()
    {
        RunListenerTest(listener =>
        {
            listener.Subscribe(Key.A, _ => { });
            listener.Unsubscribe(Key.A);
        });
    }

    [Fact]
    public void KeyboardListener_SubscribeUnsubscribeAll()
    {
        RunListenerTest(listener =>
        {
            listener.Subscribe(Key.A, _ => { });
            listener.Unsubscribe();
        });
    }

    [Fact]
    public void KeyboardListener_SubscribeUnsubscribeMany()
    {
        RunListenerTest(listener =>
        {
            Key[] keys = { Key.W, Key.A, Key.S, Key.D };
            listener.Subscribe(keys, _ => { });

            foreach (var key in keys)
                listener.Unsubscribe(key);
        });
    }

    [Fact]
    public void KeyboardListener_SubscribeManyUnsubscribe()
    {
        RunListenerTest(listener =>
        {
            Key[] keys = { Key.W, Key.A, Key.S, Key.D };
            listener.Subscribe(keys, _ => { });
            listener.Unsubscribe(Key.D);
            listener.Unsubscribe(Key.S);
            listener.Unsubscribe(Key.A);
            listener.Unsubscribe(Key.W);
        });
    }

    [Fact]
    public void KeyboardListener_SubscribeCoupleUnsubscribeAll()
    {
        RunListenerTest(listener =>
        {
            Key[] keys = { Key.W, Key.A, Key.S, Key.D };
            listener.Subscribe(keys, _ => { });
            listener.Subscribe(Key.A, _ => { });
            listener.Unsubscribe();
        });
    }

    [Fact]
    public void KeyboardListener_UnsubscribeAll()
    {
        RunListenerTest(listener => listener.Unsubscribe());
    }

    [Fact]
    public void KeyboardListener_Empty()
    {
        RunListenerTest(_ => { });
    }

    [Fact]
    public void KeyboardListener_SubscribeCombinationUnsubscribeById()
    {
        RunListenerTest(listener =>
        {
            Key[] combination = { Key.W, Key.A };
            listener.SubscribeCombination(combination, () => { });
            var combinationId = listener.Combinations.First().Id;
            listener.Unsubscribe(combinationId);
        });
    }

    [Fact]
    public void KeyboardListener_SubscribeCombinationUnsubscribeAll()
    {
        RunListenerTest(listener =>
        {
            Key[] combination = { Key.W, Key.A };
            for (var i = 0; i < 5; i++)
                listener.SubscribeCombination(combination, () => { });

            listener.Unsubscribe();
        });
    }

    [Fact]
    public void KeyboardListener_SubscribeCombinationUnsubscribeSingleKey()
    {
        var keyboardListener = new KeyboardListener();
        Key[] combination = { Key.W, Key.A };
        keyboardListener.SubscribeCombination(combination, () => { });
        keyboardListener.Unsubscribe(Key.W);
        keyboardListener.Unsubscribe(Key.A);

        Assert.Single(keyboardListener.Combinations);
        Assert.True(keyboardListener.Combinations.All(x => x.Combination.SequenceEqual(combination.AsEnumerable())));
    }

    [Fact]
    public void KeyboardListener_SubscribeCombinationUnsubscribeCombinaiton()
    {
        var keyboardListener = new KeyboardListener();
        Key[] combination = { Key.W, Key.A };
        keyboardListener.SubscribeCombination(combination, () => { });
        keyboardListener.UnsubscribeCombination(combination);

        Assert.Empty(keyboardListener.Combinations);

    }
    [Fact]
    public void KeyboardListener_SubscribeCombinationUnsubscribeCombinaiton2()
    {
        var keyboardListener = new KeyboardListener();
        Key[] combination1 = { Key.W, Key.A };
        Key[] combination2 = { Key.W, Key.K };
        keyboardListener.SubscribeCombination(combination1, () => { });
        keyboardListener.SubscribeCombination(combination2, () => { });
        keyboardListener.UnsubscribeCombination(combination2);


        var test1 = keyboardListener.Combinations
            .FirstOrDefault(x=> x.Combination.SequenceEqual(combination2)) is null;
        Assert.True(test1);

        var test2 = keyboardListener.Combinations
            .FirstOrDefault(x => x.Combination.SequenceEqual(combination1)) != null;
        Assert.True(test2);

        Assert.Single(keyboardListener.Combinations);

    }

    [Fact]
    public void KeyboardListener_SubscribeSequenceUnsubscribeSingleKey()
    {
        var keyboardListener = new KeyboardListener();
        Key[] sequence = { Key.A, Key.K, Key.A, Key.B };
        keyboardListener.SubscribeSequence(sequence, () => { });
        keyboardListener.Unsubscribe(Key.A);
        keyboardListener.Unsubscribe(Key.K);
        keyboardListener.Unsubscribe(Key.B);

        Assert.Single(keyboardListener.Sequences);
        Assert.True(keyboardListener.Sequences.All(x => x.Sequence.SequenceEqual(sequence.AsEnumerable())));
    }


    [Fact]
    public void KeyboardListener_SubscribeSequenceUnsubscribeById()
    {
        RunListenerTest(listener =>
        {
            Key[] sequence = { Key.A, Key.K, Key.A, Key.B };
            listener.SubscribeSequence(sequence, () => { });
            var sequenceId = listener.Sequences.First().Id;
            listener.Unsubscribe(sequenceId);
        });
    }

    [Fact]
    public void KeyboardListener_SubscribeSequenceUnsubscribeAll()
    {
        RunListenerTest(listener =>
        {
            Key[] sequence = { Key.A, Key.K, Key.A, Key.B };
            for (int i = 0; i < 5; ++i)
                listener.SubscribeSequence(sequence, () => { });

            listener.Unsubscribe();
        });
    }


}