# PROJECT_HANDOFF.md — FOCUS CUBE

**Atualização:** 15/09/2026  
**Checkpoint:** VIS-01 / fidelidade híbrida 0.2.1  
**Versão de trabalho:** 0.2.1  
**Validação existente:** 0.1.0 e 0.2.0 foram compiladas/executadas no Windows. O usuário rejeitou ambas visualmente; na 0.2.0 o tamanho ainda foi considerado excessivo e o material continuou distante da referência.  
**Etapa atual:** compilar 0.2.1 e validar a nova estratégia híbrida baseada diretamente na referência aprovada.

## 1. Estado vigente

O Focus Cube é um timer/widget desktop para Windows em C# + WPF + .NET 8.

A captura real da 0.2.0 mostrou que a redução anterior foi insuficiente e que a reconstrução puramente vetorial ainda não reproduzia com fidelidade o relevo/material da ilustração. A 0.2.1 muda a estratégia visual de VIS-01:

- tamanho padrão reduzido de `260 × 390` para `170 × 251` DIPs;
- toda a composição passa a usar uma superfície de design `1388 × 2048`, exatamente na proporção da referência;
- `Viewbox` reduz essa superfície para o tamanho do widget sem alterar proporções internas;
- carcaça, gaveta, botões em repouso, reflexos e relevo usam a própria referência aprovada como textura estática;
- o fundo externo da referência é recortado por geometria, preservando transparência ao redor do widget;
- o display original da referência é coberto por uma superfície escura limpa;
- `TIMER`, `52:18`, aro e chevron são redesenhados por WPF sobre essa superfície;
- aro continua vetorial, segmentado em 12 partes e preparado para `verde → amarelo → laranja → vermelho`;
- os botões da gaveta possuem hit targets reais e transparentes alinhados sobre a arte de referência;
- a interface continua funcionalmente preparada para receber countdown e interações reais sem depender de texto/ring rasterizados.

Ainda não implementado:

- countdown real;
- pause/reset/incrementos funcionais;
- ligação do aro ao timer real;
- expansão/recolhimento;
- snap/persistência.

Referência aprovada:

`docs/reference/widget-expanded-approved.png`

Asset usado pelo executável para o shell:

`src/FocusCube/Assets/widget-shell-reference.png`

## 2. Próxima ação exata

1. Enviar/substituir os arquivos da revisão 0.2.1 no mesmo repositório GitHub pelo navegador.
2. Confirmar a alteração no site.
3. Abrir **Actions → Windows build and publish**.
4. Se vermelho, enviar somente o primeiro erro relevante da nova execução.
5. Se verde, baixar **FocusCube-win-x64**.
6. Extrair e abrir `FocusCube.exe`.
7. Enviar uma captura de tela inteira com o widget aberto.
8. Validar primeiro **tamanho** e **fidelidade do shell**; somente depois ajustar tipografia/aro em detalhes ou iniciar TIMER-01.

## 3. Regras do workflow atual

- Todo o processo operacional do usuário ocorre pelo navegador.
- Não exigir Git local, GitHub Desktop, Visual Studio ou SDK .NET.
- Não introduzir Pull Request ou branches auxiliares sem necessidade concreta.
- GitHub Actions é o compilador do projeto para os testes do usuário.
- Workflow verde prova compilação/publicação daquela versão; não prova fidelidade visual.
- A aprovação visual depende do teste do usuário.

## 4. Decisões visuais vigentes

- widget pequeno e discreto no desktop;
- timer grande e dominante;
- shell deve se aproximar diretamente da referência aprovada;
- estratégia híbrida: material/relevo estático pode vir da referência raster, enquanto conteúdo dinâmico permanece WPF;
- display preto/recuado;
- gaveta inferior integrada e mais estreita que o corpo;
- botões `+5`, `+10`, `+30`, `+60` preservam o relevo da referência;
- aro segmentado e funcional ao redor do timer;
- cor do aro: `verde → amarelo → laranja → vermelho` conforme o tempo acaba;
- reset e menu secundários; pause é o controle principal;
- sem dashboard permanente.

## 5. Validação disponível

Comprovado pelo usuário:

- `FocusCube.exe` foi executado no Windows;
- GitHub Actions conseguiu produzir versão executável após correção do restore `win-x64`;
- janela transparente/always-on-top foi renderizada;
- 0.1.0 foi rejeitada por tamanho e distância visual;
- 0.2.0 ficou menor, mas ainda foi rejeitada por tamanho e fidelidade insuficientes.

A 0.2.1 foi inspecionada estruturalmente neste ambiente, mas ainda não foi compilada nem vista no Windows.

## 6. Limites atuais

- a carcaça híbrida prioriza fidelidade à referência em detrimento de permitir recoloração completa do material sem novos assets;
- tipografia do Windows pode diferir levemente da tipografia da imagem conceitual;
- `Progress=0.87` continua estático durante VIS-01;
- o tamanho em DIPs ainda depende de DPI/escala do Windows, portanto a captura real continua sendo a evidência decisiva.

## 7. Histórico resumido

### BASE-01 / 0.1.0

- primeiro shell WPF e GitHub Actions;
- executável validado no Windows;
- VIS-01 rejeitado.

### VIS-01 / 0.2.0

- redução para `260 × 390`;
- refinamento puramente vetorial de material/aro/gaveta;
- compilado e executado;
- ainda rejeitado por tamanho e distância visual.

### VIS-01 / 0.2.1

- redução para `170 × 251`;
- adoção de superfície de design baseada nas coordenadas exatas da referência;
- shell híbrido raster + display/timer/aro WPF real;
- hit targets transparentes preservam os botões funcionais sem sacrificar o acabamento visual.
