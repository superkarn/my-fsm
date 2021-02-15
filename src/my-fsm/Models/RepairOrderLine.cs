using my_fsm.StateMachines;

namespace my_fsm.Models
{
    public class RepairOrderLine
    {
        DmsSyncStateMachine _dmsSyncStateMachine { get; set; }

        public RepairOrderLine(
            DmsSyncStateMachine.State initialWritebackState = DmsSyncStateMachine.State.Unsynced)
        {
            this._dmsSyncStateMachine = new DmsSyncStateMachine(initialWritebackState);
        }

        public void Update()
        {
            // Update the line

            // Then update the DmsSync state
            this._dmsSyncStateMachine.StateMachine.Fire(DmsSyncStateMachine.Trigger.SetToUnsynced);
        }

        public void Writeback()
        {
            // Write the line back

            // Then update the DmsSync state
            this._dmsSyncStateMachine.StateMachine.Fire(DmsSyncStateMachine.Trigger.SetToSynced);
        }
    }
}
