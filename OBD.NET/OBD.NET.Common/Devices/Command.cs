namespace OBD.NET.Common.Devices
{
    /// <summary>
    /// Class used for queued command
    /// </summary>
    public class QueuedCommand
    {
      

        public string CommandText { get; private set; }

        public CommandResult CommandResult { get; }

   

        #region Constructors

        public QueuedCommand(string commandText)
        {
            this.CommandText = commandText;

            CommandResult = new CommandResult();
        }

        #endregion
    }
}
