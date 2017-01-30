namespace Caliburn.Micro
{
    /// <summary>
    /// Exposes an event aggregator singleton.
    /// </summary>
    /// <remarks>Added by Diederik Krols.</remarks>
    partial class EventAggregator
    {
        private static IEventAggregator instance;

        /// <summary>
        /// Gets the singleton instance.
        /// </summary>
        public static IEventAggregator Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EventAggregator();
                }
                return instance;
            }
        }
    }
}
