// ************************
// Author: Sean Cheung
// Create: 2016/06/28/14:08
// Modified: 2016/06/28/17:41
// ************************

using System;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;

namespace Eyesar.Remoting
{
    public abstract class EyesarServer : IDisposable
    {
        private readonly IpcServerChannel _channel;
        public EyesarSharedObject SharedObject { get; private set; }

        protected EyesarServer(EyesarSharedObject sharedObject)
        {
            if (ChannelServices.RegisteredChannels.Any(channel => channel.ChannelName == "Eyesar"))
                throw new InvalidOperationException();
            _channel = new IpcServerChannel("Eyesar", "eyesar");
            ChannelServices.RegisterChannel(_channel, false);
            RemotingServices.Marshal(SharedObject = sharedObject, "shared", typeof (EyesarSharedObject));
        }

        ~EyesarServer()
        {
            try
            {
                _channel.StopListening(null);
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
            _channel.StopListening(null);
            ChannelServices.UnregisterChannel(_channel);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}