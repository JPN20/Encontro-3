using Chapter.WebApi.Contexts;
using Chapter.WebApi.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;

namespace Chapter.WebApi
{
    public class Startup
    {
        // Chamado pelo host antes do método Configure para configurar os serviços do aplicativo.
        public void ConfigureServices(IServiceCollection services)
        {
            // adiciona os serviços necessários para 
            services.AddControllers();
            // se não existir uma instância na memória da aplicação, cria um novo
            services.AddScoped<ChapterContext, ChapterContext>();
            // a cada solicitação, uma nova instância é criada
            services.AddTransient<LivroRepository, LivroRepository>();
            services.AddTransient<UsuarioRepository, UsuarioRepository>();
            // configura o swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ChapterApi", Version = "v1" });
            });

            services
               // Define a forma de autenticacao
               .AddAuthentication(options =>
               {
                   options.DefaultAuthenticateScheme = "JwtBearer";
                   options.DefaultChallengeScheme = "JwtBearer";
               })

               // Define os parametros de validacaoo do token
               .AddJwtBearer("JwtBearer", options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                        // Valida quem está solicitando
                        ValidateIssuer = true,

                        // Valida quem está recebendo
                        ValidateAudience = true,

                        // Define se o tempo de expiracao sera validado
                        ValidateLifetime = true,

                        // Forma de criptografia e ainda valida a chave de autenticacaoo
                        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("chapter-chave-autenticacao")),

                        // Valida o tempo de expiracao do token
                        ClockSkew = TimeSpan.FromMinutes(30),

                        // Nome do issuer, de onde está vindo
                        ValidIssuer = "chapter.webapi",

                        // Nome do audience, para onde está indo
                        ValidAudience = "chapter.webapi"
                   };
               });
        }

        // O método Configure é usado para especificar como o aplicativo responde às solicitações HTTP.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                // ativa o middleware para uso do swagger
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ChapterApi v1"));
            }

            app.UseRouting();

            // Habilita a autenticacao
            app.UseAuthentication();

            // Habilita a autorizacao
            app.UseAuthorization();

            // mapear os controller
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
