# Focus Cube · 0.2.0 — VIS-01

Focus Cube é um timer/widget desktop para Windows com aparência de pequeno objeto digital/hardware premium.

## Estado atual

A primeira execução real da 0.1.0 confirmou que o aplicativo abre no Windows, mas o usuário rejeitou a fidelidade visual porque o widget ficou grande demais e distante da referência.

A 0.2.0 é o primeiro refinamento VIS-01.

Implementado no código:

- janela sem borda e transparente;
- always-on-top;
- tamanho reduzido para `260 × 390` DIPs;
- carcaça e gaveta com relevo e highlights refinados;
- display preto/recuado;
- timer estático `52:18` em maior destaque;
- aro vetorial segmentado;
- cor visual do aro preparada para verde→amarelo→laranja→vermelho conforme `Progress`;
- gaveta com `+5/+10/+30/+60`;
- botões com relevo, hover e pressed;
- reset/menu discretos e pause elevado;
- arraste básico.

Ainda não implementado:

- countdown real;
- pause/reset/incrementos funcionais;
- ligação do aro ao countdown;
- expansão/recolhimento;
- snap/persistência.

## Como compilar e testar — somente navegador

Não é necessário instalar Git, Visual Studio, .NET SDK ou GitHub Desktop.

1. Envie/substitua os arquivos desta versão no mesmo repositório GitHub.
2. Confirme a alteração pelo próprio site.
3. Abra **Actions**.
4. Abra **Windows build and publish**.
5. Aguarde a execução.
6. Se ficar vermelha, abra **Build and publish** e copie somente o primeiro erro relevante.
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
