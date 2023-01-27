# C# /.NET Job Test - Código Refeito para funcionar

Refeito o código de teste de uma entrevista, no qual o código tinha:
**- 2 design Patterns misturado, sendo parte do CQRS e parte de TDD
**- Não funcionava, poderia tentar rodar que dava erro.

#### Após Corrigir:
## Desing Patterns: CQRS

##### 1 - Inclusão do SeriLog
Para gerar logs, quando starta o serviço e quando faz qualquer chamada na consulta API

**FALTA 1:** Incluir os logs nas camadas

**FALTA 2:** Incluir o envio destes logs no Elastic Search ou via Filebeat que é uma aplicativo que pega os logs de um path e sobe no elasticSearch

##### 2 - Testes Unitários
**FALTA:** Incluir os Testes Unitários
