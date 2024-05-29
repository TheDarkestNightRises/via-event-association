// using Microsoft.AspNetCore.Components;
// using Microsoft.AspNetCore.Mvc;
// using ViaEventAssociation.Core.Application.CommandDispatching.Commands.Event;
// using ViaEventAssociation.Core.Application.CommandDispatching.Dispatcher;
// using ViaEventAssociation.Core.Tools.ObjectMapper;
// using ViaEventAssociation.Core.Tools.OperationResult;
// using ViaEventAssociation.Presentation.WebAPI.Common;
// using System;
// using System.Threading.Tasks;
//
// namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.Event.UpdateEventTitleEndpoint;
//
// public class UpdateEventTitleEndpoint(IMapper mapper, ICommandDispatcher dispatcher) : ApiEndpoint
//     .WithRequest<UpdateEventTitleRequest>
//     .WithoutResponse
// {
//     [HttpPost("events/{Id}/update-title")]
//     public override async Task<ActionResult> HandleAsync([FromRoute] UpdateEventTitleRequest request)
//     {
//
//         var cmdResult = UpdateEventTitleCommand.Create(request.Id, request.RequestBody.Title);
//         if (cmdResult.IsFailure)
//         {
//             return BadRequest(cmdResult.Errors);
//         }
//
//         var result = await dispatcher.DispatchAsync(cmdResult.PayLoad);
//         return result.IsSuccess ? Ok() : BadRequest(result.Errors);
//     }
// }
//
// public class UpdateEventTitleRequest
// {
//     [FromRoute] public string Id { get; set; }
//
//     [FromBody] public Body RequestBody { get; set; }
//
//     public record Body(string Title);
// }