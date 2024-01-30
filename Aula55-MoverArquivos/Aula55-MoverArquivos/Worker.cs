namespace Aula55_MoverArquivos
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            string pastaOrigem = "c:\\Teste\\Pasta 1";
            string pastaDestinoTxt = @"c:\Teste\Pasta 2\Txt";
            string pastaDestinoExcel = @"c:\Teste\Pasta 2\Excel";
            string pastaDestinoPowerPoint = @"c:\Teste\Pasta 2\PowerPoint";
            string pastaDestinoWord = @"c:\Teste\Pasta 2\Word";
            List<FileInfo> arquivos;

            DirectoryInfo directoryInfo = new DirectoryInfo(pastaOrigem);
            while (!stoppingToken.IsCancellationRequested)
            {

                    arquivos = directoryInfo.GetFiles()
                    .Where(x => x.Extension.Equals(".txt") 
                    || x.Extension.Equals(".xlsx")
                    || x.Extension.Equals(".pptx")
                    || x.Extension.Equals(".docx")
                    )
                    .ToList();

                foreach (var arquivo in arquivos)
                {
                    //_logger.LogInformation(arquivo.Name);
                    //arquivo.MoveTo(pastaDestino + arquivo.Name);
                    if (arquivo.Extension.Equals(".txt"))
                        arquivo.MoveTo(Path.Combine(pastaDestinoTxt, arquivo.Name));
                    else if(arquivo.Extension.Equals(".xlsx"))
                        arquivo.MoveTo(Path.Combine(pastaDestinoExcel, arquivo.Name));
                    else if (arquivo.Extension.Equals(".pptx"))
                        arquivo.MoveTo(Path.Combine(pastaDestinoPowerPoint, arquivo.Name));
                    else if (arquivo.Extension.Equals(".docx"))
                        arquivo.MoveTo(Path.Combine(pastaDestinoWord, arquivo.Name));

                }

                //_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}