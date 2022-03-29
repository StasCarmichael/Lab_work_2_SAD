using System;

namespace BLL.EventArgs
{
    public interface IEventLoggerable
    {
        event EventHandler<LoggerArgs> Logger;
        event EventHandler<ErrorArgs> Error;
        event EventHandler<ResultArgs> Result;
    }
}
