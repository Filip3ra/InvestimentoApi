using Microsoft.AspNetCore.Mvc;
using InvestimentoApi.Services;

[ApiController]
[Route("api/investimento")]
public class InvestimentoController : ControllerBase
{
    [HttpGet("calcular")]
    public IActionResult CalcularInvestimento(
        [FromQuery] double patrimonio,
        [FromQuery] double valorMensal, 
        [FromQuery] double percentualCDI, 
        [FromQuery] double taxaAnual, 
        [FromQuery] double inflacaoAnual, 
        [FromQuery] double valorObjetivo) 
    {
        try
        {
            var meses = ServicoInvestimento.CalcularTempo(
                patrimonio, valorMensal, percentualCDI, taxaAnual, inflacaoAnual, valorObjetivo);
            
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
