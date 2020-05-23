using System;
using System.IO;
using System.Threading.Tasks;
using Demo.Core.Services;
using Demo.Core.Services.Commands.UpdateHero;
using MediatR;
using MediatR.DDD.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Demo.Function.V1.Heroes
{
    public sealed class HeroesPut
    {
        private readonly IMediator _mediator;
        private readonly IValidatorService _validatorService;

        public HeroesPut(IMediator mediator, IValidatorService validatorService)
        {
            _mediator = mediator;
            _validatorService = validatorService;
        }

        [FunctionName("Heroes_Put")]
        public async Task<IActionResult> Put
        (
            [HttpTrigger(AuthorizationLevel.Function, RouteConfig.PUT, RouteConfig.PATCH, Route = RouteConfig.V1_URL_ID)]
            HttpRequest request,
            Guid id,
            ILogger logger
        )
        {
            try
            {
                string body = await new StreamReader(request.Body).ReadToEndAsync();
                UpdateHeroCommand hero = JsonConvert.DeserializeObject<UpdateHeroCommand>(body);

                UpdateHeroCommand command = new UpdateHeroCommand(id, hero.Name);
                await _validatorService.ValidateAsync(command);

                UpdateHeroCommandResponse response = await _mediator.Send(command);

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