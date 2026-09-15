# PROJECT_HANDOFF.md — FOCUS CUBE

**Atualização:** 15/09/2026  
**Checkpoint:** POLISH-01 / release candidate 1.0.0  
**Versão de trabalho:** 1.0.0  
**Validação existente:** VIS-01 / 0.2.1 aprovado visualmente pelo usuário em 15/09/2026. O usuário considerou o visual perfeito, com apenas leve suavidade nas letras pequenas rasterizadas.  
**Etapa atual:** enviar a versão 1.0.0 ao GitHub Actions, validar testes/build e executar o roteiro final no Windows.

## 1. Estado vigente

O Focus Cube é um timer/widget desktop para Windows em C# + WPF + .NET 8.

### Visual preservado

A versão 1.0.0 mantém o shell híbrido e a escala aprovados:

- `170 × 251` DIPs expandido;
- carcaça/relevo derivados da referência aprovada;
- display, tempo e aro reais em WPF;
- gaveta integrada;
- material/relief sem redesenho visual amplo.

### Funcionalidades implementadas no código nesta versão

**Timer**

- countdown por deadline;
- sessão inicial `60:00` e início automático;
- pause/continue;
- reset;
- `+5/+10/+30/+60`;
- seleção exata de `5`, `25` e `60` minutos pelo menu;
- tempo personalizado em minutos, `MM:SS` ou `H:MM:SS`;
- reset volta à última duração exata selecionada;
- quick-add estende a sessão sem alterar o alvo do reset;
- formato do display adapta-se para durações acima de uma hora.

**Aro/alerta**

- progresso proporcional real;
- transição de cor verde → amarelo → laranja → vermelho;
- em `00:00`, aro fica vermelho e o display entra em estado `FIM`;
- pulso visual de conclusão;
- alerta sonoro opcional usando som nativo do Windows.

**Gaveta/interface**

- seta abre/recolhe a gaveta;
- animação de 180 ms;
- janela compacta usa aproximadamente `170 × 170` sem comprimir a arte;
- quando está dockada embaixo, expandir/recolher preserva o alinhamento inferior;
- menu `...` funcional;
- opção de sair pelo menu;
- atalhos locais: `Space`, `R`, `E`.

**Desktop**

- arraste pela área não interativa;
- snap em bordas/cantos do monitor atual;
- posição persistida;
- estado da gaveta persistido;
- always-on-top configurável e persistido;
- som de conclusão configurável e persistido;
- preferências em `%LOCALAPPDATA%\FocusCube\settings.json`;
- preferência corrompida não impede o app de abrir.

**Qualidade/build**

- versão de assembly definida como `1.0.0`;
- projeto `FocusCube.Tests` sem dependências externas de teste;
- testes automatizados de TimerSession e parser de duração;
- GitHub Actions agora executa lógica de testes antes de build/publish;
- artifact esperado continua `FocusCube-win-x64`.

## 2. Próxima ação exata

1. Enviar/substituir os arquivos da versão 1.0.0 no mesmo repositório GitHub pelo navegador.
2. Confirmar a alteração no site.
3. Abrir **Actions → Windows build and publish**.
4. Se vermelho, enviar somente o primeiro erro relevante da nova execução.
5. Se verde, confirmar que **Run logic tests**, **Build app** e **Publish win-x64** ficaram verdes.
6. Baixar **FocusCube-win-x64**.
7. Extrair e executar `FocusCube.exe`.
8. Executar `TESTE_FINAL01.md` por completo.
9. Se todos os cenários passarem, registrar `Focus Cube 1.0 aprovado`.

## 3. Validação disponível

Comprovado pelo usuário antes desta versão:

- GitHub Actions produziu executáveis das revisões anteriores;
- `FocusCube.exe` abriu no Windows;
- VIS-01 / 0.2.1 foi aprovado visualmente;
- escala, shell e composição foram aceitos.

Verificado estruturalmente nesta sessão:

- XAML e `.csproj` são XML válidos;
- handlers declarados no XAML existem no code-behind;
- delimitadores C# básicos estão balanceados;
- arquivos obrigatórios e workflow estão presentes.

Ainda **não comprovado** para 1.0.0:

- testes automatizados no GitHub Actions;
- compilação final;
- timer e quick-add desta consolidação;
- alerta de conclusão;
- gaveta animada;
- menu/custom time;
- snap/persistência;
- opções persistidas.

## 4. Arquivos principais alterados/adicionados

- `src/FocusCube/TimerSession.cs`
- `src/FocusCube/MainWindow.xaml`
- `src/FocusCube/MainWindow.xaml.cs`
- `src/FocusCube/FocusCubeSettings.cs`
- `src/FocusCube/SettingsStore.cs`
- `src/FocusCube/TimeInputParser.cs`
- `src/FocusCube/DesktopDocking.cs`
- `src/FocusCube/NativeFeedback.cs`
- `src/FocusCube/CustomTimeDialog.xaml`
- `src/FocusCube/CustomTimeDialog.xaml.cs`
- `src/FocusCube/FocusCube.csproj`
- `src/FocusCube.Tests/FocusCube.Tests.csproj`
- `src/FocusCube.Tests/Program.cs`
- `.github/workflows/windows.yml`
- `AGENTS.md`
- `PROJECT_HANDOFF.md`
- `README.md`
- `TESTE_FINAL01.md`

## 5. Limites conhecidos

- Este ambiente não possui SDK .NET/Windows para compilar WPF; a compilação real permanece responsabilidade do GitHub Actions.

- As letras pequenas gravadas na textura raster podem parecer levemente suaves na escala atual; o usuário aceitou isso como não bloqueante.
- Snap/persistência dependem de teste real no Windows, especialmente em setups com múltiplos monitores/DPI diferentes.
- O alerta sonoro usa o som nativo do Windows, sem arquivo de áudio externo.
- O app não instala inicialização automática com o Windows e não usa tray icon nesta versão.

## 6. Workflow

Workflow vigente:

`.github/workflows/windows.yml`

Sequência esperada:

`Restore app → Restore logic tests → Run logic tests → Build app → Publish win-x64 → Verify executable → Upload artifact`

Artifact esperado:

`FocusCube-win-x64`
