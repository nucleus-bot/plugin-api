using Nucleus.Plugins.Contextualization;

namespace Nucleus.Plugins {
    /// <summary>
    /// <para>Calling the <paramref name="nextMiddleware"/> more than once can cause some weird issues</para>
    /// <list type="bullet">
    ///     <item>
    ///         <description>If all middleware has been called, an Empty task will be returned</description>
    ///     </item>
    ///     <item>
    ///         <description>If the next middleware short circuits, it will call the next Middleware from the queue</description>
    ///     </item>
    /// </list>
    /// <para>No need to pass the <see cref="IMessageContext"/> as it will be done automatically</para>
    /// </summary>
    /// <param name="context">The context of the message</param>
    /// <param name="nextMiddleware">The next Middleware to be called. Middleware can be short-circuited by not calling this</param>
    /// <returns>The Task for the next Middleware in the queue, the very final Middleware populates the Text window</returns>
    public delegate ValueTask MessageReceivedDelegate(IMessageContext context, Func<ValueTask> nextMiddleware);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public delegate void ChannelStateDelegate(IChannelContext context);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="content"></param>
    /// <typeparam name="TR"></typeparam>
    /// <typeparam name="TI"></typeparam>
    public delegate IEnumerable<TR> EnumerableDelegate<TR, TI>(TI content);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    public delegate void WebServicesDelegate(IServiceCollection services);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="app"></param>
    /// <param name="environment"></param>
    public delegate void WebServerDelegate(IApplicationBuilder app, IWebHostEnvironment environment);
}
