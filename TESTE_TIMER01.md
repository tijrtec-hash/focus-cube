# Teste Focus Cube 0.3.0 — TIMER-01

Esta rodada valida o motor de countdown, pause/continue, reset e incrementos rápidos. O visual VIS-01 já foi aprovado e só deve ser observado para regressões.

## 1. Build

1. Envie/substitua os arquivos desta versão no mesmo repositório GitHub.
2. Abra **Actions → Windows build and publish**.
3. Se ficar vermelho, abra **Build and publish** e pare.
4. Envie somente o primeiro erro relevante.
5. Se ficar verde, baixe **FocusCube-win-x64**.
6. Extraia e abra `FocusCube.exe`.

**Esperado:** o aplicativo abre normalmente com o mesmo visual aprovado.

## 2. Countdown inicial

Ao abrir o programa:

**Esperado:**

- o display começa próximo de `60:00`;
- o tempo diminui continuamente;
- após aproximadamente 10 segundos, o valor exibido deve estar aproximadamente 10 segundos menor;
- o aro acompanha o progresso em vez de permanecer estático.

## 3. Pausar e continuar

1. Com o timer rodando, clique no botão central de pause.
2. Aguarde aproximadamente 5 segundos.

**Esperado:** o valor exibido não muda durante a pausa.

3. Clique novamente no mesmo botão.

**Esperado:** a contagem continua do ponto em que parou, sem descontar os segundos em que ficou pausada.

## 4. Incrementos rápidos

Com o timer visível, teste individualmente:

- `+5` adiciona exatamente 5 minutos;
- `+10` adiciona exatamente 10 minutos;
- `+30` adiciona exatamente 30 minutos;
- `+60` adiciona exatamente 60 minutos.

O timer pode estar rodando ou pausado. O estado de pause/run deve ser preservado após o incremento.

**Exemplo:** se estiver em `58:20`, clicar `+10` deve levar o display para aproximadamente `68:20`.

## 5. Reset

1. Adicione alguns minutos.
2. Clique em reset.

**Esperado:** volta para `60:00`, descartando os incrementos da sessão corrente.

Repita uma vez com o timer rodando e uma vez pausado.

**Esperado:**

- se estava rodando, reinicia em `60:00` e continua rodando;
- se estava pausado, volta para `60:00` e continua pausado.

## 6. Progresso básico do aro

Observe o aro durante a contagem e depois de usar um incremento.

**Esperado:**

- comprimento ativo representa a proporção de tempo restante;
- ao adicionar tempo, o aro recalcula a proporção para a nova duração da sessão;
- durante esse teste curto, o aro tende a permanecer verde porque a sessão ainda está longe do fim.

A faixa completa verde→amarelo→laranja→vermelho e as transições visuais serão validadas especificamente em RING-01.

## 7. Regressão visual curta

Confirme que a implementação do timer não alterou:

- tamanho do widget;
- shell/relevo aprovado;
- gaveta e botões;
- transparência ao redor do objeto;
- arraste da janela.

## 8. Resultado

Se tudo passar, responda:

`Focus Cube TIMER-01 aprovado`

Se falhar, informe o número da etapa e descreva o comportamento observado. Captura ou vídeo curto é útil quando a falha envolve atualização do display ou aro.
