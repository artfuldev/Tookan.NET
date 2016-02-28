namespace Tookan.NET.Http
{
    /// <summary>
    ///     The status as returned by the Tookan API.
    /// </summary>
    public enum Status
    {
        None = 0,
        ParameterMissing = 100,
        ActionComplete = 200,
        ShowErrorMessage = 201,
        InvalidAccessToken = 101,
        ErrorInExecution = 404
    }
}