# Teste final Focus Cube 1.0.0

Este roteiro valida a release candidate completa. Faça os testes no executável baixado do artifact **FocusCube-win-x64** da mesma execução do GitHub Actions.

## 1. GitHub Actions

1. Abra **Actions → Windows build and publish**.
2. Confirme que a execução terminou verde.
3. Confirme que ficaram verdes:
   - **Run logic tests**;
   - **Build app**;
   - **Publish win-x64**;
   - **Verify executable**.
4. Baixe **FocusCube-win-x64** e execute `FocusCube.exe`.

**Esperado:** aplicativo abre sem instalação adicional.

## 2. Regressão visual

Compare com `docs/reference/widget-expanded-approved.png`.

**Esperado:** shell, escala, relevo, display e gaveta permanecem visualmente iguais à versão VIS-01 aprovada.

## 3. Countdown e aro

1. Ao abrir, observe `60:00` começar a diminuir.
2. Observe o aro ao longo da sessão ou use tempo personalizado curto para acelerar o teste.

**Esperado:** o aro diminui proporcionalmente e caminha de verde para amarelo, laranja e vermelho conforme o tempo restante cai.

## 4. Pausa, continuação e reset

1. Clique no botão central.
2. Aguarde alguns segundos.
3. Clique novamente.
4. Clique em reset.

**Esperado:** pausa congela o tempo; continuar retoma sem saltos; reset volta à duração exata selecionada.

## 5. Incrementos rápidos

1. Com o timer rodando, clique `+5`, `+10`, `+30` e `+60` separadamente.

**Esperado:** cada botão soma o valor ao tempo restante. O aro recalcula o progresso sem quebrar o visual.

## 6. Tempo exato e personalizado

1. Abra `...`.
2. Escolha `25 min`.
3. Confirme que inicia em `25:00`.
4. Abra `... → Definir tempo…`.
5. Digite `0:10` e inicie.

**Esperado:** aceita o tempo e inicia em `00:10`.

## 7. Conclusão

Deixe o teste de `00:10` chegar a zero.

**Esperado:** mostra `00:00`, título `FIM`, aro vermelho, pulso visual e som se **Som ao terminar** estiver marcado.

Depois desmarque **Som ao terminar**, repita com outro tempo curto e confirme que há alerta visual sem som.

## 8. Gaveta

1. Clique na seta abaixo do display.
2. Clique novamente.

**Esperado:** gaveta recolhe/expande suavemente; o corpo superior não é comprimido nem deformado.

Se o widget estiver encostado na parte inferior da tela, a borda inferior deve permanecer alinhada ao expandir/recolher.

## 9. Arraste, snap e persistência

1. Arraste o widget para perto de um canto.
2. Solte.
3. Feche pelo menu `... → Sair`.
4. Abra `FocusCube.exe` novamente.

**Esperado:** encaixa na borda/canto e reabre aproximadamente na posição salva.

## 10. Preferências

1. No menu `...`, desligue **Sempre no topo**.
2. Feche e abra novamente.
3. Verifique o estado.
4. Ligue novamente.

**Esperado:** preferência persiste entre execuções. O mesmo vale para **Som ao terminar** e estado expandido/recolhido da gaveta.

## 11. Atalhos locais

Com o widget focado:

- `Space`: pausa/continua;
- `R`: reset;
- `E`: expande/recolhe.

**Esperado:** os três atalhos funcionam sem fechar o aplicativo.

## Aprovação

Se tudo acima passar, responda:

`Focus Cube 1.0 aprovado`

Se algo falhar, informe apenas o número da etapa, o que aconteceu e, se possível, uma captura curta.
