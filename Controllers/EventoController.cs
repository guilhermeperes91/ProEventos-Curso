using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEventos.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {
        public IEnumerable<Evento> _evento = new Evento[]{
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
            };
        public EventoController()
        {
        }

        [HttpGet]

        //public string Get() - ORIG
        //public Evento Get() - alterado pra retornar a classe evento
        public IEnumerable <Evento> Get() // IEnumerable utilizado para passar um array de eventos
        {
          //return "Evento de Get"; - ORIG - Retornando uma mensagem
            return _evento; // retornando o metodo _evento, onde tem todos os eventos
        }

        [HttpGet("{id}")]
        public IEnumerable<Evento> GetById(int id) //metodo get passando o id do evento por parametro para filtrar 
        {
            return _evento.Where(evento => evento.EventoId == id); //lambda expression (utilizado para funções executadas somente uma vez, representada pela seta =>
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
