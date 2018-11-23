namespace TripLog.ViewModels
{
    using System;

    public class EntryNotProvidedException : Exception
    {
        public EntryNotProvidedException() : base("Entry not provided to the VM!")
        { }
    }
}
