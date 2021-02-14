using System;
using my_fsm.StateMachines;
using Stateless;

namespace my_fsm
{
    public class RepairOrder
    {
        RoModeStateMachine _roModeStateMachine { get; set; }
        PartStatusStateMachine _partStatusStateMachine { get; set; }

        public RepairOrder(
            RoModeStateMachine.State initialRoModeState = RoModeStateMachine.State.Dispatch,
            PartStatusStateMachine.State initialPartStatusState = PartStatusStateMachine.State.Default)
        {
            this._roModeStateMachine = new RoModeStateMachine(initialRoModeState);
            this._partStatusStateMachine = new PartStatusStateMachine(initialPartStatusState);
        }

        public void GoToNextRoMode()
        {
            this._roModeStateMachine.GoToNextState();
        }

        public void GoToNextPartStatus()
        {
            this._partStatusStateMachine.GoToNextState();
        }
    }
}
