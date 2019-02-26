namespace KarolinkaUznani.Common.Requests.Data
{
    /// <summary>
    /// Object containing data related loading specified Katedra
    /// </summary>
    public class KatedraRequest : IRequest
    {
        /// <summary>
        /// Text that is the user searching for
        /// </summary>
        public string SearchText { get; set; }
    }
}