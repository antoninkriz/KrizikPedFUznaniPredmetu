namespace KarolinkaUznani.Common.Responses
{
    /// <inheritdoc />
    /// <summary>
    /// Universal response that contains only success status and possible error message
    /// </summary>
    public class BasicResponse : IResponse
    {
        /// <summary>
        /// Did the login succeed
        /// </summary>
        public bool Success { get; set; }
        
        /// <summary>
        /// Error message if exists
        /// </summary>
        public string Message { get; set; }
    }
}