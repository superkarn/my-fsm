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

        public void GoToNextRoMode()
        {
            switch(this._roModeStateMachine.StateMachine.State)
            {
                case RoModeStateMachine.State.Dispatch:
                    this._roModeStateMachine.StateMachine.Fire(RoModeStateMachine.Trigger.SetToInspection);
                    break;

                case RoModeStateMachine.State.Inspection:
                    this._roModeStateMachine.StateMachine.Fire(RoModeStateMachine.Trigger.SetToPendingApproval);
                    break;

                case RoModeStateMachine.State.PendingApproval:
                    this._roModeStateMachine.StateMachine.Fire(RoModeStateMachine.Trigger.SetToRepair);
                    break;

                case RoModeStateMachine.State.Repair:
                    this._roModeStateMachine.StateMachine.Fire(RoModeStateMachine.Trigger.SetToReview);
                    break;

                case RoModeStateMachine.State.Review:
                    this._roModeStateMachine.StateMachine.Fire(RoModeStateMachine.Trigger.SetToClosed);
                    break;

                case RoModeStateMachine.State.Closed:
                    this._roModeStateMachine.StateMachine.Fire(RoModeStateMachine.Trigger.SetToDispatch);
                    break;
            }
        }

        public void GoToNextPartStatus()
        {
            switch(this._partStatusStateMachine.StateMachine.State)
            {
                case PartStatusStateMachine.State.Default:
                    this._partStatusStateMachine.StateMachine.Fire(PartStatusStateMachine.Trigger.SetToWaiting);
                    break;

                case PartStatusStateMachine.State.Waiting:
                    this._partStatusStateMachine.StateMachine.Fire(PartStatusStateMachine.Trigger.SetToWorking);
                    break;

                case PartStatusStateMachine.State.Working:
                    this._partStatusStateMachine.StateMachine.Fire(PartStatusStateMachine.Trigger.SetToViewed);
                    break;

                case PartStatusStateMachine.State.Viewed:
                    this._partStatusStateMachine.StateMachine.Fire(PartStatusStateMachine.Trigger.SetToDefault);
                    break;
            }
        }

        public void Close()
        {
            this._roModeStateMachine.StateMachine.Fire(RoModeStateMachine.Trigger.SetToClosed);
        }

        public void AddLine(RepairOrderLine line)
        {
            this.Lines.Add(line);
        }
    }
}
