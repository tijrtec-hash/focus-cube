# PROJECT_HANDOFF.md — FOCUS CUBE

**Atualização:** 15/09/2026  
**Checkpoint:** BASE-01 / preparação para VIS-01  
**Versão de trabalho:** 0.1.0  
**Validação existente:** estrutura XAML/XML checada; build no GitHub e validação visual ainda pendentes.  
**Etapa atual:** enviar a versão atual ao mesmo repositório, compilar no GitHub Actions e testar o primeiro `FocusCube.exe`.

## 1. Estado vigente

O Focus Cube é um timer/widget desktop para Windows em C# + WPF + .NET 8.

Já existe no código:

- janela sem borda;
- transparência ao redor do widget;
- `Topmost="True"`;
- corpo grafite com sombras/relevos;
- display escuro/recuado;
- timer estático `52:18`;
- aro vetorial estático;
- gaveta inferior;
- botões `+5/+10/+30/+60` com estados visuais;
- botões visuais de reset, pause e menu;
- arraste básico em áreas não interativas.

Ainda não implementado:

- countdown real;
- pause/reset/incrementos funcionais;
- cor dinâmica verde→vermelho;
- expansão/recolhimento da gaveta;
- snap/persistência.

A referência visual aprovada permanece em:

`docs/reference/widget-expanded-approved.png`

## 2. Próxima ação exata

1. Enviar/substituir estes arquivos no **mesmo repositório GitHub pelo navegador**.
2. Confirmar a alteração no site do GitHub.
3. Abrir **Actions → Windows build and publish**.
4. Se ficar vermelho, abrir **Build and publish** e enviar o primeiro erro relevante.
5. Se ficar verde, baixar **FocusCube-win-x64**.
6. Extrair o artifact e abrir `FocusCube.exe`.
7. Executar `TESTE_VIS01.md` e comparar com a referência visual.
8. Enviar captura/relato do resultado para o próximo ajuste.

## 3. Regras do workflow atual

- Todo o processo operacional do usuário ocorre pelo navegador.
- Não exigir Git local, GitHub Desktop, Visual Studio ou SDK .NET.
- Não introduzir Pull Request ou branches auxiliares sem necessidade concreta.
- GitHub Actions é o compilador do projeto para os testes do usuário.
- Workflow verde prova compilação/publicação daquela versão; não prova fidelidade visual.
- A aprovação visual depende do teste do usuário.

## 4. Decisões visuais vigentes

- timer grande e dominante;
- display preto/recuado;
- carcaça grafite/cinza com relevos sutis;
- gaveta inferior integrada;
- botões luminosos `+5`, `+10`, `+30`, `+60`;
- aro funcional ao redor do timer;
- cor futura do aro: `verde → amarelo → laranja → vermelho` conforme o tempo acaba;
- sem dashboard permanente;
- interface principal construída com elementos reais do WPF.

## 5. Validação disponível

No ambiente de preparação não foi executado WPF/Windows nem build .NET real.

Já verificado:

- estrutura XML/XAML válida;
- arquivos do workflow presentes.

Ainda não comprovado:

- restore/build/publicação no GitHub;
- criação real de `FocusCube.exe`;
- aparência no Windows;
- hover/pressed em execução;
- drag em execução;
- always-on-top em execução.

## 6. Limites atuais

- tipografia pode diferir da referência;
- WPF com transparência/sombras precisa de teste real no Windows;
- `ProgressRing` ainda não está ligado a timer real;
- botões da gaveta ainda são apenas visuais.

## 7. Histórico resumido

### BASE-01

- documentação de continuidade criada;
- projeto WPF criado;
- shell visual inicial implementado;
- GitHub Actions preparado;
- `TESTE_VIS01.md` criado;
- workflow simplificado para o mesmo modelo operacional do HotDeck: enviar arquivos → Actions → artifact → teste.
