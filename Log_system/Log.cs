using System;
using System.IO;

namespace Desafio_itera.Log
{
    public class LogArquivo
    {
        private static string _arquivoLog = "./Log/Desafio_ITERA_Log.log";
        public static bool SetLog(string funcao, string erro)
        {
            if (!File.Exists(_arquivoLog)){
                File.Create(_arquivoLog).Close(); ;
            }

            File.AppendAllText(_arquivoLog,
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+
                " - FUNÇÃO: "+ funcao +
                Environment.NewLine +
                "ERRO:" + erro +
                Environment.NewLine+
                "-".PadRight(100,'-')+
                Environment.NewLine
                );
            return true;
        }
    }
}
