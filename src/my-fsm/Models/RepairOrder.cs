using System.Collections.Generic;
using my_fsm.StateMachines;

namespace my_fsm.Models
{
    public class RepairOrder
    {
        RoModeStateMachine _roModeStateMachine { get; set; }
        PartStatusStateMachine _partStatusStateMachine { get; set; }

        IList<RepairOrderLine> Lines { get; set; }

        public RepairOrder(
            RoModeStateMachine.State initialRoModeState = RoModeStateMachine.State.Dispatch,
            PartStatusStateMachine.State initialPartStatusState = PartStatusStateMachine.State.Default)
        {
            this._roModeStateMachine = new RoModeStateMachine(initialRoModeState);
            this._partStatusStateMachine = new PartStatusStateMachine(initialPartStatusState);

            this.Lines = new List<RepairOrderLine>();
        }

        #region RO stuff
        public void GoToNextRoMode()
        {
            this._roModeStateMachine.GoToNextState();
        }

        public void GoToNextPartStatus()
        {
            this._partStatusStateMachine.GoToNextState();
        }

        public void Close()
        {
            this._roModeStateMachine.StateMachine.Fire(RoModeStateMachine.Trigger.SendToClosed);
        }
        #endregion

        #region Lines stuff
        public void AddLine(RepairOrderLine line)
        {
            this.Lines.Add(line);
        }
        #endregion
    }
}
