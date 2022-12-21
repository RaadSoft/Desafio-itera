﻿using Desafio_itera.Models;
using System.Collections.Generic;
using System.Text.Json;

namespace Desafio_itera.json
{
    public class DbJson
    {
        private static string _empresaJson = "./json/companys.json";
        private static string _grupoJson = "./json/group.json";

        public static List<Empresa> Empresas()
        {
            try
            {
                string jsonEmpresas = System.IO.File.ReadAllText(_empresaJson);
                List<Empresa> empresas = JsonSerializer.Deserialize<List<Empresa>>(jsonEmpresas);
                return empresas;
            }
            catch (System.Exception erro)
            {
                throw new System.Exception("ERRO Ler Arquivo Empresas:" + erro.Message);
            }
        }
        public static List<Grupo> Grupos()
        {
            try
            {
                string jsonGrupos = System.IO.File.ReadAllText(_grupoJson);
                List<Grupo> grupos = JsonSerializer.Deserialize<List<Grupo>>(jsonGrupos);
                return grupos;
            }
            catch (System.Exception erro)
            {
                throw new System.Exception("ERRO Ler Arquivo Grupos:" + erro.Message);
            }
        }
        public static bool UpdateEmpresas(List<Empresa> empresas)
        {
            try
            {
                System.IO.File.WriteAllText(_empresaJson, JsonSerializer.Serialize(empresas));
                return true;
            }
            catch (System.Exception erro)
            {
                throw new System.Exception("ERRO Atualizar Arquivo Empresas:" + erro.Message);
            }
        }
        public static bool UpdateGrupos(List<Grupo> grupos)
        {
            try
            {
                System.IO.File.WriteAllText(_grupoJson, JsonSerializer.Serialize(grupos));
                return true;
            }
            catch (System.Exception erro)
            {
                throw new System.Exception("ERRO Atualizar Arquivo Grupos:" + erro.Message);
            }
        }
    }
}
