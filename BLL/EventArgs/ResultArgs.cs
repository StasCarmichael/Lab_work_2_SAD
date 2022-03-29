namespace BLL.EventArgs
{
    public class ResultArgs : System.EventArgs
    {
        public ResultArgs(string info)
        {
            Info = info;
        }

        public string Info { get; private set; }
    }
}
