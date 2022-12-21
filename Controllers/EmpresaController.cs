using Desafio_itera.Models;
using Desatio_itera.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Desatio_itera.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {

        // GET <EmpresaController>/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            try
            {
                string jsonEmpresas = System.IO.File.ReadAllText("./json/companys.json");
                List<Empresa> empresas = JsonSerializer.Deserialize<List<Empresa>>(jsonEmpresas);
                Empresa empresa = empresas.Find(e => e.id == id);
                if (empresa != null)
                    return Ok(empresa);
                else
                    return NotFound("Empresa não encontrada");
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }

        // POST <EmpresaController>
        [HttpPost]
        public IActionResult Post([FromBody] Empresa empresa)
        {
            try
            {
                string jsonEmpresas = System.IO.File.ReadAllText("./json/companys.json");
                List<Empresa> empresas = JsonSerializer.Deserialize<List<Empresa>>(jsonEmpresas);
                Empresa empresaCadastrada = empresas.Find(e => e.id == empresa.id);

                if (empresaCadastrada == null )
                {
                    if(empresa.status != "ATIVO" && empresa.status != "INATIVO")
                    {
                        return BadRequest("Status Inválido");
                    }
                    empresas.Add(empresa);

                    System.IO.File.WriteAllText("./json/companys.json", JsonSerializer.Serialize(empresas));
                    return Ok("Empresa Salva com Sucesso");
                }
                else
                {
                    return BadRequest("Empresa já Cadastrada");
                }
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }

        // PUT /<EmpresaController>/custos/{id_empresa}
        [HttpPut("custos/{id}")]
        public IActionResult Put(string id, [FromBody] Custo custo)
        {
            try
            {
                string jsonEmpresas = System.IO.File.ReadAllText("./json/companys.json");
                List<Empresa> empresas = JsonSerializer.Deserialize<List<Empresa>>(jsonEmpresas);
                Empresa empresaCadastrada = empresas.Find(e => e.id == id);

                if (empresaCadastrada != null)
                {
                    Custo custoCadastrado = empresaCadastrada.custos.Find(c => c.id_type == custo.id_type && c.ano == custo.ano);
                    if(custoCadastrado != null)
                    {
                        custoCadastrado.valor = custo.valor;
                    }
                    else
                    {
                        empresaCadastrada.custos.Add(custo);
                    }
                    
                    System.IO.File.WriteAllText("./json/companys.json", JsonSerializer.Serialize(empresas));
                    return Ok("Custo Salvo com Sucesso");
                }
                else
                {
                    return BadRequest("Empresa não Cadastrada");
                }
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }

        // DELETE /<EmpresaController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                string jsonEmpresas = System.IO.File.ReadAllText("./json/companys.json");
                List<Empresa> empresas = JsonSerializer.Deserialize<List<Empresa>>(jsonEmpresas);
                Empresa empresaCadastrada = empresas.Find(e => e.id == id);

                if (empresaCadastrada != null)
                {
                    empresaCadastrada.status = "INATIVO";

                    System.IO.File.WriteAllText("./json/companys.json", JsonSerializer.Serialize(empresas));
                    return Ok("Empresa Inativada com sucesso!");
                }
                else
                {
                    return BadRequest("Empresa não Cadastrada");
                }
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
