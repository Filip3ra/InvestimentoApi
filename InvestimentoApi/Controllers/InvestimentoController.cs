using Microsoft.AspNetCore.Mvc;
using InvestimentoApi.Services;

[ApiController] // api com controller
[Route("api/investimento")] // caminho base
public class InvestimentoController : ControllerBase
{
    [HttpGet("calcular")] // requisição get no caminho api/investimento/calcular
    public IActionResult CalcularInvestimento(
        [FromQuery] double valorMensal, 
        [FromQuery] double taxa,
        [FromQuery] string periodicidade, 
        [FromQuery] double inflacaoAnual) 
    {
    /* 
    Parâmetros do método devem corresponder exatamente aos nomes da query.
    ASP.NET Core analisa a url recebida e extrai esses valores do parâmetros
    para armazenar nas variáveis.
    */

    try
    {
      var taxaConvertida = ServicoInvestimento.ConverterTaxaParaMensal(taxa, periodicidade);
      var meses = ServicoInvestimento.CalcularTempo(valorMensal, taxaConvertida, inflacaoAnual);
      int anos = meses / 12;
      int mesesRestantes = meses % 12;
      return Ok(new { anos, meses = mesesRestantes });
    }
    catch (Exception ex)
    {
      return BadRequest(ex.Message);
    }
    }
}


