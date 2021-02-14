using my_fsm.StateMachines;

namespace my_fsm.Models
{
    public class RepairOrderLine
    {
        WritebackStateMachine _writebackStateMachine { get; set; }

        public RepairOrderLine(
            WritebackStateMachine.State initialWritebackState = WritebackStateMachine.State.Unsynced)
        {
            this._writebackStateMachine = new WritebackStateMachine(initialWritebackState);
        }

        public void Update()
        {
            // Update the line

            // Then update the writeback state
            this._writebackStateMachine.StateMachine.Fire(WritebackStateMachine.Trigger.SendToUnsynced);
        }

        public void Writeback()
        {
            // Write the line back

            // Then update the writeback state
            this._writebackStateMachine.StateMachine.Fire(WritebackStateMachine.Trigger.SendToSynced);
        }
    }
}
