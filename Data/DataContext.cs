using Microsoft.EntityFrameworkCore;
using ProEventos.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProEventos.API.Data
{
    public class DataContext : DbContext //CONTEXTO PARA CRIAÇÃO DA BASE NO SQLITE
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { } //CONSTRUTOR DO DATACONTEXT

        public DbSet<Evento> Eventos { get; set; }//TABELA EVENTOS, QUE SERÁ CRIADA NO BANCO DE DADOS A PARTIR DA CRIAÇÃO DA MIGRATION, ONDE RECEBERÁ OS CAMPOS DA CLASSE <EVENTO>, PASSADO POR PARAMETRO
    }
    
}

