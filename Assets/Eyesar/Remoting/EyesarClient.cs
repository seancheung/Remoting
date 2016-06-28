// ************************
// Author: Sean Cheung
// Create: 2016/06/28/14:08
// Modified: 2016/06/28/17:42
// ************************

using System;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;

namespace Eyesar.Remoting
{
    public abstract class EyesarClient : IDisposable
    {
        private readonly IpcClientChannel _channel;
        public EyesarSharedObject SharedObject { get; private set; }

        protected EyesarClient()
        {
            _channel = new IpcClientChannel();
            ChannelServices.RegisterChannel(_channel, false);
            SharedObject = (EyesarSharedObject) Activator.GetObject(typeof (EyesarSharedObject), "ipc://eyesar/shared");
        }

        ~EyesarClient()
        {
            try
            {
                ChannelServices.UnregisterChannel(_channel);
            }
            catch
            {
                // ignored
            }
        }

        #region Implementation of IDisposable

        public void Dispose()
        {
            try
            {
                ChannelServices.UnregisterChannel(_channel);
            }
            catch
            {
                // ignored
            }
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}