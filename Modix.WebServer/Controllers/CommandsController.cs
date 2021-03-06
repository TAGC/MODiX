﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Modix.Services.CommandHelp;

namespace Modix.WebServer.Controllers
{
    [Route("~/api")]
    public class CommandsController : Controller
    {
        private CommandHelpService _commandHelpService;

        public CommandsController(CommandHelpService commandHelpService)
        {
            _commandHelpService = commandHelpService;
        }

        [HttpGet("commands")]
        public IActionResult Commands()
        {
            return Ok(_commandHelpService.GetData());
        }
    }
}
