using System;


namespace Foundation.BlazorExtensions.CustomBlazorRouter
{
    /// <summary>
    /// An exception thrown when <see cref="NavigationManager.LocationChanged"/> throws an exception.
    /// </summary>
    public sealed class LocationChangeException : Exception
    {
        /// <summary>
        /// Creates a new instance of <see cref="LocationChangeException"/>.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="innerException">The inner exception.</param>
        public LocationChangeException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}