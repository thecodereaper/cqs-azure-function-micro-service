using System;
using System.IO;
using System.Threading.Tasks;
using Demo.Core.Services;
using Demo.Core.Services.Commands.CreateHero;
using MediatR;
using MediatR.DDD.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Demo.Api.V1.Heroes
{
    public sealed class HeroesPost
    {
        private readonly IMediator _mediator;
        private readonly IValidatorService _validatorService;

        public HeroesPost(IMediator mediator, IValidatorService validatorService)
        {
            _mediator = mediator;
            _validatorService = validatorService;
        }

        [FunctionName("HeroesPost")]
        public async Task<IActionResult> Post
        (
            [HttpTrigger(AuthorizationLevel.Function, RouteConfig.POST, Route = RouteConfig.V1_URL)]
            HttpRequest request,
            ILogger logger
        )
        {
            try
            {
                string body = await new StreamReader(request.Body).ReadToEndAsync();
                CreateHeroCommand command = JsonConvert.DeserializeObject<CreateHeroCommand>(body);

                await _validatorService.ValidateAsync(command);
                CreateHeroCommandResponse response = await _mediator.Send(command);

                return new CreatedResult($"{RouteConfig.V1_URL}/{response.Hero.Id}", response.Hero);
            }
            catch (BadRequestException ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
            catch (ConflictException)
            {
                return new ConflictObjectResult(null);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }
    }
}