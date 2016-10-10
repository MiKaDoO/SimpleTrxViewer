using Nancy;
using Nancy.ErrorHandling;
using Nancy.ViewEngines;

namespace TrxViewer
{
    public class PageNotFoundHandler : DefaultViewRenderer, IStatusCodeHandler
    {
        public PageNotFoundHandler(IViewFactory factory) : base(factory)
        {
        }

        public bool HandlesStatusCode(HttpStatusCode statusCode, NancyContext context)
        {
            return statusCode == HttpStatusCode.NotFound;
        }

        public void Handle(HttpStatusCode statusCode, NancyContext context)
        {
            var response = RenderView(context, "Errors/NotFound");
            response.StatusCode = HttpStatusCode.NotFound;
            context.Response = response;
        }
    }

    public class ErrorHandler : DefaultViewRenderer, IStatusCodeHandler
    {
        public ErrorHandler(IViewFactory factory) : base(factory)
        {
        }

        public bool HandlesStatusCode(HttpStatusCode statusCode, NancyContext context)
        {
            return statusCode == HttpStatusCode.InternalServerError;
        }

        public void Handle(HttpStatusCode statusCode, NancyContext context)
        {
            var response = RenderView(context, "Errors/InternalError");
            response.StatusCode = HttpStatusCode.InternalServerError;
            context.Response = response;
        }
    }
}
