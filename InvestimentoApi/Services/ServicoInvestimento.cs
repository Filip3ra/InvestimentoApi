namespace InvestimentoApi.Services;

public static class ServicoInvestimento
{

  public static double ConverterTaxaParaMensal(double taxa, string peridiocidade)
  {
    switch (peridiocidade.ToLower())
    {
      case "anual":
        return Math.Pow(1 + (taxa / 100), 1.0 / 12) - 1;
      case "trimestral":
        return Math.Pow(1 + (taxa / 100), 1.0 / 3) - 1;
      case "semestral":
        return Math.Pow(1 + (taxa / 100), 1.0 / 6) - 1;
      case "mensal":
        return taxa / 100;
      default:
        throw new ArgumentException("Peridiocidade inválida.");
    }
  }

  public static int CalcularTempo(double valorMensal, double taxaMensal, double inflacaoAnual)
  {

    double saldo = 0;
    double inflacaoMensal = Math.Pow(1 + (inflacaoAnual / 100), 1.0 / 12) - 1;
    int meses = 0;
    while (saldo < 100000)
    {
      saldo = (saldo + valorMensal) * (1 + taxaMensal - inflacaoMensal);
      meses++;
    }
    return meses;        
  }
}

