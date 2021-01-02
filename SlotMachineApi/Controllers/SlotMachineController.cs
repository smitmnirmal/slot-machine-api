using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SlotMachineApi.Models;
using SlotMachineApi.Services;

namespace SlotMachineApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SlotMachineController : ControllerBase
    {
        private readonly SlotMachineService _slotMachineService;
        private readonly ConfigurationService _configService;

        public SlotMachineController(SlotMachineService slotMachineService, ConfigurationService configService)
        {
            _slotMachineService = slotMachineService;
            _configService = configService;
        }

        [HttpGet]
        public ActionResult<SlotMachine> Get()
        {
            /*var slotUser = _slotMachineService.Get(name);*/
            var slotUser = _slotMachineService.Get(GetUserId());

            if (slotUser == null)
            {
                return NotFound("User Not found");
            }

            return slotUser;
        }

        [HttpPost]
        public ActionResult<SlotResponse> CalculateWin([FromForm] decimal amount)
        {
            /*var slotUser = _slotMachineService.Get(name);*/
            var slotUser = _slotMachineService.Get(GetUserId());
            if (slotUser == null)
            {
                return NotFound("User not found");
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
            finalResponse.UserId = slotUser.UserId;
            finalResponse.WinAmount = finalWin;

            slotUser.Balance = slotUser.Balance - amount + finalWin;
            _slotMachineService.Update(slotUser.UserId, slotUser);

            return finalResponse;
        }

        protected string GetUserId()
        {
            return this.User.Claims.First(i => i.Type == "Id").Value;
        }

        protected string GetUserName()
        {
            return this.User.Claims.First(i => i.Type == "Name").Value;
        }
    }
}