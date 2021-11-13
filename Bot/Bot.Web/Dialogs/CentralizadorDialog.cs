// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio CoreBot v4.14.0

using Bot.Aplicacao.ViewModels;
using Bot.Web.Cards.Gerenciador;
using Bot.Web.CognitiveModels;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Logging;
using Microsoft.Recognizers.Text.DataTypes.TimexExpression;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bot.Web.Dialogs
{
    public class CentralizadorDialog : ComponentDialog
    {
        protected readonly ILogger Logger;

        // Dependency injection uses this constructor to instantiate MainDialog
        public CentralizadorDialog(Dialogo dialogo, ILogger<CentralizadorDialog> logger)
            : base(nameof(CentralizadorDialog))
        {
            Logger = logger;
            AddDialog(new TextPrompt(nameof(TextPrompt)));
            AddDialog(dialogo);
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), new WaterfallStep[]
            {
                DirecionarUsuarioParaEspecialidadeCorreta
            }));

            // The initial child Dialog to run.
            InitialDialogId = nameof(WaterfallDialog);
        }

        private async Task<DialogTurnResult> DirecionarUsuarioParaEspecialidadeCorreta(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var especialidade = new EspecialidadeViewModel
            {
                Descricao = EspecialidadeViewModelTransicao.Descricao,
                Nome = EspecialidadeViewModelTransicao.Nome
            };


            return await stepContext.BeginDialogAsync(nameof(Dialogo), especialidade, cancellationToken);
        }

    }
}
