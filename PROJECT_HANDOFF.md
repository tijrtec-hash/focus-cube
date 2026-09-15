# PROJECT_HANDOFF.md — FOCUS CUBE

**Atualização:** 15/09/2026  
**Checkpoint:** TIMER-01 / countdown funcional 0.3.0  
**Versão de trabalho:** 0.3.0  
**Validação existente:** VIS-01 / 0.2.1 aprovado visualmente pelo usuário em 15/09/2026. O usuário considerou o visual perfeito e registrou apenas leve suavidade/baixa resolução nas letras pequenas rasterizadas, sem bloquear a continuidade.  
**Etapa atual:** compilar 0.3.0 no GitHub Actions e validar countdown, pause/continue, reset e incrementos no Windows.

## 1. Estado vigente

O Focus Cube é um timer/widget desktop para Windows em C# + WPF + .NET 8.

### Visual aprovado

VIS-01 foi aprovado manualmente pelo usuário. Permanecem aprovados:

- escala `170 × 251` DIPs;
- shell híbrido baseado na referência raster;
- display WPF sobre o shell;
- relevo/material;
- gaveta inferior;
- botões e composição geral;
- transparência e always-on-top observados no Windows.

Observação não bloqueante para POLISH-01:

- letras pequenas rasterizadas dos botões parecem um pouco suaves/sem resolução na escala atual.

### TIMER-01 implementado no código nesta versão

- novo `TimerSession.cs`, separando estado temporal da camada WPF;
- duração base de 60 minutos;
- início automático ao abrir;
- countdown calculado por deadline, não por subtração fixa de um segundo;
- display `mm:ss` em tempo real;
- pause/continue real;
- reset para 60 minutos preservando o estado rodando/pausado;
- `+5`, `+10`, `+30`, `+60` funcionais;
- incrementos estendem o tempo restante e a duração corrente da sessão;
- aro ligado ao progresso real da sessão;
- tooltip do botão central alterna entre `Pausar` e `Continuar`.

Ainda não validado:

- compilação 0.3.0 no GitHub Actions;
- comportamento temporal real no Windows;
- incrementos e reset reais;
- regressão visual da 0.3.0.

Ainda não implementado/refinado:

- alerta ao terminar;
- comportamento visual especial em `00:00`;
- refinamento/animação do aro em RING-01;
- expansão/recolhimento em DRAWER-01;
- snap/persistência em DOCK-01;
- menu `...`;
- nitidez dos pequenos textos rasterizados em POLISH-01.

## 2. Próxima ação exata

1. Enviar/substituir os arquivos da revisão 0.3.0 no mesmo repositório GitHub pelo navegador.
2. Confirmar a alteração no site.
3. Abrir **Actions → Windows build and publish**.
4. Se vermelho, enviar somente o primeiro erro relevante da nova execução.
5. Se verde, baixar **FocusCube-win-x64**.
6. Extrair e abrir `FocusCube.exe`.
7. Executar `TESTE_TIMER01.md`.
8. Se todos os cenários passarem, registrar `Focus Cube TIMER-01 aprovado` e avançar para refinamento do aro/estado final.

## 3. Regras do workflow atual

- Todo o processo operacional do usuário ocorre pelo navegador.
- Não exigir Git local, GitHub Desktop, Visual Studio ou SDK .NET.
- Não introduzir Pull Request ou branches auxiliares sem necessidade concreta.
- GitHub Actions é o compilador do projeto para os testes do usuário.
- Workflow verde prova compilação/publicação daquela versão; não prova funcionamento do timer.
- A aprovação funcional depende do teste do usuário.

## 4. Decisões funcionais vigentes

- sessão padrão: 60 minutos;
- inicia automaticamente ao abrir;
- botão central alterna pause/continue;
- reset restaura a duração base de 60 minutos;
- reset preserva se a sessão estava rodando ou pausada;
- `+5/+10/+30/+60` adicionam tempo à sessão atual;
- incrementos também aumentam a duração corrente usada para calcular progresso;
- se o timer terminou e o usuário adiciona tempo, a nova duração fica pausada até continuar;
- o aro recebe o progresso temporal real, mas refinamentos visuais continuam pertencendo a RING-01.

## 5. Validação disponível

Comprovado pelo usuário:

- GitHub Actions conseguiu produzir executável das versões anteriores após correção do restore `win-x64`;
- `FocusCube.exe` abriu no Windows;
- 0.2.1 foi aprovada visualmente;
- tamanho, shell e composição atuais foram aceitos.

Não comprovado nesta versão:

- build 0.3.0;
- timer funcional;
- pausa/continuação;
- reset/incrementos;
- progresso temporal do aro.

## 6. Arquivos alterados nesta revisão

- `src/FocusCube/TimerSession.cs`
- `src/FocusCube/MainWindow.xaml`
- `src/FocusCube/MainWindow.xaml.cs`
- `README.md`
- `PROJECT_HANDOFF.md`
- `TESTE_TIMER01.md`

## 7. Workflow de build

O workflow vigente permanece:

`.github/workflows/windows.yml`

Com restore específico para `win-x64` antes do build/publish.

Artifact esperado:

`FocusCube-win-x64`
