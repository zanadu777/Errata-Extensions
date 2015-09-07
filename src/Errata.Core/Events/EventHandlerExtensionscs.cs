using System;

namespace Errata.Events
{
   public static  class EventHandlerExtensionscs
    {
        public static void Raise<T>(this EventHandler<T> handler, object sender, T e) where T : EventArgs
        {
            if (handler != null)
            {
                handler(sender, e);
            }
        }

       public static void Raise(this EventHandler handler, object sender)
       {
           if (handler != null)
           {
               handler(sender, EventArgs.Empty);
           }
       }
    }
}
