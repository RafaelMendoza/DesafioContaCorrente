# DesafioContaCorrente

**RealizarDeposito**  
Endpoint: POST - api/ContaBancaria/RealizarDeposito  
Espera Receber:[FromQuery] ContaId (Int), [FromBody] Deposito (DepositoViewModel)  
Retorna: Uma mensagem de sucesso ou erro em formato de String dentro de um HTTP Response com Status Code equivalente.  

**RealizarSaque**  
Endpoint: POST - api/ContaBancaria/RealizarSaque  
Espera Receber:[FromQuery] ContaId (Int), [FromBody] Saque (SaqueViewModel)  
Retorna: Uma mensagem de sucesso ou erro em formato de String dentro de um HTTP Response com Status Code equivalente.  

**RealizarDeposito**  
Endpoint: GET - api/ContaBancaria/RealizarDeposito  
Espera Receber: ContaId (INT), DataInicio (DateTime), DataFim (DateTime)
Retorna: Uma mensagem de sucesso ou erro em formato de String dentro de um HTTP Response com Status Code equivalente.  
