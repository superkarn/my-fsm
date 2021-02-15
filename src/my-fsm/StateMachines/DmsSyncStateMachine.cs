using System;
using Stateless;

namespace my_fsm.StateMachines
{
    public class DmsSyncStateMachine : BaseStateMachine<DmsSyncStateMachine.State, DmsSyncStateMachine.Trigger>
    {
        public enum State
        {
            Synced,
            Unsynced,
        }

        public enum Trigger
        {
            SendToSynced,
            SendToUnsynced,
        }

        public DmsSyncStateMachine() : base(State.Synced)
        { }
        
        public DmsSyncStateMachine(State initialState) : base(initialState)
        { }

        public override void SetUp()
        {
            this.StateMachine.Configure(State.Synced)
                .Ignore(Trigger.SendToSynced)
                .Permit(Trigger.SendToUnsynced, State.Unsynced)
                .OnEntry(() => this.BroadcastStateEntry())
                .OnExit(() => this.BroadcastStateExit());
                
            this.StateMachine.Configure(State.Unsynced)
                .Ignore(Trigger.SendToUnsynced)
                .Permit(Trigger.SendToSynced, State.Synced)
                .OnEntry(() => this.BroadcastStateEntry())
                .OnExit(() => this.BroadcastStateExit());
        }
    }
}
