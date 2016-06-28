// ************************
// Author: Sean Cheung
// Create: 2016/06/28/14:09
// Modified: 2016/06/28/18:05
// ************************

using System;

namespace Eyesar.Remoting
{
    public abstract class EyesarSharedObject : MarshalByRefObject
    {
        public DateTime TimeStamp { get; private set; }
        public abstract string Provider { get; }

        protected EyesarSharedObject(DateTime timeStamp)
        {
            TimeStamp = timeStamp;
        }
    }
}