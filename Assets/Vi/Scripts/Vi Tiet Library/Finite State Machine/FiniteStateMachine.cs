using System.Collections.Generic;

namespace FiniteStateMachine.StateInterface
{
    public delegate bool TransitionCondition();
    public delegate void StateAction();

    public interface IState
    {
        void Update(float deltaTime);
        void OnStateEnter();
        void OnStateExit();
    }

    public class Transition
    {
        public Transition(string destinationState, TransitionCondition condition)
        {
            DestinationState = destinationState;
            EvaluateCondition = condition;
        }

        public string DestinationState { get; }
        public TransitionCondition EvaluateCondition { get; }
    }

    public class EmptyState : IState
    {
        public void OnStateEnter()
        {
        }

        public void OnStateExit()
        {
        }

        public void Update(float deltaTime)
        {
        }
    }

    public class FSM
    {
        private IState activeState;
        private Dictionary<string, IState> states;
        private Dictionary<IState, List<Transition>> transitions;

        public FSM()
        {
            activeState = new EmptyState();
            states = new Dictionary<string, IState>();
            transitions = new Dictionary<IState, List<Transition>>();
        }

        public void AddState(string name, IState state)
        {
            if (states.Count == 0)
            {
                activeState = state;
                InitiateStateMachine(activeState);
            }

            states.Add(name, state);
        }

        public void AddTransition(string fromStateName, string toStateName, TransitionCondition condition)
        {
            IState fromState = new EmptyState();
            if (states[fromStateName] != fromState) fromState = states[fromStateName];
            Transition transition = new Transition(toStateName, condition);

            if (!states.ContainsKey(toStateName) && !states.TryGetValue(fromStateName, out fromState))
                return;

            if (!transitions.ContainsKey(fromState))
                transitions.Add(fromState, new List<Transition>());

            if (!transitions[fromState].Contains(transition))
                transitions[fromState].Add(transition);
        }

        public void Update(float deltaTime)
        {
            activeState.Update(deltaTime);

            if (transitions.Count > 0 && transitions.ContainsKey(activeState))
            {
                foreach (Transition transition in transitions[activeState])
                {
                    if (transition.EvaluateCondition())
                    {
                        TransitionTo(transition.DestinationState);
                    }
                }
            }
        }

        public void TransitionTo(string stateName)
        {
            activeState.OnStateExit();
            activeState = states[stateName];
            activeState.OnStateEnter();
        }

        public void InitiateStateMachine(IState state)
        {
            state.OnStateEnter();
        }
    }
}

namespace FiniteStateMachine.StatePolymorphism
{
    public delegate bool TransitionCondition();
    public delegate void StateAction();

    public class State
    {
        virtual public void Update(float deltaTime)
        {
        }

        virtual public void OnStateEnter()
        {
        }

        virtual public void OnStateExit()
        {
        }
    }

    public class Transition
    {
        public Transition(string destinationState, TransitionCondition condition)
        {
            DestinationState = destinationState;
            EvaluateCondition = condition;
        }

        public string DestinationState { get; }
        public TransitionCondition EvaluateCondition { get; }
    }

    public class EmptyState : State
    {
        override public void OnStateEnter()
        {
        }

        override public void OnStateExit()
        {
        }

        override public void Update(float deltaTime)
        {
        }
    }

    public class FSM
    {
        private State activeState;
        private Dictionary<string, State> states;
        private Dictionary<State, List<Transition>> transitions;

        public FSM()
        {
            activeState = new EmptyState();
            states = new Dictionary<string, State>();
            transitions = new Dictionary<State, List<Transition>>();
        }

        public void AddState(string name, State state)
        {
            if (states.Count == 0)
            {
                activeState = state;
                InitiateStateMachine(activeState);
            }

            states.Add(name, state);
        }

        public void AddTransition(string fromStateName, string toStateName, TransitionCondition condition)
        {
            State fromState = new EmptyState();
            if (states[fromStateName] != fromState) fromState = states[fromStateName];
            Transition transition = new Transition(toStateName, condition);

            if (!states.ContainsKey(toStateName) && !states.TryGetValue(fromStateName, out fromState))
                return;

            if (!transitions.ContainsKey(fromState))
                transitions.Add(fromState, new List<Transition>());

            if (!transitions[fromState].Contains(transition))
                transitions[fromState].Add(transition);
        }

        public void Update(float deltaTime)
        {
            activeState.Update(deltaTime);

            if (transitions.Count > 0 && transitions.ContainsKey(activeState))
            {
                foreach (Transition transition in transitions[activeState])
                {
                    if (transition.EvaluateCondition())
                    {
                        TransitionTo(transition.DestinationState);
                    }
                }
            }
        }

        public void TransitionTo(string stateName)
        {
            activeState.OnStateExit();
            activeState = states[stateName];
            activeState.OnStateEnter();
        }

        public void InitiateStateMachine(State state)
        {
            state.OnStateEnter();
        }
    }
}