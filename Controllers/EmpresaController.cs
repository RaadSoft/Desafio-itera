using Desafio_itera.Models;
using Desafio_ITERA.Json;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;



// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Desafio_itera.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {

        // GET <EmpresaController>/5
        [HttpGet("{_id}")]
        public IActionResult Get(string _id)
        {
            try
            {
                List<Empresa> empresas = DbJson.Empresas();
                Empresa empresa = empresas.Find(e => e.id == _id);
                if (empresa != null)
                    return Ok(empresa);
                else
                    return NotFound("Empresa não encontrada");
            }
            catch (Exception erro)
            {
                Log.LogArquivo.SetLog("EmpresaController - GET{_id}", erro.Message);
                return StatusCode(500);
            }
        }

        // POST <EmpresaController>
        [HttpPost]
        public IActionResult Post([FromBody] Empresa empresa)
        {
            try
            {
                List<Empresa> empresas = DbJson.Empresas();
                Empresa empresaCadastrada = empresas.Find(e => e.id == empresa.id);

                if (empresaCadastrada == null )
                {
                    if(empresa.status != "ATIVO" && empresa.status != "INATIVO")
                    {
                        return BadRequest("Status Inválido");
                    }
                    
                    empresa.date_ingestion = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                    empresa.last_update = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                    empresas.Add(empresa);

                    DbJson.UpdateEmpresas(empresas);
                    return Ok("Empresa Cadastrada com Sucesso");
                }
                else
                {
                    return BadRequest("Empresa já Cadastrada");
                }
            }
            catch (Exception erro)
            {
                Log.LogArquivo.SetLog("EmpresaController - POST", erro.Message);
                return StatusCode(500);
            }
        }

        // PUT /<EmpresaController>/custos/{id_empresa}
        [HttpPut("custos/{_id}")]
        public IActionResult Put(string _id, [FromBody] Custo custo)
        {
            try
            {
                List<Empresa> empresas = DbJson.Empresas();
                Empresa empresaCadastrada = empresas.Find(e => e.id == _id);

                if (empresaCadastrada != null)
                {
                    Custo custoCadastrado = empresaCadastrada.custos.Find(c => c.id_type == custo.id_type && c.ano == custo.ano);

                    if(custoCadastrado != null)
                    {
                        custoCadastrado.last_update = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                        custoCadastrado.valor = custo.valor;
                    }
                    else
                    {
                        custo.last_update = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                        empresaCadastrada.custos.Add(custo);
                    }

                    empresaCadastrada.last_update = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

                    DbJson.UpdateEmpresas(empresas);
                    return Ok("Custo Salvo com Sucesso");
                }
                else
                {
                    return BadRequest("Empresa não Cadastrada");
                }
            }
            catch (Exception erro)
            {
                Log.LogArquivo.SetLog("EmpresaController - PUT custos/{id_empresa}", erro.Message);
                return StatusCode(500);
            }
        }

        // DELETE /<EmpresaController>/5
        [HttpDelete("{_id}")]
        public IActionResult Delete(string _id)
        {
            try
            {
                List<Empresa> empresas = DbJson.Empresas();
                Empresa empresaCadastrada = empresas.Find(e => e.id == _id);

                if (empresaCadastrada != null)
                {
                    empresaCadastrada.status = "INATIVO";
                    DbJson.UpdateEmpresas(empresas);
                    return Ok("Empresa Inativada com sucesso!");
                }
                else
                {
                    return BadRequest("Empresa não Cadastrada");
                }
            }
            catch (Exception erro)
            {
                Log.LogArquivo.SetLog("EmpresaController - DELETE {_id}", erro.Message);
                return StatusCode(500);
            }
        }
    }
}
