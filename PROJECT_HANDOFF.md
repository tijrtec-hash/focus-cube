# PROJECT_HANDOFF.md — FOCUS CUBE

**Atualização:** 15/09/2026  
**Checkpoint:** VIS-01 / refinamento visual 0.2.0  
**Versão de trabalho:** 0.2.0  
**Validação existente:** o primeiro executável 0.1.0 foi gerado e executado no Windows; o usuário enviou captura real e rejeitou a fidelidade visual por tamanho excessivo e distância da referência.  
**Etapa atual:** compilar a revisão VIS-01 0.2.0 no GitHub Actions e comparar novamente com a referência.

## 1. Estado vigente

O Focus Cube é um timer/widget desktop para Windows em C# + WPF + .NET 8.

O primeiro executável real confirmou que a base WPF abre e renderiza como widget. A captura enviada pelo usuário mostrou dois problemas principais:

- o conjunto ficou grande demais no desktop;
- o shell estava visualmente mais simples e escuro que a referência aprovada.

A revisão 0.2.0 implementa no código:

- janela reduzida de `360 × 486` para `260 × 390` DIPs;
- proporção geral mais próxima da referência vertical aprovada;
- carcaça mais clara, metálica e com highlights de borda;
- display maior em relação à carcaça, com menos molduras consumindo área útil;
- timer `52:18` maior proporcionalmente;
- aro mais espesso e segmentado em 12 partes;
- cor automática do aro preparada para `verde → amarelo → laranja → vermelho` conforme `Progress` diminui;
- chevron integrado dentro da face principal, em vez de uma ponte externa;
- gaveta mais próxima da proporção da referência;
- botões rápidos com relevo, highlight, sombra e glow no hover;
- reset e menu visualmente planos; pause permanece como botão físico elevado;
- workflow corrigido para restaurar explicitamente o runtime `win-x64` antes de build/publish.

Ainda não implementado:

- countdown real;
- pause/reset/incrementos funcionais;
- ligação do aro ao timer real;
- expansão/recolhimento da gaveta;
- snap/persistência.

A referência visual aprovada permanece em:

`docs/reference/widget-expanded-approved.png`

## 2. Próxima ação exata

1. Enviar/substituir os arquivos da revisão 0.2.0 no mesmo repositório GitHub pelo navegador.
2. Confirmar a alteração no site.
3. Abrir **Actions → Windows build and publish**.
4. Se vermelho, enviar somente o primeiro erro relevante da nova execução.
5. Se verde, baixar **FocusCube-win-x64**.
6. Extrair e abrir `FocusCube.exe`.
7. Executar `TESTE_VIS01.md` e enviar uma captura do widget no desktop.
8. Comparar tamanho, proporções, aro, relevo e gaveta com a referência antes de iniciar o motor do timer.

## 3. Regras do workflow atual

- Todo o processo operacional do usuário ocorre pelo navegador.
- Não exigir Git local, GitHub Desktop, Visual Studio ou SDK .NET.
- Não introduzir Pull Request ou branches auxiliares sem necessidade concreta.
- GitHub Actions é o compilador do projeto para os testes do usuário.
- Workflow verde prova compilação/publicação daquela versão; não prova fidelidade visual.
- A aprovação visual depende do teste do usuário.

## 4. Decisões visuais vigentes

- timer grande e dominante;
- widget pequeno no desktop;
- display preto/recuado ocupando a maior parte da face superior;
- carcaça grafite/cinza com relevo, bevel e highlights sutis;
- gaveta inferior integrada e ligeiramente mais estreita que o corpo;
- botões luminosos `+5`, `+10`, `+30`, `+60`;
- aro segmentado e funcional ao redor do timer;
- cor do aro: `verde → amarelo → laranja → vermelho` conforme o tempo acaba;
- reset e menu secundários discretos; pause é o controle físico principal;
- sem dashboard permanente;
- interface principal construída com elementos reais do WPF.

## 5. Validação disponível

Comprovado pelo usuário:

- um `FocusCube.exe` da base 0.1.0 foi executado no Windows;
- janela sem borda/transparente abriu;
- widget foi renderizado;
- a fidelidade VIS-01 da base 0.1.0 **não foi aprovada**;
- problemas relatados: tamanho excessivo e visual distante da referência.

No ambiente de preparação da 0.2.0 foi possível somente inspeção/checagem estrutural. A revisão 0.2.0 ainda não foi compilada nem vista no Windows.

## 6. Limites atuais

- tipografia pode continuar diferindo da imagem gerada de referência;
- materiais WPF dependem de teste real para ajuste fino;
- o aro possui lógica visual de cor, mas ainda recebe `Progress` estático;
- o tamanho em DIPs pode variar visualmente conforme escala/DPI do Windows; o teste real dirá se é necessário reduzir mais.

## 7. Histórico resumido

### BASE-01 / 0.1.0

- documentação de continuidade criada;
- projeto WPF criado;
- shell visual inicial implementado;
- GitHub Actions preparado;
- primeiro executável executado pelo usuário;
- VIS-01 rejeitado por tamanho e fidelidade.

### VIS-01 / 0.2.0

- reduzido o footprint do widget;
- refinados material, display, proporções e controles;
- criado aro segmentado com paleta dinâmica verde→vermelho;
- corrigido workflow `win-x64` para refletir o hotfix usado no GitHub.
