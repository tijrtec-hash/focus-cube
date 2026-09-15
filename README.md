# Focus Cube · 0.3.0 — TIMER-01

Focus Cube é um timer/widget desktop para Windows com aparência de pequeno objeto digital/hardware premium.

## Estado atual

**VIS-01 foi aprovado visualmente pelo usuário em 15/09/2026.** O shell híbrido, escala e aparência geral foram considerados corretos. O usuário observou apenas que as letras pequenas rasterizadas dos botões parecem um pouco suaves; esse refinamento fica registrado para POLISH-01 e não bloqueia as funcionalidades.

A versão 0.3.0 inicia **TIMER-01**.

Implementado nesta revisão:

- countdown real com base inicial de `60:00`;
- início automático da sessão ao abrir o aplicativo;
- relógio baseado em deadline, evitando acumular erro de um segundo por tick;
- botão central alterna **pausar / continuar**;
- reset volta a sessão para `60:00` e preserva o estado atual (rodando ou pausado);
- `+5`, `+10`, `+30`, `+60` adicionam tempo à sessão atual;
- incrementos aumentam também a duração corrente usada como referência de progresso;
- display é atualizado em tempo real;
- aro já recebe o progresso real da sessão e usa a regra existente verde→amarelo→laranja→vermelho;
- arraste básico e shell visual aprovado foram preservados.

Ainda não implementado/refinado:

- alerta ao chegar a zero;
- animações específicas do aro e estado final;
- expansão/recolhimento da gaveta;
- snap/persistência;
- painel/menu `...`;
- melhoria de nitidez dos textos rasterizados pequenos.

## Comportamento do timer

- Ao abrir, começa em `60:00` e inicia a contagem.
- **Pausar/continuar:** mantém o tempo restante sem perder segundos enquanto pausado.
- **Reset:** restaura `60:00`. Se estava rodando, continua rodando; se estava pausado, permanece pausado.
- **+5/+10/+30/+60:** acrescentam o valor ao tempo restante e à duração da sessão atual.
- Se o timer já terminou, adicionar tempo cria uma nova duração corrente, mas permanece pausado até o usuário continuar.

## Como compilar e testar — somente navegador

1. Envie/substitua os arquivos desta versão no mesmo repositório GitHub.
2. Confirme a alteração pelo site.
3. Abra **Actions → Windows build and publish**.
4. Se ficar vermelho, abra **Build and publish** e envie o primeiro erro relevante.
5. Se ficar verde, baixe **FocusCube-win-x64**.
6. Extraia o ZIP.
7. Execute `FocusCube.exe`.
8. Siga `TESTE_TIMER01.md`.

## Referência visual

`docs/reference/widget-expanded-approved.png`

## Continuidade

Leia antes de alterar:

1. `AGENTS.md`
2. `PROJECT_HANDOFF.md`
