# Focus Cube · 0.2.1 — VIS-01

Focus Cube é um timer/widget desktop para Windows com aparência de pequeno objeto digital/hardware premium.

## Estado atual

As versões 0.1.0 e 0.2.0 foram executadas no Windows, mas a fidelidade visual ainda não foi aprovada. A 0.2.1 adota uma estratégia híbrida para se aproximar diretamente da ilustração aprovada.

Implementado nesta revisão:

- janela sem borda, transparente e always-on-top;
- tamanho padrão `170 × 251` DIPs;
- composição interna baseada na proporção exata `1388 × 2048` da referência;
- carcaça, gaveta, botões em repouso, relevo e reflexos derivados diretamente da referência visual;
- recorte do shell para manter transparência fora do objeto;
- display limpo redesenhado sobre o shell;
- `TIMER` e `52:18` reais em WPF;
- aro vetorial segmentado em 12 partes;
- cor do aro preparada para verde→amarelo→laranja→vermelho;
- hit targets reais sobre `+5/+10/+30/+60`, reset, pause e menu;
- arraste básico do widget.

Ainda não implementado:

- countdown real;
- pause/reset/incrementos funcionais;
- ligação do aro ao countdown;
- expansão/recolhimento;
- snap/persistência.

## Estratégia visual

A arte da referência é usada somente para o **shell estático** (material, relevo, gaveta e botões em repouso). O timer e o aro não são imagens: continuam sendo componentes WPF e serão ligados ao motor de tempo nas próximas etapas.

## Como compilar e testar — somente navegador

1. Envie/substitua os arquivos desta versão no mesmo repositório GitHub.
2. Confirme a alteração pelo site.
3. Abra **Actions → Windows build and publish**.
4. Se ficar vermelho, abra **Build and publish** e envie o primeiro erro relevante.
5. Se ficar verde, baixe **FocusCube-win-x64**.
6. Extraia o ZIP.
7. Execute `FocusCube.exe`.
8. Siga `TESTE_VIS01.md`.

## Referência visual

`docs/reference/widget-expanded-approved.png`

## Continuidade

Leia antes de alterar:

1. `AGENTS.md`
2. `PROJECT_HANDOFF.md`
