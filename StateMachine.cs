using System;
using System.Collections.Generic;
using UnityEngine;

namespace Drafts.StateMachines
{
    public abstract class StateMachine : MonoBehaviour
    {
        [ContextMenu("BackToDefault")]
        public abstract void BackToDefault();

        public abstract void SendStateMessage(object message);
    }

    public abstract class StateMachine<TContext, TState> : StateMachine where TState : IMachineState<TContext>
    {
        public TContext Context { get; private set; }
        public TState DefState { get; private set; }
        public TState PrevState { get; private set; }
        public TState CurrState { get; private set; }

        [SerializeField] private string currentState;
        private readonly Dictionary<Type, TState> _states = new();

        private void Update() => CurrState?.Tick(Context, Time.deltaTime);
        
        public void ChangeState<T>(TContext context, object data = null) where T : TState, new()
        {
            if (!_states.TryGetValue(typeof(T), out var state))
                _states[typeof(T)] = state = new T();
            Initialize(context, state, data);
        }
        
        public void ChangeState(TState next, object data = null)
        {
            PrevState = CurrState;
            CurrState = next;
            currentState = next.Name;

            PrevState?.Exit(Context, data);
            CurrState.Enter(Context, data);
        }

        public void Initialize<T>(TContext context, object data = null) where T : TState, new()
        {
            var state = new T();
            _states[typeof(T)] = state;
            Initialize(context, state, data);
        }
        
        public void Initialize(TContext context, TState defaultState, object data = null)
        {
            Context = context;
            DefState = defaultState;
            ChangeState(DefState, data);
            enabled = true;
        }

        [ContextMenu("BackToDefault")]
        public override void BackToDefault() => ChangeState(DefState);

        public override void SendStateMessage(object message) => CurrState.ReceiveMessage(Context, message);
    }
}