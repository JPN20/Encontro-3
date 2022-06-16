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
        // Chamado pelo host antes do m�todo Configure para configurar os servi�os do aplicativo.
        public void ConfigureServices(IServiceCollection services)
        {
            // adiciona os servi�os necess�rios para 
            services.AddControllers();
            // se n�o existir uma inst�ncia na mem�ria da aplica��o, cria um novo
            services.AddScoped<ChapterContext, ChapterContext>();
            // a cada solicita��o, uma nova inst�ncia � criada
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
                        // Valida quem est� solicitando
                        ValidateIssuer = true,

                        // Valida quem est� recebendo
                        ValidateAudience = true,

                        // Define se o tempo de expiracao sera validado
                        ValidateLifetime = true,

                        // Forma de criptografia e ainda valida a chave de autenticacaoo
                        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("chapter-chave-autenticacao")),

                        // Valida o tempo de expiracao do token
                        ClockSkew = TimeSpan.FromMinutes(30),

                        // Nome do issuer, de onde est� vindo
                        ValidIssuer = "chapter.webapi",

                        // Nome do audience, para onde est� indo
                        ValidAudience = "chapter.webapi"
                   };
               });
        }

        // O m�todo Configure � usado para especificar como o aplicativo responde �s solicita��es HTTP.
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
