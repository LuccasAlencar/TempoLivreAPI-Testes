using TempoLivreAPI.Models;

namespace TempoLivreAPI.Data
{
    public static class DbSeeder
    {
        public static void Seed(AppDbContext context)
        {
            if (context.Usuarios.Any())
                return;

            // Seed Usuarios
            var usuarios = new List<Usuario>
            {
                new() { Nome = "Alice Silva",  Email = "alice@example.com",  Senha = "senha1", DataCadastro = DateTime.UtcNow },
                new() { Nome = "Bob Souza",    Email = "bob@example.com",    Senha = "senha2", DataCadastro = DateTime.UtcNow },
                new() { Nome = "Carla Mendes",Email = "carla@example.com",  Senha = "senha3", DataCadastro = DateTime.UtcNow },
                new() { Nome = "Daniel Oliveira", Email = "daniel@example.com", Senha = "senha4", DataCadastro = DateTime.UtcNow },
                new() { Nome = "Eva Pereira",  Email = "eva@example.com",    Senha = "senha5", DataCadastro = DateTime.UtcNow }
            };
            context.Usuarios.AddRange(usuarios);
            context.SaveChanges();

            // Seed LocalizacaoUsuario
            var locs = usuarios.Select((u, i) => new LocalizacaoUsuario
            {
                UsuarioId      = u.Id,
                Latitude       = -23.0 - i,
                Longitude      = -46.0 - i,
                DataHoraRegistro = DateTime.UtcNow
            }).ToList();
            context.Localizacoes.AddRange(locs);
            context.SaveChanges();

            // Seed Abrigos
            var abrigos = new List<Abrigo>
            {
                new() { Nome = "Abrigo Centro", Endereco = "Rua A, 123", Latitude = -23.5, Longitude = -46.5, CapacidadeMax = 100, DisponibilidadeAtual = 50, Contato = "(11) 1111-1111" },
                new() { Nome = "Abrigo Leste",  Endereco = "Rua B, 456", Latitude = -23.6, Longitude = -46.6, CapacidadeMax = 80,  DisponibilidadeAtual = 20, Contato = "(11) 2222-2222" },
                new() { Nome = "Abrigo Oeste",  Endereco = "Rua C, 789", Latitude = -23.7, Longitude = -46.7, CapacidadeMax = 120, DisponibilidadeAtual = 60, Contato = "(11) 3333-3333" },
                new() { Nome = "Abrigo Norte",  Endereco = "Rua D, 321", Latitude = -23.8, Longitude = -46.8, CapacidadeMax = 90,  DisponibilidadeAtual = 30, Contato = "(11) 4444-4444" },
                new() { Nome = "Abrigo Sul",    Endereco = "Rua E, 654", Latitude = -23.9, Longitude = -46.9, CapacidadeMax = 110, DisponibilidadeAtual = 70, Contato = "(11) 5555-5555" }
            };
            context.Abrigos.AddRange(abrigos);
            context.SaveChanges();

            // Seed RotasSeguras
            var rotas = usuarios.Select((u, i) => new RotasSeguras
            {
                UsuarioId        = u.Id,
                AbrigoDestinoId  = abrigos[i % abrigos.Count].Id,
                OrigemLatitude   = -23.0 - i,
                OrigemLongitude  = -46.0 - i,
                DestinoLatitude  = abrigos[i % abrigos.Count].Latitude,
                DestinoLongitude = abrigos[i % abrigos.Count].Longitude,
                TipoRota         = i % 2 == 0 ? "Pedestre" : "Veicular"
            }).ToList();
            context.RotasSeguras.AddRange(rotas);
            context.SaveChanges();

            // Seed Sensor
            var sensors = new List<Sensor>
            {
                new() { TipoSensor = "Temperatura", LocalizacaoLat = -23.5, LocalizacaoLong = -46.5, Status = "ativo",       DataInstalacao = DateTime.UtcNow.AddDays(-10) },
                new() { TipoSensor = "Umidade",     LocalizacaoLat = -23.6, LocalizacaoLong = -46.6, Status = "ativo",       DataInstalacao = DateTime.UtcNow.AddDays(-8)  },
                new() { TipoSensor = "Movimento",   LocalizacaoLat = -23.7, LocalizacaoLong = -46.7, Status = "inativo",     DataInstalacao = DateTime.UtcNow.AddDays(-5)  },
                new() { TipoSensor = "Fumaça",      LocalizacaoLat = -23.8, LocalizacaoLong = -46.8, Status = "manutenção", DataInstalacao = DateTime.UtcNow.AddDays(-3)  },
                new() { TipoSensor = "Luminosidade",LocalizacaoLat = -23.9, LocalizacaoLong = -46.9, Status = "ativo",       DataInstalacao = DateTime.UtcNow.AddDays(-1)  }
            };
            context.Sensors.AddRange(sensors);
            context.SaveChanges();

            // Seed LeituraSensor
            var leituras = sensors.Select((s, i) => new LeituraSensor
            {
                SensorId    = s.Id,
                ValorLido   = 10 + i * 5,
                DataHora    = DateTime.UtcNow,
                UnidadeMedida = i % 2 == 0 ? "C°" : "%"
            }).ToList();
            context.Leituras.AddRange(leituras);
            context.SaveChanges();

            // Seed Alerta
            var alertas = usuarios.Select((u, i) => new Alerta
            {
                UsuarioId   = u.Id,
                TipoEvento  = i % 2 == 0 ? "Incêndio" : "Alagamento",
                NivelAlerta = i % 3 == 0 ? "perigo" : "atenção",
                Mensagem    = $"Alerta {i}",
                Latitude    = -23.0 - i,
                Longitude   = -46.0 - i,
                DataEmissao = DateTime.UtcNow,
                Status      = "ativo"
            }).ToList();
            context.Alertas.AddRange(alertas);
            context.SaveChanges();

            // Seed OcorrenciaColaborativa
            var ocorrencias = usuarios.Select((u, i) => new OcorrenciaColaborativa
            {
                UsuarioId      = u.Id,
                TipoOcorrencia = i % 2 == 0 ? "Roubo" : "Acidente",
                Descricao      = $"Descrição {i}",
                Latitude       = -23.0 - i,
                Longitude      = -46.0 - i,
                DataEnvio      = DateTime.UtcNow,
                Status         = "pendente"
            }).ToList();
            context.Ocorrencias.AddRange(ocorrencias);
            context.SaveChanges();
        }
    }
}
