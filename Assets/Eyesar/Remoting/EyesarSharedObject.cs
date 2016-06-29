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
        public abstract string Provider { get; }
        public abstract DateTime TimeStamp { get; }
    }
}