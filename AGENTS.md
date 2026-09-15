# AGENTS.md — FOCUS CUBE

## 1. Finalidade

Regras permanentes de trabalho e continuidade do **Focus Cube**, timer/widget desktop para Windows.

Leia este arquivo junto com `PROJECT_HANDOFF.md` antes de alterar o projeto. Instruções atuais do usuário prevalecem sobre o fluxo aqui descrito.

## 2. Fontes e evidências

- Código/configuração: prova o que está escrito, não prova que funciona.
- GitHub Actions: prova build/testes executados sobre a versão enviada ao repositório.
- Confirmação do usuário: prova a validação manual apenas do que ele realmente testou.
- Referência visual aprovada: prova a aparência desejada, não o mecanismo interno.
- `AGENTS.md`: regras permanentes.
- `PROJECT_HANDOFF.md`: estado vigente, bloqueadores e próxima ação.
- `README.md`: uso e compilação pelo GitHub.

Não transformar implementação em validação. Não inventar build, teste, commit, execução do GitHub Actions ou aprovação visual.

## 3. Início e retomada

1. Ler `AGENTS.md` e `PROJECT_HANDOFF.md`.
2. Conferir os arquivos atuais do projeto.
3. Localizar a única seção **Próxima ação exata**.
4. Continuar dali.
5. Pedir ao usuário somente informação que realmente esteja faltando para prosseguir.

Não pedir que o usuário reconte decisões já registradas.

## 4. Produto e decisões atuais

- Produto: **Focus Cube**.
- Plataforma: Windows 10/11.
- Base técnica: C# / WPF / .NET 8.
- Executável esperado: `FocusCube.exe`.
- O aplicativo deve parecer um pequeno objeto digital/hardware premium, não um dashboard.
- O timer é o foco visual principal.
- Display escuro e recuado.
- Corpo e gaveta com relevo, bevel, highlights e sombras sutis.
- Gaveta inferior integrada ao mesmo objeto.
- Botões rápidos: `+5`, `+10`, `+30`, `+60` minutos.
- Esses botões adicionam tempo à sessão atual.
- O aro de progresso deve diminuir com o tempo restante.
- Cor do aro: `verde → amarelo → laranja → vermelho`, ficando vermelho somente perto do fim.
- Estado compacto: corpo + display + aro + controle de expansão.
- Estado expandido: estado compacto + gaveta.
- Estratégia visual híbrida aprovada para VIS-01: a carcaça, gaveta e relevo estático podem usar textura raster derivada da referência aprovada; timer, aro, estados, hit targets e comportamento continuam sendo elementos reais do WPF.
- A referência raster não deve transformar o timer em imagem estática: conteúdo temporal e progresso permanecem dinâmicos e vetoriais.
- Não trocar tecnologia sem decisão explícita do usuário.

## 5. Interação aprovada

- Arrastar área não interativa move o widget.
- Pause/play alterna a sessão.
- Reset restaura a sessão.
- Gaveta abre e fecha com animação.
- Futuro snap deve respeitar bordas/cantos da tela.
- Always-on-top deve ser testado no Windows real.

## 6. Workflow oficial — simples, pelo navegador

O usuário **não quer instalar Git, Visual Studio, SDK ou GitHub Desktop** para operar o projeto.

O fluxo oficial é:

1. A IA prepara os arquivos atualizados.
2. O usuário envia/substitui esses arquivos no **mesmo repositório GitHub pelo navegador**.
3. O usuário confirma a alteração no próprio site do GitHub.
4. O usuário abre **Actions → Windows build and publish**.
5. Se a execução ficar vermelha, envia o primeiro erro relevante.
6. Se ficar verde, baixa o artifact **FocusCube-win-x64**.
7. Extrai o artifact e executa `FocusCube.exe`.
8. Executa o roteiro `TESTE_*.md` correspondente.
9. Envia o resultado/captura para a próxima correção.

Não introduzir Pull Request, branches auxiliares, Git local ou ferramentas adicionais sem uma necessidade concreta aprovada pelo usuário.

GitHub Actions é o caminho escolhido para compilar/publicar o executável de teste. A existência do workflow não prova que ele foi executado.

## 7. Validação

| Estado | Evidência necessária |
| --- | --- |
| Planejado | requisito documentado |
| Implementado no código | arquivos pertinentes inspecionados |
| Verificado estruturalmente | checagem estrutural executada |
| Compilação aprovada | GitHub Actions verde para a versão correspondente |
| Validado visualmente | usuário executou e comparou com a referência |
| Validado manualmente | usuário testou o comportamento e relatou o resultado |
| Entregue para uso | artifact/executável identificado e testável |

Uma etapa não substitui outra. Build verde não comprova fidelidade visual ou interação.

## 8. Checkpoints

- **BASE-01 / 0.1.x:** GitHub Actions + primeiro executável.
- **VIS-01 / 0.2.x:** carcaça, display, gaveta, relevo e fidelidade visual.
- **TIMER-01 / 0.3.x:** countdown, pause/play, reset e incrementos.
- **RING-01 / 0.4.x:** progresso proporcional e cor verde→vermelho.
- **DRAWER-01 / 0.5.x:** expansão/recolhimento e microinterações.
- **DOCK-01 / 0.6.x:** drag refinado, snap e persistência.
- **POLISH-01:** DPI, tipografia, animações e acabamento.

## 9. Documentação

- Manter somente uma seção vigente chamada **Próxima ação exata** no handoff.
- Registrar somente resultados reais.
- Atualizar `AGENTS.md` apenas quando surgir nova regra permanente aprovada.
- Criar/atualizar `TESTE_*.md` quando um checkpoint precisar de teste manual.

## 10. Encerramento

Quando o usuário disser `sessão terminada`, `encerrar sessão`, `gerar handoff` ou equivalente:

1. parar de iniciar recursos novos;
2. revisar o que realmente foi alterado;
3. atualizar `PROJECT_HANDOFF.md`;
4. separar implementado, compilado, testado e validado;
5. registrar a nova **Próxima ação exata**;
6. entregar os arquivos modificados.
