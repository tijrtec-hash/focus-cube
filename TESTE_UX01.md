# Teste Focus Cube 1.1.0 — UX-01

Este roteiro valida as correções solicitadas depois da primeira execução funcional da linha 1.0.

## 1. GitHub Actions

1. Abra **Actions → Windows build and publish**.
2. Confirme que a nova execução ficou verde.
3. Baixe **FocusCube-win-x64** e execute `FocusCube.exe`.

**Esperado:** o aplicativo abre normalmente.

## 2. Alinhamento dos botões

Passe o mouse e clique separadamente em:

- `+5`;
- `+10`;
- `+30`;
- `+60`;
- pause/play.

**Esperado:** a área clicável coincide com o botão físico mostrado na arte. Não deve aparecer um quadrado azul deslocado. Cada clique deve acionar somente o botão correspondente.

Teste também reset e `...`.

**Esperado:** as áreas clicáveis ficam centradas sobre seus respectivos glyphs, sem retângulo de seleção visível.

## 3. Pause / play

1. Com o timer rodando, observe o botão central.
2. Clique nele.
3. Clique novamente.

**Esperado:**

- rodando: mostra `Ⅱ`;
- pausado: mostra `▶`;
- o símbolo muda imediatamente junto com o estado real do timer.

## 4. Gaveta

1. Com os controles abertos, clique na seta.
2. Observe todo o recolhimento.
3. Expanda novamente.

**Esperado:** a gaveta física sobe para trás do corpo principal; a velocidade desacelera suavemente perto do final. O corpo superior não deve achatar nem deformar.

## 5. Três toques internos

Abra `... → Toque de fim`.

Teste, usando **Testar toque**:

1. **Suave**;
2. **Digital**;
3. **Sino**.

**Esperado:** os três sons são diferentes e reproduzem sem travar o aplicativo.

## 6. Som personalizado

1. Abra `... → Toque de fim → Personalizado…`.
2. Escolha um arquivo `.wav`, `.mp3` ou `.wma` existente.
3. Use **Testar toque**.
4. Feche e reabra o Focus Cube.
5. Abra novamente `Toque de fim`.

**Esperado:** o arquivo selecionado é reproduzido e a opção personalizada permanece selecionada após reiniciar.

## 7. Conclusão do timer

Defina um tempo curto, por exemplo `00:05`, e aguarde terminar.

**Esperado:** o toque atualmente selecionado é reproduzido no fim, além do alerta visual.

Depois desmarque **Som ao terminar** e repita.

**Esperado:** alerta visual continua, mas nenhum áudio toca.

## Aprovação

Se todos os itens passarem, responda:

`Focus Cube 1.1.0 UX-01 aprovado`

Se algo falhar, informe somente o número da etapa, o comportamento observado e, se útil, uma captura.
