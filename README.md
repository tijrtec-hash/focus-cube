# Focus Cube · 1.0.0 — release candidate

Focus Cube é um timer/widget desktop compacto para Windows, construído em C# + WPF + .NET 8.

O visual aprovado usa uma carcaça híbrida baseada na referência do produto, enquanto tempo, aro, interações e estados são componentes reais do aplicativo.

## Funções

- countdown iniciado automaticamente em `60:00`;
- pause/continue;
- reset para a última duração exata selecionada;
- `+5`, `+10`, `+30`, `+60` minutos;
- presets exatos de 5, 25 e 60 minutos no menu `...`;
- tempo personalizado em minutos, `MM:SS` ou `H:MM:SS`;
- aro proporcional com verde → amarelo → laranja → vermelho;
- estado de conclusão `FIM`, aro vermelho e pulso visual;
- som opcional ao terminar;
- gaveta expansível/recolhível;
- arraste do widget;
- snap nas bordas/cantos;
- posição e preferências persistidas;
- always-on-top configurável;
- saída pelo menu `...`.

## Atalhos quando o widget tem foco

- `Space`: pausar/continuar;
- `R`: reset;
- `E`: expandir/recolher gaveta.

## Preferências locais

Arquivo:

```text
%LOCALAPPDATA%\FocusCube\settings.json
```

Guarda apenas preferências do widget, como posição, gaveta, always-on-top e som. Se o arquivo estiver corrompido, o aplicativo usa valores padrão em vez de impedir a abertura.

## Como compilar e testar — somente navegador

1. Envie/substitua os arquivos desta versão no mesmo repositório GitHub.
2. Confirme a alteração pelo site.
3. Abra **Actions → Windows build and publish**.
4. A execução deve passar por **Run logic tests**, **Build app** e **Publish win-x64**.
5. Se ficar vermelho, abra o primeiro passo que falhou e envie o primeiro erro relevante.
6. Se ficar verde, baixe **FocusCube-win-x64**.
7. Extraia o ZIP.
8. Execute `FocusCube.exe`.
9. Siga `TESTE_FINAL01.md`.

## Referência visual

`docs/reference/widget-expanded-approved.png`

## Estrutura principal

- `src/FocusCube/`: aplicativo WPF;
- `src/FocusCube.Tests/`: testes lógicos sem framework externo;
- `.github/workflows/windows.yml`: build/test/publish no GitHub Actions;
- `AGENTS.md`: regras permanentes;
- `PROJECT_HANDOFF.md`: estado e próxima ação;
- `TESTE_FINAL01.md`: validação manual final.
