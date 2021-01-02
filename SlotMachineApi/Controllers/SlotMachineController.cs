using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SlotMachineApi.Models;
using SlotMachineApi.Services;

namespace SlotMachineApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlotMachineController : ControllerBase
    {
        private readonly SlotMachineService _slotMachineService;
        private readonly UserService _userService;
        private readonly ConfigurationService _configService;

        public SlotMachineController(SlotMachineService slotMachineService, UserService userService, ConfigurationService configService)
        {
            _slotMachineService = slotMachineService;
            _configService = configService;
            _userService = userService;
        }

        [HttpGet("{name}", Name = "GetUser")]
        public ActionResult<SlotMachine> Get(string name)
        {
            var slotUser = _slotMachineService.Get(name);

            if (slotUser == null)
            {
                return NotFound();
            }

            return slotUser;
        }

        [HttpPost]
        public ActionResult<SlotResponse> CalculateWin([FromForm] string name, [FromForm] decimal amount)
        {
            var slotUser = _slotMachineService.Get(name);
            if (slotUser == null)
            {
                return NotFound();
            }
            if(slotUser.Balance < amount)
            {
                return BadRequest("Not enough balance");
            }

            var configData = _configService.Get("5fede93d67fed1af36bfc647");

            if(configData == null)
            {
                return NotFound();
            }

            int[] bet = new int[configData.ArraySize];

            Random randNum = new Random();

            for (int i = 0; i < bet.Length; i++)
            {
                bet[i] = randNum.Next(0, 10);
            }

            int winAmount = bet[0];
            int prev = bet[0];

            for (int i = 1; i < bet.Length; i++)
            {
                if(bet[i] == prev)
                {
                    winAmount += bet[0];
                }
                else
                {
                    break;
                }
            }

            decimal finalWin = winAmount * amount;


            var finalResponse = new SlotResponse();

            finalResponse.Balance = slotUser.Balance - amount + finalWin;
            finalResponse.spinArray = bet;
            finalResponse.UserName = slotUser.UserName;
            finalResponse.WinAmount = finalWin;

            slotUser.Balance = slotUser.Balance - amount + finalWin;
            _slotMachineService.Update(slotUser.UserName, slotUser);

            return finalResponse;
        }
    }
}