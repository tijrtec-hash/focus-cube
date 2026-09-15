# Teste Focus Cube 0.2.0 — VIS-01

Este roteiro valida a segunda versão visual após a captura real da 0.1.0.

## 1. Build

1. Envie/substitua os arquivos desta versão no mesmo repositório GitHub.
2. Abra **Actions → Windows build and publish**.
3. Se ficar vermelho, abra **Build and publish** e pare.
4. Envie somente o primeiro erro relevante da nova execução.
5. Se ficar verde, baixe **FocusCube-win-x64**.
6. Extraia o arquivo baixado.
7. Abra `FocusCube.exe`.

**Esperado:** o aplicativo abre e mostra o widget.

## 2. Tamanho no desktop

Compare com a versão anterior.

**Esperado:**

- o widget está claramente menor;
- ocupa uma fração discreta da tela;
- ainda é legível sem parecer uma janela grande;
- a proporção geral é mais vertical, próxima da referência aprovada.

Se ainda estiver grande, envie uma captura de tela inteira, sem recortar.

## 3. Aparência geral

Compare lado a lado com:

`docs/reference/widget-expanded-approved.png`

**Esperado:**

- carcaça com aparência mais clara/metálica;
- display ocupa mais da face superior;
- menos molduras pesadas entre carcaça e display;
- gaveta parece sair de baixo do corpo;
- conjunto lembra mais o objeto da referência e menos um painel genérico.

## 4. Timer e aro

Observe `52:18`, `TIMER` e o círculo.

**Esperado:**

- `52:18` domina visualmente o display;
- `TIMER` é secundário e discreto;
- aro é espesso e dividido em segmentos;
- com `Progress=0.87`, a cor atual é verde;
- partes inativas aparecem em cinza escuro;
- chevron fica dentro da parte inferior do display.

O tempo ainda é estático nesta versão.

## 5. Gaveta e botões

**Esperado:**

- gaveta é um pouco mais estreita que o corpo superior;
- `+5`, `+10`, `+30`, `+60` parecem botões físicos elevados;
- hover produz glow discreto;
- pause é o controle elevado principal;
- reset e `•••` são mais discretos, sem três caixas idênticas.

Os controles ainda não alteram o timer.

## 6. Relevo e material

Observe bordas superiores, sombras e superfícies.

**Esperado:**

- corpo tem highlight superior e sombra inferior;
- display parece recuado;
- botões parecem elevados;
- gaveta tem volume próprio sem parecer separada do corpo.

## 7. Arraste e always-on-top

Arraste uma área não interativa.

**Esperado:** o widget acompanha o mouse.

Abra outra janela por trás.

**Esperado:** o Focus Cube permanece acima da janela comum.

## 8. Resultado

Envie uma captura de tela inteira com o Focus Cube aberto.

Se o visual estiver próximo o suficiente para avançar, responda:

`Focus Cube VIS-01 aprovado`

Caso contrário, descreva somente as diferenças visuais mais importantes percebidas.
