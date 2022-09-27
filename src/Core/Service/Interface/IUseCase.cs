namespace Service.Interface
{
    /// <summary>
    /// Interface implememted by use cases
    /// </summary>
    /// <typeparam name="TResult">Returning type</typeparam>
    /// <typeparam name="TCommand">Command type</typeparam>
    public interface IUseCase<TResult, TCommand>
        where TResult : class
        where TCommand : class
    { 
        /// <summary>
        /// Interface implememted by use cases
        /// </summary>
        /// <param name="entry">Command entry</param>
        /// <returns>Use case result</returns>
        Task<TResult> Execute(TCommand entry); 
    }

    /// <summary>
    /// Interface implememted by use cases
    /// </summary>
    /// <typeparam name="TResult">Returning type</typeparam>
    
    public interface IUseCase<TResult>
        where TResult : class
    {
        /// <summary>
        /// Main execution of use case
        /// </summary>
        /// <returns>Use case result</returns>
        Task<TResult> Execute(); 
    }
}