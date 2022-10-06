using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEventos.API.Data;
using ProEventos.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProEventos.API.Controllers //third-alter git
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {
        /*public IEnumerable<Evento> _evento = new Evento[]{
                new Evento(){ //criando objeto do evento, passando os parametros inseridos na classe evento
                    EventoId = 1,
                    Tema = "Angular 11 e .NET 5",
                    Local = "Ribeirão Preto",
                    Lote = "1ª Lote",
                    QtdPessoa = 250,
                    DataEvento = DateTime.Now.AddDays(2).ToString("dd/MM/yyyy"),
                    ImagemURL = "foto1.png"
                },
                new Evento(){
                    EventoId = 2,
                    Tema = "Angular e suas novidades",
                    Local = "Ribeirão Preto",
                    Lote = "2ª Lote",
                    QtdPessoa = 350,
                    DataEvento = DateTime.Now.AddDays(3).ToString("dd/MM/yyyy"),
                    ImagemURL = "foto2.png"
                }
            };*/ // REMOVIDO PARA PEGAR AS INFORMAÇÕES DO BANCO DE DADOS AO INVES DE POPULAR O OBJETO EVENTO COM AS INFORMAÇOES CHUMBADAS

        private readonly DataContext _context;

        public EventosController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]

        //public string Get() - ORIG
        //public Evento Get() - alterado pra retornar a classe evento
        public IEnumerable <Evento> Get() // IEnumerable utilizado para passar um array de eventos
        {
            //return "Evento de Get"; - ORIG - Retornando uma mensagem
            //return _evento; // retornando o metodo _evento, onde tem todos os eventos LINHA 16
            return _context.Eventos; //retornando o contexto evento que vem da database
        }

        [HttpGet("{id}")]
        //public IEnumerable<Evento> GetById(int id) //metodo get passando o id do evento por parametro para filtrar  
        public Evento GetById(int id) // estava retornando com colchetes, alterado para a classe
        {
            //return _context.Eventos.Where(evento => evento.EventoId == id); lambda expression (utilizado para funções executadas somente uma vez, representada pela seta =>
            return _context.Eventos.FirstOrDefault( //melhor visualização do return teste
                evento => evento.EventoId == id);
        }

        [HttpPost]
        public string Post()
        {
            return "Exemplo de Post";

        }

        [HttpPut("{id}")]
        public string Put(int id)
        {
            return $"Exemplo de Put com id = {id}";

        }

        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            return $"Exemplo de Delete com id = {id}";

        }
    }
}
