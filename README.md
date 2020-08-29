# Cadastro Anúncios

Sistema para cadastro de anúncios Implementado com .net e angular, em arquitetura ddd com cqrs.</br>
</br>
O sistema contem as seguintes funcionalidades: </br>
</br>
Cadastro/Edição dos Anúncios</br>
--> Marca (obrigatório)</br>
--> Modelo (obrigatório)</br>
--> Versão (obrigatório)</br>
--> Ano (obrigatório)</br>
--> Quilometragem (obrigatório)</br>
--> Observações (obrigatório)</br>
</br>
Listagem </br>
--> Colunas: Marca, Modelo, Versão, Ano, Quilometragem, Observações, Opção para Deletar </br>
--> Paginação Server-Side </br>
</br>
Tecnologias utilizadas:</br>
</br>
WEB API C# .net core</br>
Dapper</br>
Angular</br>
MS SQL Server</br>
DDD</br>
CQRS</br>
</br>
* Script para criação da tabela na pasta "Scripts DB" dentro da camada de infra.</br>
* Lembrar de alterar a conection string dentro da camada de api para que a aplicação ache o banco.</br>
* Rodar npm install na pasta ./src/API.Consumption.Test.Api/ClientApp/</br>
* Para startar a aplicação apenas rode a camada de API, ela ira subir tanto a api quanto o frontend em angular 8.</br>
* Rota para Swagger (Documentação Api) '/swagger/index.html'</br>
