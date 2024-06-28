using System;

namespace JwtAuthApi.Models
{
    public class ClientService
    {
        public int ClienteId { get; set; }
        public int ServicoId { get; set; }
        public decimal PrecoCobrado { get; set; }
        public DateTime DataContratacao { get; set; }
        public string NomeServico { get; set; } // Nome do serviço contratado
        public bool Status { get; set; } // Status do serviço (ativo/inativo)
    }
}