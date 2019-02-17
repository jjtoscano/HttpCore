namespace HttpCore.Entities
{
    /// <summary>
    /// Supported HTTP methods for the HTTP service.
    /// </summary>
    public enum CustomHttpMethod
    {
        /// <summary>
        /// Requests to retrieve resource representation/information only.
        /// </summary>
        Get,

        /// <summary>
        /// Replaces all current representations of the target resource with the request payload.
        /// </summary>
        Put,

        /// <summary>
        /// Used to submit an entity to the specified resource.
        /// </summary>
        Post,

        /// <summary>
        /// Used to apply partial modifications to a resource.
        /// </summary>
        Patch,

        /// <summary>
        /// Deletes the specified resource.
        /// </summary>
        Delete
    }
}
