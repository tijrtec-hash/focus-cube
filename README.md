# Focus Cube · 0.1.0 — BASE-01

Focus Cube é um timer/widget desktop para Windows com aparência de pequeno objeto digital/hardware premium.

## Estado atual

O protótipo já contém o primeiro shell visual, mas o timer ainda é estático.

Implementado no código:

- janela sem borda;
- transparência ao redor do widget;
- always-on-top atual;
- corpo/display com relevo e sombras;
- timer estático `52:18`;
- aro vetorial estático;
- gaveta com `+5/+10/+30/+60`;
- estados visuais dos botões;
- arraste básico.

Ainda não implementado:

- countdown real;
- pause/reset/incrementos funcionais;
- aro dinâmico verde→vermelho;
- expansão/recolhimento;
- snap/persistência.

## Como compilar e testar — somente navegador

Não é necessário instalar Git, Visual Studio, .NET SDK ou GitHub Desktop.

1. Envie/substitua os arquivos desta versão no mesmo repositório GitHub.
2. Confirme a alteração pelo próprio site.
3. Abra **Actions**.
4. Abra **Windows build and publish**.
5. Aguarde a execução.
6. Se ficar vermelha, abra **Build and publish** e copie o primeiro erro relevante.
7. Se ficar verde, baixe **FocusCube-win-x64** em **Artifacts**.
8. Extraia o ZIP baixado.
9. Execute `FocusCube.exe`.
10. Siga `TESTE_VIS01.md`.

## Referência visual

`docs/reference/widget-expanded-approved.png`

## Continuidade

Antes de alterar o projeto, leia:

1. `AGENTS.md`
2. `PROJECT_HANDOFF.md`
