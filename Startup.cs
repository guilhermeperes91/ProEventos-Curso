using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ProEventos.Application;
using ProEventos.Application.Contratos;
using ProEventos.Persistence;
using ProEventos.Persistence.Contratos;
using ProEventos.Persistense.Contextos;

namespace ProEventos.API
{
    public class Startup //CRIANDO PRIMEIRA ALTERAÇÃO NO GIT
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration; //INJEÇÃO DA CLASSE DENTRO DO PROGRAM.CS, FEITO PARA ACESSAR O APPSETINGS
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ProEventosContext>( //  INJEÇÃO DE DEPENDENCIA DO DATACONTEXT
                context => context.UseSqlite(Configuration.GetConnectionString("Default"))); //REFERENCIA AO BANCO DE DADOS, GETCONNECTION RECEBE A BASE DE DADOS POR PARAMETRO
            services.AddControllers()
                    .AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling =
                       Newtonsoft.Json.ReferenceLoopHandling.Ignore
                    );
            services.AddScoped<IEventoService, EventoService>(); //INJÇÃO DE DEPENDENCIA DAS ITERFACES
            services.AddScoped<IGeralPersist, GeralPersist>();
            services.AddScoped<IEventoPersist, EventosPersist>();
            services.AddCors(); //adicionando cors
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProEventos.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProEventos.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors(x => x.AllowAnyHeader() //autorizando as requisições do front CORS policy: No 'Access-Control-Allow-Origin' - qualquer cabeçalho
                               .AllowAnyMethod() //qualquer metodo (get, set, post, delete)
                               .AllowAnyOrigin()); //qualquer origem

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
