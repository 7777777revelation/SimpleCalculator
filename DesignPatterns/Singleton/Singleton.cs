using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.Singleton
{
    public sealed class Singleton  //'sealed' ==> other classes cannot inherit from it
    {
        private static Int64 _nextId = 0; // max value of Int64 is 9,223,372,036,854,775,807 so that will take a while to reach :-)
        private static Singleton instance = null;
        private static readonly object padlock = new object();
        public static Int64 GetNextId()
        {
            return ++_nextId;
        }

        Singleton()
        {
        }

        public static Singleton Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new Singleton();
                    }
                    return instance;
                }
            }
        }
    }
}
