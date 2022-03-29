namespace BLL.EventArgs
{
    public class ErrorArgs : System.EventArgs
    {
        public ErrorArgs(string info , byte errorCriticality, object whoInvolvedInError)
        {
            Info = info;
            ErrorCriticality = errorCriticality;
            WhoInvolvedInError = whoInvolvedInError;
        }

        public string Info { get; private set; }
        public byte ErrorCriticality { get; private set; }
        public object WhoInvolvedInError { get; private set; }
    }
}
