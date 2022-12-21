using Desafio_ITERA.Json;
using Desafio_itera.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using Desafio_itera.Services;

namespace Desafio_itera.Controllers
{
    [Route("/login")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // POST <UserController>
        [HttpPost]
        public IActionResult Post([FromBody] UserLogin user)
        {
            try
            {
                List<User> usuarios = DbJson.Usuarios();
                User usuario = usuarios.Find(u => u.user == user.user);
                if(usuario == null || usuario.password != user.password)
                {
                    return BadRequest("Usuário ou senha inválidos");
                }
                string token = TokenService.GerarToken(usuario);
                return Ok( new {token = token});
            }
            catch (Exception erro)
            {
                Log.LogArquivo.SetLog("UserController - POST Login", erro.Message);
                return StatusCode(500);
            }
        }
    }
}
