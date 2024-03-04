using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ArtworkSharing.Core.Exceptions
{
    public class BusinessException : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            switch (context.Exception)
            {
                case Exception ex: context.Result = new BadRequestObjectResult(new { Status = 400, Message = ex.Message }); break;
                    // Add whatever you need
            }
        }
    }
}
