using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Desafio_itera.Models;
using System.Text.Json;
using System;
using System.Linq;
using Desafio_ITERA.Json;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Desafio_itera.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GrupoController : ControllerBase
    {
        // GET <GrupoController>/_id
        [HttpGet("{_id}")]
        [Authorize]
        public IActionResult Get(string _id)
        {
            try
            {
                List<Grupo> grupos = DbJson.Grupos();
                Grupo grupo = grupos.Find(e => e.id == _id);
                if (grupo != null)
                    return Ok(grupo);
                else
                    return NotFound("Grupo não encontrado");
            }
            catch (Exception erro)
            {
                Log.LogArquivo.SetLog("GrupoController - GET {_id}", erro.Message);
                return StatusCode(500);
            }
        }
        // GET <GrupoController>?date='YYYY-MM-DD'
        [HttpGet()]
        [Authorize]
        public IActionResult Get(DateTime date)
        {
            try
            {
                List<Grupo> gruposCadastrados = DbJson.Grupos();
                List<Grupo> grupos = gruposCadastrados.FindAll(e => DateTime.Parse(e.date_ingestion) <= date);

                if (grupos.Count > 0)
                {
                    List<Empresa> empresasCadastradas = DbJson.Empresas();
                    List<Empresa> empresas = new List<Empresa>();
                    foreach (Grupo grupo in grupos)
                    {
                        List<Empresa> empresasEncontradas = empresasCadastradas.FindAll(e => grupo.companys.Contains(e.id));
                        empresas = empresas.Concat(empresasEncontradas.Where(e2 => !empresas.Contains(e2))).ToList();
                    }
                    if (empresas.Count > 0)
                        return Ok(empresas);
                    else
                        return NotFound("Não foi encontrado Empresas cadastradas que satisfação a consulta.");
                }
                else
                    return NotFound("Não existe Grupos cadastrados antes da data informada.");
            }
            catch (Exception erro)
            {
                Log.LogArquivo.SetLog("GrupoController - GET {?date='YYYY-MM-DD'}", erro.Message);
                return StatusCode(500);
            }
        }

        // GET <GrupoController>/custos/{_id}
        [HttpGet("custos/{_id}")]
        [Authorize]
        public IActionResult GetCustos(string _id)
        {
            try
            {
                List<Grupo> grupos = DbJson.Grupos();
                Grupo grupo = grupos.Find(g => g.id == _id);
                if (grupo != null)
                {
                    List<Empresa> empresas = DbJson.Empresas().FindAll(e => grupo.companys.Contains(e.id));
                    List<CustoReport> report = new List<CustoReport>();
                    foreach (Empresa empresa in empresas)
                    {
                        foreach (Custo custo in empresa.custos)
                        {
                            CustoReport item = report.Find(r => r.id_type == custo.id_type && r.ano == custo.ano);
                            if (item != null)
                            {
                                item.valor += custo.valor;
                            }
                            else
                            {
                                item = new CustoReport()
                                {
                                    id_type = custo.id_type,
                                    ano = custo.ano,
                                    valor = custo.valor,
                                };
                                report.Add(item);
                            }
                        }
                    }
                    return Ok(report);
                }
                else
                {
                    return NotFound("Grupo não encontrado");
                }
            }
            catch (Exception erro)
            {
                Log.LogArquivo.SetLog("GrupoController - GET custos/{_id}", erro.Message);
                return StatusCode(500);
            }
        }

        // POST <GrupoController>
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] Grupo grupo)
        {
            try
            {
                List<Grupo> gruposCadastrados = DbJson.Grupos();
                Grupo grupoCadastrado = gruposCadastrados.Find(g => g.id == grupo.id);
                if (grupoCadastrado == null)
                {
                    grupo.date_ingestion = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                    gruposCadastrados.Add(grupo);
                    DbJson.UpdateGrupos(gruposCadastrados);
                    return Ok("Grupo Cadastrado com sucesso!");
                }
                else
                {
                    return BadRequest("Grupo Já cadastrado");
                }

            }
            catch (Exception erro)
            {
                Log.LogArquivo.SetLog("GrupoController - POST FromBody {Grupo} ", erro.Message);
                return StatusCode(500);
            }
        }

        // PUT <GrupoController>/{_id}?id_empresa={id_empresa}
        [HttpPut("{_id}")]
        [Authorize]
        public IActionResult Put(string _id, string id_empresa)
        {
            try
            {

                List<Grupo> gruposCadastrados = DbJson.Grupos();
                List<Empresa> empresasCadastradas = DbJson.Empresas();

                Grupo grupo = gruposCadastrados.Find(g => g.id == _id);
                Empresa empresa = empresasCadastradas.Find(e => e.id == id_empresa);
                if (grupo != null && empresa != null)
                {
                    if(grupo.companys == null)
                    {
                        grupo.companys = new List<string>();
                    }
                    grupo.companys.Add(empresa.id);
                    DbJson.UpdateGrupos(gruposCadastrados);
                    return Ok("Empresa associada ao Grupo com sucesso!");
                }
                else
                {
                    return BadRequest("Grupo ou Empresa não econtrado!");
                }

            }
            catch (Exception erro)
            {
                Log.LogArquivo.SetLog("GrupoController - PUT {_id} Query {id_empresa} ", erro.Message);
                return StatusCode(500);
            }
        }


    }
}
