namespace TripLog.Server.Test
{
    using System.IO;

    public class ExtendedDbreezeTripLogPersistency : DbreezeTripLogPersistency
    {
        public ExtendedDbreezeTripLogPersistency(DirectoryInfo directory) : base(directory)
        {
        }

        public void ShutDown()
        {
            if (_dbDirectory != null && _dbDirectory.Exists)
            {
                _dbDirectory.Delete(true);
            }
        }
    }
}
