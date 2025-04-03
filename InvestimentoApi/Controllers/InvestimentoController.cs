using Microsoft.AspNetCore.Mvc;
using InvestimentoApi.Services;

[ApiController]
[Route("api/investimento")]
/* 
ControllerBase : não tem suporte para veiw, melhor pra api sem interface visual
Controller : suporte view, bom pra aplicação com html razor view
*/
public class InvestimentoController : Controller
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
            var resultado = ServicoInvestimento.CalcularTempo(
            patrimonio, valorMensal, percentualCDI, taxaAnual, inflacaoAnual, valorObjetivo);


            //var resultado = ServicoInvestimento.CalcularTempo(
            //  patrimonio, valorMensal, percentualCDI, taxaAnual, inflacaoAnual, valorObjetivo);

            int anos = resultado.Meses / 12;
            //int mesesRestantes = anos % 12;
            var listaAnual = resultado.ListaAnos;





            return Ok(new { anos, listaAnual });
            //return View("index", resultado);
            //return Ok(resultado);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
