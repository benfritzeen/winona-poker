
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using api.Models;
using api.Services;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PokerController : ControllerBase
    {
        private readonly PokerHandService _pokerHandService;

        public PokerController(PokerHandService pokerHandService)
        {
            _pokerHandService = pokerHandService;
        }

        [HttpPost]
        public IActionResult DealHands([FromBody] DealRequest request)
        {
            foreach (var playerName in request.PlayerNames)
            {
                _pokerHandService.DealHand(playerName);
            }
            var evaluatedPlayers = _pokerHandService.EvaluateHands();
            return Ok(evaluatedPlayers);
        }
    }
}
