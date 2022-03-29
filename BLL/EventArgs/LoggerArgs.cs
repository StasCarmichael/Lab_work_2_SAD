namespace BLL.EventArgs
{
    public enum ActionResult { Message, Error, Result }

    public class LoggerArgs : System.EventArgs
    {
        public LoggerArgs(string info , ActionResult action)
        {
            Info = info;
            Action = action;
        }


        public string Info { get; private set; }
        public ActionResult Action { get; private set; }
    }
}
