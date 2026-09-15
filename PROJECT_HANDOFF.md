# PROJECT_HANDOFF.md — FOCUS CUBE

**Atualização:** 15/09/2026  
**Checkpoint:** UX-01 / 1.1.0  
**Versão de trabalho:** 1.1.0  
**Validação existente:** VIS-01 / 0.2.1 aprovado visualmente pelo usuário. A linha 1.0 compilou e executou no Windows após o hotfix de `System.IO`.  
**Etapa atual:** validar no GitHub Actions e no Windows as correções de hit targets, pause/play, sons e animação da gaveta.

## 1. Estado vigente

O Focus Cube é um timer/widget desktop para Windows em C# + WPF + .NET 8.

### Visual preservado

- escala aprovada: `170 × 251` DIPs expandido;
- shell híbrido/raster aprovado;
- display, timer, aro e interação reais em WPF;
- letras pequenas rasterizadas permanecem levemente suaves, já aceitas pelo usuário como não bloqueantes.

### Correções implementadas em 1.1.0

**Hit targets**

- as áreas dos botões rápidos foram realinhadas às dimensões medidas na própria arte aprovada;
- coordenadas do shell de referência:
  - `+5`: `229,1429,206,205`;
  - `+10`: `468,1429,206,205`;
  - `+30`: `707,1429,206,205`;
  - `+60`: `946,1429,207,205`;
  - pause/play: `588,1667,206,205`;
- reset e menu `...` usam hit areas invisíveis centradas nos glyphs;
- removido o contorno azul deslocado; hover agora é apenas um realce muito discreto dentro da área correta.

**Pause / play**

- o glyph de pause deixou de ser fixo na textura;
- o shell foi limpo nessa região e o símbolo passou a ser vetorial;
- enquanto o timer roda: `Ⅱ`;
- quando pausado ou concluído: `▶`.

**Sons**

- três toques internos disponíveis: **Suave**, **Digital** e **Sino**;
- opção de arquivo personalizado `.wav`, `.mp3` ou `.wma`;
- opção **Testar toque**;
- escolha persistida em `%LOCALAPPDATA%\FocusCube\settings.json`;
- arquivo personalizado ausente usa **Suave** como fallback;
- sons internos são gerados localmente em WAV na primeira utilização.

**Gaveta**

- drawer passou a ser uma camada física separada do corpo principal;
- ao recolher, a gaveta sobe para trás do corpo;
- animação de 300 ms com `QuinticEase / EaseOut`, produzindo desaceleração suave no fim;
- expansão faz o movimento inverso;
- comportamento de alinhamento inferior preservado quando dockado embaixo.

**Hotfix preservado**

- `SettingsStore.cs` inclui explicitamente `using System.IO;`.

## 2. Próxima ação exata

1. Enviar/substituir os arquivos da versão 1.1.0 no mesmo repositório GitHub pelo navegador.
2. Abrir **Actions → Windows build and publish**.
3. Se vermelho, enviar somente o primeiro erro relevante da nova execução.
4. Se verde, baixar **FocusCube-win-x64**.
5. Executar `FocusCube.exe`.
6. Seguir `TESTE_UX01.md`.
7. Se tudo passar, registrar `Focus Cube 1.1.0 UX-01 aprovado`.

## 3. Validação disponível

Comprovado pelo usuário:

- executável abre e funciona no Windows;
- visual principal foi aprovado;
- problemas observados na versão anterior: áreas clicáveis deslocadas, ausência de troca pause/play, somente um som e gaveta sem movimento físico de recolhimento.

Verificado nesta sessão:

- coordenadas dos botões físicos foram medidas diretamente na referência raster aprovada;
- XAML foi reorganizado em camada principal + camada da gaveta;
- preferências de som e seleção de arquivo foram implementadas no código;
- versão atualizada para `1.1.0`.

Ainda não comprovado para 1.1.0:

- compilação no GitHub Actions;
- reprodução dos três WAVs internos no Windows;
- reprodução de arquivo personalizado;
- alinhamento final dos hit targets no executável;
- animação da gaveta em runtime;
- troca visual pause/play em runtime.

## 4. Arquivos principais alterados

- `src/FocusCube/MainWindow.xaml`
- `src/FocusCube/MainWindow.xaml.cs`
- `src/FocusCube/FocusCubeSettings.cs`
- `src/FocusCube/SettingsStore.cs`
- `src/FocusCube/CompletionSoundService.cs`
- `src/FocusCube/Assets/widget-shell-reference.png`
- `src/FocusCube/FocusCube.csproj`
- `README.md`
- `PROJECT_HANDOFF.md`
- `AGENTS.md`
- `TESTE_UX01.md`

## 5. Limites conhecidos

- Este ambiente não possui SDK .NET/Windows para compilar WPF; build real depende do GitHub Actions.
- A reprodução customizada depende de codecs disponíveis no Windows Media Foundation; `.wav`, `.mp3` e `.wma` são os formatos expostos pela interface.
- Snap/persistência continuam dependendo de teste real em setups com múltiplos monitores/DPI.

## 6. Workflow

Workflow vigente:

`.github/workflows/windows.yml`

Sequência:

`Restore app → Restore logic tests → Run logic tests → Build app → Publish win-x64 → Verify executable → Upload artifact`

Artifact esperado:

`FocusCube-win-x64`
