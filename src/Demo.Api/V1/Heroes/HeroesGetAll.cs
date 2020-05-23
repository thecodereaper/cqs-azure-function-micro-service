using System;
using System.Threading.Tasks;
using Demo.Core.Services;
using Demo.Core.Services.Queries.GetAllHeroes;
using MediatR;
using MediatR.DDD.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace Demo.Api.V1.Heroes
{
    public sealed class HeroesGetAll
    {
        private readonly IMediator _mediator;
        private readonly IValidatorService _validatorService;

        public HeroesGetAll(IMediator mediator, IValidatorService validatorService)
        {
            _mediator = mediator;
            _validatorService = validatorService;
        }

        [FunctionName("HeroesGetAll")]
        public async Task<IActionResult> GetAll
        (
            [HttpTrigger(AuthorizationLevel.Function, RouteConfig.GET, Route = RouteConfig.V1_URL)]
            HttpRequest request,
            ILogger logger
        )
        {
            try
            {
                int.TryParse(request.Query["offset"], out int offset);
                int.TryParse(request.Query["limit"], out int limit);

                string sortBy = request.Query["sortBy"];
                string sortOrder = request.Query["sortOrder"];
                string filter = request.Query["filter"];

                GetAllHeroesQuery query = new GetAllHeroesQuery(offset, limit, sortBy, sortOrder, filter);
                await _validatorService.ValidateAsync(query);

                GetAllHeroesQueryResponse response = await _mediator.Send(query);

                return new OkObjectResult(response);
            }
            catch (BadRequestException ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }
    }
}