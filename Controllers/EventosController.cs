using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEventos.Application.Contratos;
using ProEventos.Domain;
using System;
using System.Threading.Tasks;

namespace ProEventos.API.Controllers //third-alter git
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {
        private readonly IEventoService _eventoService;

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

        public EventosController(IEventoService eventoService)
        {
            _eventoService = eventoService;
        }

        [HttpGet]

        public async Task<IActionResult> Get() // IEnumerable utilizado para passar um array de eventos
        {
            //IActionResult permite retornar o status do retorno..200,404
            //return "Evento de Get"; - ORIG - Retornando uma mensagem
            //return _evento; // retornando o metodo _evento, onde tem todos os eventos LINHA 16
            //return _context.Eventos; //retornando o contexto evento que vem da database
            try
            {
                var eventos = await _eventoService.GetAllEventosAsync(true);
                if (eventos == null) return NotFound("Nenhum evento encontrado");

                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar eventos. Erro: {ex.Message}"); //$ para concatenar texto com variavel
            }
        }

        [HttpGet("{id}")]
        //public IEnumerable<Evento> GetById(int id) //metodo get passando o id do evento por parametro para filtrar  
        public async Task<IActionResult> GetById(int id) // estava retornando com colchetes, alterado para a classe
        {
            try
            {
                var evento = await _eventoService.GetEventoByIdAsync(id, true);
                if (evento == null) return NotFound("Evento por ID não encontrado.");

                return Ok(evento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
            }
        }

        [HttpGet("{tema}/tema")] //Criação de rota diferente, pois no http não reconhece o tipo de variável que esta sendo passada (string tema ou int idd)
        //public IEnumerable<Evento> GetById(int id) //metodo get passando o id do evento por parametro para filtrar  
        public async Task<IActionResult> GetByTema(string tema) // estava retornando com colchetes, alterado para a classe
        {
            try
            {
                var evento = await _eventoService.GetAllEventosByTemaAsync(tema, true);
                if (evento == null) return NotFound("Eventos por tema não encontrados");

                return Ok(evento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Evento model)
        {
            try
            {
                var evento = await _eventoService.AddEventos(model);
                if (evento == null) return BadRequest("Erro ao tentar adicionar evento");

                return Ok(evento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Evento model)
        {
            try
            {
                var evento = await _eventoService.UpdateEvento(id, model);
                if (evento == null) return BadRequest("Erro ao tentar atualizar evento");

                return Ok(evento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar atualizar eventos. Erro: {ex.Message}");
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                //if (await _eventoService.DeleteEvento(id))
                //    return Ok("Deletado");
                //else
                //    return BadRequest("Evento não deletado");
                        //REFATORAÇÃO DO CODIGO//
                return await _eventoService.DeleteEvento(id) ?
                    Ok("Deletado") : 
                    BadRequest("Evento não deletado");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar deletar evento. Erro: {ex.Message}");
            }

        }
    }
}
