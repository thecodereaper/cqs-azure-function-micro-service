using System;
using System.Threading.Tasks;
using Demo.Core.Services;
using Demo.Core.Services.Queries.GetOneHero;
using MediatR;
using MediatR.DDD.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace Demo.Function.V1.Heroes
{
    public sealed class HeroesGetOne
    {
        private readonly IMediator _mediator;
        private readonly IValidatorService _validatorService;

        public HeroesGetOne(IMediator mediator, IValidatorService validatorService)
        {
            _mediator = mediator;
            _validatorService = validatorService;
        }

        [FunctionName("HeroesGetOne")]
        public async Task<IActionResult> GetOne
        (
            [HttpTrigger(AuthorizationLevel.Function, RouteConfig.GET, Route = RouteConfig.V1_URL_ID)]
            HttpRequest request,
            Guid id,
            ILogger logger
        )
        {
            try
            {
                GetOneHeroQuery query = new GetOneHeroQuery(id);
                await _validatorService.ValidateAsync(query);

                GetOneHeroQueryResponse response = await _mediator.Send(query);

                return new OkObjectResult(response.Hero);
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