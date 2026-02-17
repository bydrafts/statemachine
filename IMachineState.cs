namespace Drafts.StateMachines
{
    public interface IMachineState<in T>
    {
        string Name => GetType().Name;
        void Enter(T ctx, object data);
        void Exit(T ctx, object data);
        void Tick(T ctx, float deltaTime);
        void ReceiveMessage(T ctx, object message);
    }
}