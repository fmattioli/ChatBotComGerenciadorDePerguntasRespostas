using Bot.Aplicacao.Interfaces;
using Bot.Aplicacao.Servicos;
using Bot.Dominio.Interfaces;
using Bot.Infra.Data.Config;
using Bot.Infra.Data.Repositorio;
using Microsoft.Extensions.DependencyInjection;

namespace Bot.Infra.IoC
{
    public static class DependecyInjection
    {
        public static IServiceCollection AdicionarInfraEstruturaBot(this IServiceCollection services)
        {
            //Add injeção de classes
            services.AddSingleton<IComumServico, ComumServico>();
            services.AddSingleton<DbSession>();
            services.AddSingleton<ICardsServico, CardsServico>();
            services.AddSingleton<ICardsRepositorio, CardsRepositorio>();
            services.AddSingleton<IIntencaoServico, IntencaoServico>();
            services.AddSingleton<IIntencoesRepositorio, IntencoesRepositorio>();
            services.AddSingleton<IPerguntasServico, PerguntasServico>();
            services.AddSingleton<IPerguntasRepositorio, PerguntasRepositorio>();
            services.AddSingleton<IRespostasServico, RespostasServico>();
            services.AddSingleton<IRespostasRepositorio, RespostasRepositorio>();
            services.AddSingleton<IMensagemInicioServico, MensagemInicioServico>();
            services.AddSingleton<IMensagemInicioRepositorio, MensagemInicioRepositorio>();

            return services;
        }


        public static IServiceCollection AdicionarInfraEstruturaGerenciador(this IServiceCollection services)
        {
            //Add injeção de classes
            services.AddScoped<DbSession>();
            services.AddScoped<IUsuarioServico, UsuarioServico>();
            services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            services.AddScoped<IEspecialidadeServico, EspecialidadeServico>();
            services.AddScoped<IEspecialidadeRepositorio, EspecialidadeRepositorio>();
            services.AddScoped<IIntencaoServico, IntencaoServico>();
            services.AddScoped<IIntencoesRepositorio, IntencoesRepositorio>();
            services.AddScoped<IPerguntasServico, PerguntasServico>();
            services.AddScoped<IPerguntasRepositorio, PerguntasRepositorio>();
            services.AddScoped<IRespostasServico, RespostasServico>();
            services.AddScoped<IRespostasRepositorio, RespostasRepositorio>();
            services.AddScoped<IMensagemInicioServico, MensagemInicioServico>();
            services.AddScoped<IMensagemInicioRepositorio, MensagemInicioRepositorio>();
            services.AddScoped<ICriptografiaServico, CriptografiaService>();

            return services;
        }
    }


}
