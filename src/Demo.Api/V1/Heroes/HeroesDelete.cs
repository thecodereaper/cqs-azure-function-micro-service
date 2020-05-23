using System;
using System.Threading.Tasks;
using Demo.Core.Services;
using Demo.Core.Services.Commands.DeleteHero;
using MediatR;
using MediatR.DDD.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace Demo.Api.V1.Heroes
{
    public sealed class HeroesDelete
    {
        private readonly IMediator _mediator;
        private readonly IValidatorService _validatorService;

        public HeroesDelete(IMediator mediator, IValidatorService validatorService)
        {
            _mediator = mediator;
            _validatorService = validatorService;
        }

        [FunctionName("HeroesDelete")]
        public async Task<IActionResult> Delete
        (
            [HttpTrigger(AuthorizationLevel.Function, RouteConfig.DELETE, Route = RouteConfig.V1_URL_ID)]
            HttpRequest request,
            Guid id,
            ILogger logger
        )
        {
            try
            {
                DeleteHeroCommand command = new DeleteHeroCommand(id);
                await _validatorService.ValidateAsync(command);

                DeleteHeroCommandResponse response = await _mediator.Send(command);

                return new OkObjectResult(response);
            }
            catch (BadRequestException ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
            catch (NotFoundException)
            {
                return new NotFoundObjectResult(null);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }
    }
}