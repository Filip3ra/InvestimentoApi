const CDI = 14.15,
  calculatorForm = document.getElementById("calculator_form"),
  resultDesc = document.getElementById("result_desc"),
  result = document.getElementById("result"),
  messageErro = document.getElementById("mensagemErro"),
  detalhesLista = document.getElementById("detalhes");

function calcularInvestimento(
  patrimonio,
  valorMensal,
  percentualCDI,
  taxaAnual,
  inflacaoAnual,
  valorObjetivo
) {
  let saldo = patrimonio;
  let taxaMensal =
    Math.pow(1 + (percentualCDI / 100) * (CDI / 100), 1 / 12) - 1;
  let inflacaoMensal = Math.pow(1 + inflacaoAnual / 100, 1 / 12) - 1;
  let meses = 0;
  let listaAnual = [];

  while (saldo < valorObjetivo) {
    let investimentosAnuais = 0;
    for (let i = 0; i < 12; i++) {
      investimentosAnuais =
        (investimentosAnuais + valorMensal) * (1 + taxaMensal - inflacaoMensal);
    }

    saldo =
      (saldo * (1 + taxaAnual / 100)) / (1 + inflacaoAnual / 100) +
      investimentosAnuais;
    meses += 12;

    listaAnual.push({
      ano: meses / 12,
      saldo: saldo,
    });
  }

  return { anos: Math.floor(meses / 12), listaAnual };
}

function calcular() {
  let patrimonio = document.getElementById("patrimonio").value.trim();
  let valorMensal = document.getElementById("valorMensal").value.trim();
  let percentualCDI = document.getElementById("percentualCDI").value.trim();
  let taxaAnual = document.getElementById("taxaAnual").value.trim();
  let inflacaoAnual = document.getElementById("inflacaoAnual").value.trim();
  let valorObjetivo = document.getElementById("valorObjetivo").value.trim();
  let messageErro = document.getElementById("messageErro");

  if (
    // Verifica espaços vazios
    patrimonio === "" ||
    valorMensal === "" ||
    percentualCDI === "" ||
    taxaAnual === "" ||
    inflacaoAnual === "" ||
    valorObjetivo === ""
  ) {
    messageErro.innerText = "Preencha todos os campos corretamente.";
    messageErro.classList.remove("hidden");
    return;
  }

  messageErro.classList.add("hidden");

  patrimonio = parseFloat(patrimonio);
  valorMensal = parseFloat(valorMensal);
  percentualCDI = parseFloat(percentualCDI);
  taxaAnual = parseFloat(taxaAnual);
  inflacaoAnual = parseFloat(inflacaoAnual);
  valorObjetivo = parseFloat(valorObjetivo);

  // Verifica valores inválidos
  if (
    isNaN(patrimonio) ||
    isNaN(valorMensal) ||
    isNaN(percentualCDI) ||
    isNaN(taxaAnual) ||
    isNaN(inflacaoAnual) ||
    isNaN(valorObjetivo)
  ) {
    messageErro.innerText = "Preencha todos os campos com valores válidos.";
    messageErro.classList.remove("hidden");
    return;
  }

  let resultado = calcularInvestimento(
    patrimonio,
    valorMensal,
    percentualCDI,
    taxaAnual,
    inflacaoAnual,
    valorObjetivo
  );

  exibirResultado(valorObjetivo, resultado);
}

function exibirResultado(valorObjetivo, resultado) {
  result.classList.remove("hidden");

  // Formatar o valor objetivo
  let valorFormatado = valorObjetivo.toLocaleString("pt-BR", {
    style: "currency",
    currency: "BRL",
  });

  // Atualizar o texto principal
  resultDesc.innerText = `Você atingirá ${valorFormatado} em aproximadamente ${resultado.anos} anos.`;

  // Atualizar a lista de detalhes
  detalhesLista.innerHTML = "";
  resultado.listaAnual.forEach(({ ano, saldo }) => {
    let li = document.createElement("li");

    // Formata valor acumulado anualmente
    let saldoFormatado = saldo.toLocaleString("pt-BR", {
      style: "currency",
      currency: "BRL",
    });

    li.textContent = `Ano ${ano}: Saldo acumulado = ${saldoFormatado}`;
    detalhesLista.appendChild(li);
  });
}

calculatorForm.addEventListener("submit", (event) => {
  event.preventDefault();
  calcular();
});
