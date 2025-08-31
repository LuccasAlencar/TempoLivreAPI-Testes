# Tempo Livre - Sistema de Alertas para Desastres Naturais

<div align="center">
  <img src="https://img.shields.io/badge/.NET-8.0-purple" alt=".NET 8.0">
  <img src="https://img.shields.io/badge/ASP.NET_Core-8.0-blue" alt="ASP.NET Core 8.0">
  <img src="https://img.shields.io/badge/Entity_Framework-8.0-green" alt="Entity Framework 8.0">
  <img src="https://img.shields.io/badge/Oracle-Database-red" alt="Oracle Database">
  <img src="https://img.shields.io/badge/Swagger-OpenAPI-green" alt="Swagger OpenAPI">
  <img src="https://img.shields.io/badge/IoT-Sensors-orange" alt="IoT Sensors">
  <img src="https://img.shields.io/badge/AI-Prediction-lightblue" alt="AI Prediction">
</div>

## Grupo:
#### Daniel da Silva Barros | RM 556152
#### Luccas de Alencar Rufino | RM 558253
<br>

## 📝 Descrição do Projeto

O **Tempo Livre** é uma solução tecnológica inovadora desenvolvida para enfrentar os eventos climáticos extremos que afetam milhões de brasileiros. O projeto combina inteligência artificial, sensores IoT, mapeamento colaborativo e suporte psicológico em uma plataforma integrada que:

**Previsão Inteligente:** utiliza IA para prever desastres com base em dados climáticos históricos e atuais.

**Monitoramento IoT:** sensores em campo detectam mudanças no solo e níveis de água em tempo real.

**Alertas Humanizados:** notificações claras e acessíveis com rotas de evacuação e localização de abrigos.

**Operação Offline:** funcionalidade parcial mesmo sem internet ou energia elétrica.

**Mapeamento Colaborativo:** moradores reportam condições locais enriquecendo os dados da plataforma.

## ⭐ Principais Benefícios

- Prevenção de mortes e redução de perdas materiais
- Interface acessível para população de baixa renda
- Suporte psicológico durante emergências
- Dados em tempo real para gestores públicos
- Operação em áreas com infraestrutura precária

## 🎯 Público-Alvo e Impacto

**Público Primário:** 9 milhões de brasileiros em áreas de risco (favelas, margens de rios, encostas)

**Público Secundário:** Defesas Civis, prefeituras e governos estaduais

**Dados de Impacto:**
- 1.161 eventos hidrológicos registrados em 2023 (CEMADEN)
- Mais de 5.142 mortes por desastres desde 1991
- R$ 27,1 bilhões em custos de desastres (1991-2022)
- 87,6% dos brasileiros possuem smartphone (viabilidade tecnológica)

## ⚙️ Tecnologias e Dependências

- **.NET 8.0** - Framework principal
- **ASP.NET Core 8.0** - API Web
- **Entity Framework Core 8.0** - ORM para acesso a dados
- **Oracle Database** - Banco de dados principal
- **Swagger/OpenAPI** - Documentação da API
- **IoT Sensors** - Monitoramento em campo
- **AI/ML Models** - Previsão de desastres

### Pacotes NuGet Utilizados:
```xml
<PackageReference Include="Microsoft.AspNetCore.OpenApi" Version="8.0.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore" Version="8.0.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="8.0.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="8.0.0" />
<PackageReference Include="Oracle.EntityFrameworkCore" Version="8.21.121" />
<PackageReference Include="Swashbuckle.AspNetCore" Version="6.4.0" />
```

## 🏗️ Arquitetura

O projeto segue uma arquitetura em camadas com:

- **Controllers:** Endpoints REST da API
- **Services:** Lógica de negócios e processamento de alertas
- **Repositories:** Interação com banco de dados e sensores IoT
- **Models:** Entidades e objetos de transferência de dados (DTOs)
- **AI Module:** Modelos de previsão e análise de risco
- **IoT Integration:** Comunicação com sensores em campo
- **Middleware:** Tratamento de exceções e autenticação

## 📝 Entidades Principais

- **Abrigo:** Locais seguros para evacuação da população
- **Alerta:** Notificações de risco iminente ou confirmado
- **LeituraSensor:** Dados coletados pelos sensores IoT em campo
- **LocalizacaoUsuario:** Posicionamento GPS dos usuários cadastrados
- **OcorrenciaColaborativa:** Reportes da comunidade sobre condições locais
- **RotasSeguras:** Caminhos otimizados para evacuação
- **Sensor:** Dispositivos IoT instalados em áreas críticas
- **Usuario:** Moradores e gestores públicos cadastrados

---

## 🚀 Como Executar

**1. Clone o repositório:**
```bash
git clone https://github.com/TEMPO-LIVRE-APP/TempoLivreAPIC.git
cd TempoLivreAPIC/TempoLivreAPI
```

**2. Inicialização completa**
```bash
docker-compose down -v

docker compose up -d --build
```

**3. Acessar a API:**
- Base URL: http://localhost:5000/index.html

**4. Para ver os dados do banco**
Acesse o banco por:

```bash
docker exec -it oracle-db bash
  
sqlplus TEMPOLIVREAPI/TEMPOLIVREAPI12345@//localhost:1521/XE
```

Para visualizar os dados de cada tabela pode dar 

```bash
select * from "nome da tabela";
```

**5. Para uma melhor visualização dos dados**

Pode acessar o Oracle SQL Developer ou pela extensão do oracle no VS Code

Nome de usuário: TEMPOLIVREAPI


Senha: TEMPOLIVREAPI12345


SID: XE


Nome do host: localhost


Porta: 1521


Dai para visualizar as tabelas

```bash
select * from "nome da tabela";
```


## 📖 Documentação Swagger

Após iniciar a aplicação, acesse a documentação interativa:

**URL:** http://localhost:5000/index.html

Também pode dar um ctrl + clique na url do terminal

**Interativo:** explore e teste todos os endpoints disponíveis.

## 📜 Lista Completa de Endpoints REST

Todos os endpoints disponíveis na API:

| Entidades | Método | URL | Descrição |
|-----------|--------|-----|-----------|
| **Abrigos** | GET | /api/abrigos | Listar abrigos (filtros: ?capacidade=...&status=..., paginação) |
| | GET | /api/abrigos/{id} | Buscar abrigo por ID |
| | POST | /api/abrigos | Criar novo abrigo |
| | PUT | /api/abrigos/{id} | Atualizar abrigo existente |
| | DELETE | /api/abrigos/{id} | Deletar abrigo por ID |
| **Alertas** | GET | /api/alertas | Listar alertas (filtros: ?tipo=...&gravidade=..., paginação) |
| | GET | /api/alertas/{id} | Buscar alerta por ID |
| | POST | /api/alertas | Criar novo alerta |
| | PUT | /api/alertas/{id} | Atualizar alerta existente |
| | DELETE | /api/alertas/{id} | Deletar alerta por ID |
| **Leituras Sensores** | GET | /api/leituras | Listar leituras de sensores (filtros/data, paginação) |
| | GET | /api/leituras/{id} | Buscar leitura por ID |
| | POST | /api/leituras | Registrar nova leitura de sensor |
| | PUT | /api/leituras/{id} | Atualizar leituras por ID |
| | DELETE | /api/leituras/{id} | Deletar leituras por ID |
| **Localizações** | GET | /api/localizacoes | Listar localizações de usuários |
| | GET | /api/localizacoes/{id} | Buscar localização por ID |
| | POST | /api/localizacoes | Atualizar localização do usuário |
| | PUT | /api/localizacoes/{id} | Modificar localização existente |
| | DELETE | /api/localizacoes/{id} | Deletar localização |
| **Ocorrências** | GET | /api/ocorrencias | Listar ocorrências colaborativas (filtros, paginação) |
| | GET | /api/ocorrencias/{id} | Buscar ocorrência por ID |
| | POST | /api/ocorrencias | Criar nova ocorrência (reporte da comunidade) |
| | PUT | /api/ocorrencias/{id} | Atualizar ocorrência existente |
| | DELETE | /api/ocorrencias/{id} | Deletar ocorrência por ID |
| **Rotas Seguras** | GET | /api/rotas | Listar rotas de evacuação |
| | GET | /api/rotas/{id} | Buscar rota por ID |
| | POST | /api/rotas | Criar nova rota segura |
| | PUT | /api/rotas/{id} | Atualizar rota existente |
| | DELETE | /api/rotas/{id} | Deletar rota por ID |
| **Sensores** | GET | /api/sensores | Listar sensores IoT (filtros: ?tipo=...&status=...) |
| | GET | /api/sensores/{id} | Buscar sensor por ID |
| | POST | /api/sensores | Cadastrar novo sensor |
| | PUT | /api/sensores/{id} | Atualizar sensor existente |
| | DELETE | /api/sensores/{id} | Deletar sensor por ID |
| **Usuários** | GET | /api/usuarios | Listar usuários (paginação/ordenação) |
| | GET | /api/usuarios/{id} | Buscar usuário por ID |
| | POST | /api/usuarios | Criar novo usuário |
| | PUT | /api/usuarios/{id} | Atualizar usuário existente |
| | DELETE | /api/usuarios/{id} | Deletar usuário por ID |

### Parâmetros de Consulta Comuns:
- ?page=0&size=10&sort=id,desc para paginação/ordenação
- Filtros específicos disponíveis conforme cada endpoint
- Coordenadas geográficas para buscas por proximidade

## 🌟 Funcionalidades Diferenciais

### Inteligência Artificial
- Modelos preditivos para enchentes, deslizamentos e ondas de calor
- Análise de padrões climáticos históricos
- Estimativa de risco por localização geográfica

### Internet das Coisas (IoT)
- Sensores de umidade do solo em encostas
- Medidores de nível de água em rios e córregos
- Estações meteorológicas locais
- Comunicação via LoRaWAN para longas distâncias

### Acessibilidade e Inclusão
- Interface simplificada inspirada em apps de clima
- Mensagens de voz para pessoas com baixo letramento
- Operação offline com mapas pré-carregados
- Suporte psicológico durante emergências

## 📋 Pré-requisitos

- .NET 8.0 SDK
- Acesso ao banco Oracle (oracle.fiap.com.br)
- Credenciais válidas do Oracle
- JetBrains Rider ou Visual Studio (opcional)
- Dispositivos IoT compatíveis (para sensores)

---

**🚨 Salvando vidas através da tecnologia - Projeto Tempo Livre**

Desenvolvido com 💻 e ❤️ para proteger comunidades vulneráveis do Brasil.
