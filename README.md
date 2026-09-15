# Focus Cube · 1.1.0 — UX-01

Focus Cube é um timer/widget desktop compacto para Windows, construído em C# + WPF + .NET 8.

O visual aprovado usa uma carcaça híbrida baseada na referência do produto, enquanto tempo, aro, hit targets, interações e estados são componentes reais do aplicativo.

## Funções

- countdown iniciado automaticamente em `60:00`;
- pause/continue com símbolo dinâmico: `Ⅱ` enquanto roda e `▶` quando pausado;
- reset para a última duração exata selecionada;
- `+5`, `+10`, `+30`, `+60` minutos;
- presets exatos de 5, 25 e 60 minutos no menu `...`;
- tempo personalizado em minutos, `MM:SS` ou `H:MM:SS`;
- aro proporcional com verde → amarelo → laranja → vermelho;
- estado de conclusão `FIM`, aro vermelho e pulso visual;
- três toques internos de conclusão: **Suave**, **Digital** e **Sino**;
- som personalizado por arquivo `.wav`, `.mp3` ou `.wma`;
- opção para testar o toque escolhido no menu;
- gaveta expansível/recolhível com movimento físico para cima e desaceleração suave no fim;
- hit targets dos quatro botões rápidos e pause alinhados às dimensões reais da arte aprovada;
- arraste do widget;
- snap nas bordas/cantos;
- posição e preferências persistidas;
- always-on-top configurável;
- saída pelo menu `...`.

## Som de conclusão

Abra `... → Toque de fim` e escolha:

- **Suave**;
- **Digital**;
- **Sino**;
- **Personalizado…** para selecionar um arquivo local.

`Testar toque` reproduz a seleção atual sem precisar aguardar o timer terminar.

A escolha e o caminho do arquivo personalizado ficam registrados em:

```text
%LOCALAPPDATA%\FocusCube\settings.json
```

Se um arquivo personalizado deixar de existir, o programa usa o toque **Suave** como fallback.

## Atalhos quando o widget tem foco

- `Space`: pausar/continuar;
- `R`: reset;
- `E`: expandir/recolher gaveta.

## Preferências locais

Arquivo:

```text
%LOCALAPPDATA%\FocusCube\settings.json
```

Guarda posição, estado da gaveta, always-on-top e preferências de som. Se o arquivo estiver corrompido, o aplicativo usa valores padrão em vez de impedir a abertura.

## Como compilar e testar — somente navegador

1. Envie/substitua os arquivos desta versão no mesmo repositório GitHub.
2. Confirme a alteração pelo site.
3. Abra **Actions → Windows build and publish**.
4. A execução deve passar por **Run logic tests**, **Build app** e **Publish win-x64**.
5. Se ficar vermelho, envie o primeiro erro relevante do novo build.
6. Se ficar verde, baixe **FocusCube-win-x64**.
7. Extraia o ZIP.
8. Execute `FocusCube.exe`.
9. Siga `TESTE_UX01.md`.

## Referência visual

`docs/reference/widget-expanded-approved.png`

## Estrutura principal

- `src/FocusCube/`: aplicativo WPF;
- `src/FocusCube.Tests/`: testes lógicos sem framework externo;
- `.github/workflows/windows.yml`: build/test/publish no GitHub Actions;
- `AGENTS.md`: regras permanentes;
- `PROJECT_HANDOFF.md`: estado e próxima ação;
- `TESTE_UX01.md`: validação manual desta revisão.
