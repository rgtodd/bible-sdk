﻿using BibleCore.Service;
using BibleCore.Service.Data;

using Microsoft.AspNetCore.Mvc;

namespace BibleWebApi.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class LexemeApiController(ILexemeService lexemeService) : ControllerBase
    {
        [HttpGet("strongs/{id}")]
        public LexemeData? GetByStrongsNumber(int id)
        {
            return lexemeService.GetByStrongsNumber(id);
        }

        [HttpGet("gk/{id}")]
        public LexemeData? GetByGkNumber(int id)
        {
            return lexemeService.GetByGkNumber(id);
        }
    }
}