# Laqus DevTalks => CQRS

Neste repo temos uma aplicação para computar votos. Esta aplicação pode fazer uso pesado de escrita e leitura de banco de dados devido ao grande volume de acessos simultaneos que pode conter. Ela foi escrita de duas formas:

- Sem CQRS - com o intuito de mostrar possíveis impactos na leitura dos votos computados
- Com CQRS - com o intuito de mostrar como funciona na prática e como o CQRS pode ser uma boa solução para determinados problemas de leitura/escrita
