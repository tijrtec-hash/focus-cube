# Teste Focus Cube 0.2.1 — VIS-01

Esta rodada valida especificamente a redução de escala e a estratégia visual híbrida.

## 1. Build

1. Envie/substitua os arquivos desta versão no mesmo repositório GitHub.
2. Abra **Actions → Windows build and publish**.
3. Se ficar vermelho, abra **Build and publish** e pare.
4. Envie somente o primeiro erro relevante.
5. Se ficar verde, baixe **FocusCube-win-x64**.
6. Extraia e abra `FocusCube.exe`.

**Esperado:** o aplicativo abre normalmente.

## 2. Tamanho

A janela foi reduzida de `260 × 390` para `170 × 251` DIPs.

**Esperado:**

- diferença de tamanho é imediatamente perceptível;
- widget ocupa uma área discreta no desktop;
- ainda é possível ler `52:18` sem esforço normal;
- conjunto se aproxima mais da escala de um widget do que de uma janela de aplicativo.

Envie captura da tela inteira se ainda estiver grande ou pequeno demais.

## 3. Fidelidade do shell

Compare com:

`docs/reference/widget-expanded-approved.png`

**Esperado:**

- relevo da carcaça é muito mais próximo da referência;
- moldura, gaveta, botões, reflexos e gradações de material lembram diretamente a ilustração aprovada;
- gaveta tem a mesma linguagem física do corpo superior;
- não existe retângulo escuro de fundo ao redor do objeto.

## 4. Display dinâmico

**Esperado:**

- display continua preto/recuado;
- `TIMER` e `52:18` aparecem limpos sobre o shell;
- aro tem 12 segmentos;
- com `Progress=0.87`, segmentos ativos são verdes e os restantes escuros;
- chevron aparece na parte inferior do display.

O timer continua estático durante VIS-01.

## 5. Botões

Passe o mouse sobre `+5`, `+10`, `+30`, `+60`, reset, pause e menu.

**Esperado:**

- aparência normal dos botões vem da própria referência;
- hover acrescenta apenas um realce discreto;
- não surgem caixas ou componentes desalinhados sobre a arte.

Os botões ainda não alteram o tempo.

## 6. Resultado

Envie uma captura de tela inteira com o Focus Cube aberto.

Se tamanho e shell estiverem próximos o suficiente da referência, responder:

`Focus Cube VIS-01 aprovado`

Caso contrário, indicar prioritariamente:

1. tamanho;
2. proporções;
3. display/aro/tipografia;
4. qualquer artefato de recorte/transparência.
