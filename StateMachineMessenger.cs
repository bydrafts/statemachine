using UnityEngine;

namespace Drafts.StateMachines
{
    public abstract class StateMachineMessenger : MonoBehaviour
    {
        public StateMachine target;

        public void StateSendString(string message) => target.SendStateMessage(message);
        public void StateSendInt(int message) => target.SendStateMessage(message);
        public void StateSendFloat(float message) => target.SendStateMessage(message);
        public void SendStateObject(Object message) => target.SendStateMessage(message);
        public void SendStateMessage(object message) => target.SendStateMessage(message);
    }
}