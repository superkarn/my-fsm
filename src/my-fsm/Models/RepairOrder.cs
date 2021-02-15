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
                    this._roModeStateMachine.StateMachine.Fire(RoModeStateMachine.Trigger.SendToInspection);
                    break;

                case RoModeStateMachine.State.Inspection:
                    this._roModeStateMachine.StateMachine.Fire(RoModeStateMachine.Trigger.SendToPendingApproval);
                    break;

                case RoModeStateMachine.State.PendingApproval:
                    this._roModeStateMachine.StateMachine.Fire(RoModeStateMachine.Trigger.SendToRepair);
                    break;

                case RoModeStateMachine.State.Repair:
                    this._roModeStateMachine.StateMachine.Fire(RoModeStateMachine.Trigger.SendToReview);
                    break;

                case RoModeStateMachine.State.Review:
                    this._roModeStateMachine.StateMachine.Fire(RoModeStateMachine.Trigger.SendToClosed);
                    break;

                case RoModeStateMachine.State.Closed:
                    this._roModeStateMachine.StateMachine.Fire(RoModeStateMachine.Trigger.SendToDispatch);
                    break;
            }
        }

        public void GoToNextPartStatus()
        {
            switch(this._partStatusStateMachine.StateMachine.State)
            {
                case PartStatusStateMachine.State.Default:
                    this._partStatusStateMachine.StateMachine.Fire(PartStatusStateMachine.Trigger.SendToWaiting);
                    break;

                case PartStatusStateMachine.State.Waiting:
                    this._partStatusStateMachine.StateMachine.Fire(PartStatusStateMachine.Trigger.SendToWorking);
                    break;

                case PartStatusStateMachine.State.Working:
                    this._partStatusStateMachine.StateMachine.Fire(PartStatusStateMachine.Trigger.SendToViewed);
                    break;

                case PartStatusStateMachine.State.Viewed:
                    this._partStatusStateMachine.StateMachine.Fire(PartStatusStateMachine.Trigger.SendToDefault);
                    break;
            }
        }

        public void Close()
        {
            this._roModeStateMachine.StateMachine.Fire(RoModeStateMachine.Trigger.SendToClosed);
        }

        public void AddLine(RepairOrderLine line)
        {
            this.Lines.Add(line);
        }
    }
}
