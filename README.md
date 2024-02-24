# envio-de-ordem

Repositório para ilustrar a comunicação do protocolo FIX com a B3 usando [QuickFix](https://new.quickfixn.org/) como client e [MiniFix](https://elato.se/minifix/doc.html).

# Dependencias

Essa implementação usa:

- `ASP .NET CORE 6.0`
- `Quick Fix\n 4.4`
- `Quick Fix core`

# Como instalar:

Você vai precisar instalar o core e a versão do quickfix só usar os comandos abaixo:
```c#
dotnet add package QuickFIXn.Core --version 1.11.2
dotnet add package QuickFIXn.FIX4.4 --version 1.11.2
```

# Como configurar o client?

O OmsSample já vem com um arquivo `.cfg` e `xml` configurado para você. Você apenas precisar renomear o arquivo `.cfg` para `quickfix.cfg` e editar as variaveis:
- `DataDictionary` Para o caminho do .xml (caminho absoluto)
- `FilelogPath e FileStorePath` Para o caminho que você quiser.

Caso você obtenha erros em relação a algum arquivo, opte por usar o caminho absoluto.


# Como configurar o servidor?

No seu servidor `MiniFix` vá na aba "Transactions" e clique em Execution Report, os campos devem estar nessa ordem (Da esquerda pra direita, Tag - Value):
- 35 - 8
- 37 - $UNIQUE
- 453 - 0
- 17 - $UNIQUE
- 150 - 0
- 39 - 0
- 54 - 1
- 38 - 10
- 55 - `Nome que você colocou no campo symbol`
- 14 - 0
- 6 - 0

Na aba Session desabilite `Auto-Logon` e `Auto-respond to test requests`, e tudo deve funcionar. 


# Como enviar uma mensagem?

O OmsSample envia uma NewOrderSingle automaticamente, você só precisa preencher os 3 campos: `OrderSymbol: string`,  `Price: decimal` e `OrderAmount: uint`. Se ocorrer tudo certo, você deve ver uma mensagem de Logon e uma newOrderSingle no seu MiniFix e retornar uma ExecutionReport configurada. Assim, será salvo no seu banco de dados, caso tudo ocorra certo.

Caso tenha problema com o arquivo .cfg, vá no diretório `Controllers` e abra o arquivo `.cs` na linha 21 e edite o `"quickfix.cfg` pelo caminho absoluto. 

Então após isso, você pode dar um `dotnet run` no seu projeto e tudo funcionar normalmente.


# Como rodar o docker?

Apenas vá na pasta onde se encontra o docker compose e rode:
```docker
docker-compose up
```

Ele irá rodar o codigo automaticamente e criar um banco de dados postgresql via docker.