# Teste Focus Cube 0.1.0 — BASE-01 / VIS-01

Este roteiro valida o primeiro executável e o shell visual atual.

## 1. Build

1. Envie esta versão para o mesmo repositório GitHub.
2. Abra **Actions → Windows build and publish**.
3. Se ficar vermelho, abra **Build and publish** e pare.
4. Envie o primeiro erro relevante.
5. Se ficar verde, baixe **FocusCube-win-x64**.
6. Extraia o arquivo baixado.
7. Abra `FocusCube.exe`.

**Esperado:** o aplicativo abre e mostra o widget.

## 2. Aparência geral

Compare com:

`docs/reference/widget-expanded-approved.png`

**Esperado:**

- timer é o elemento mais chamativo;
- corpo parece um objeto compacto, não uma janela comum;
- display parece recuado;
- gaveta parece parte do mesmo objeto;
- proporções lembram claramente a referência.

## 3. Timer e display

Observe `52:18` e `TIMER`.

**Esperado:**

- `52:18` grande e legível;
- texto nítido e sem cortes;
- timer centralizado dentro do aro;
- display escuro e recuado.

O número ainda é estático nesta versão.

## 4. Aro

**Esperado:**

- trilha circular nítida;
- arco verde contínuo;
- sem deformação aparente;
- ainda estático.

A transição verde→vermelho será implementada depois.

## 5. Relevo e material

**Esperado:**

- superfícies altas e recuadas são distinguíveis;
- highlights e sombras são sutis;
- display parece encaixado;
- gaveta parece uma peça do mesmo produto.

## 6. Botões

Passe o mouse em `+5`, `+10`, `+30`, `+60`.

**Esperado:** glow/borda luminosa discreta.

Pressione um botão por um instante.

**Esperado:** sensação visual de botão pressionado.

Os botões ainda não alteram o tempo nesta versão.

## 7. Arraste

Arraste uma área da carcaça que não seja botão.

**Esperado:** o widget acompanha o mouse.

Tente arrastar clicando diretamente em um botão.

**Esperado:** o botão não arrasta a janela.

## 8. Always-on-top

Abra outra janela por trás do Focus Cube.

**Esperado:** o Focus Cube continua acima da janela comum.

## 9. Resultado

Se tudo estiver aceitável, responda:

`Focus Cube VIS-01 base aprovada`

Se houver problema, informe o número da etapa e envie uma captura do executável.
