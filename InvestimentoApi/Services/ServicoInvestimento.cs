namespace InvestimentoApi.Services;

public static class ServicoInvestimento
{
    private const double CDI = 14.15; // Taxa CDI atual (%)

    public static int CalcularTempo(
        double patrimonio, double valorMensal, 
        double percentualCDI, double taxaAnual,
        double inflacaoAnual, double valorObjetivo)
    {
        double saldo = patrimonio;
        double taxaMensal = Math.Pow(1 + (percentualCDI / 100) * (CDI / 100), 1.0 / 12) - 1;
        double inflacaoMensal = Math.Pow(1 + (inflacaoAnual / 100), 1.0 / 12) - 1;
        int meses = 0;

        while (saldo < valorObjetivo)
        {
            // Acumulando investimento mensal ao longo do ano
            double investimentosAnuais = 0;
            for (int i = 0; i < 12; i++)
            {// acho que ta errado também
                investimentosAnuais = (investimentosAnuais + valorMensal) * (1 + taxaMensal - inflacaoMensal);
            }

            // Crescimento do patrimônio inicial no final do ano
            saldo = (saldo * (1 + taxaAnual / 100) / (1 + inflacaoAnual / 100)) + investimentosAnuais;
            meses += 12;
            Console.WriteLine($"Ano {meses / 12}: Saldo acumulado = R$ {saldo:F2}");
        }

        return meses;
    }
}
