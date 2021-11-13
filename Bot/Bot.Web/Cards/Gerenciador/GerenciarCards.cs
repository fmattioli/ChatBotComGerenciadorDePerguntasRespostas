using Bot.Aplicacao.ViewModels;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Bot.Web.Cards.Gerenciador
{
    public class GerenciarCards
    {

        public PromptOptions RetornarAdaptiveCard(IList<string> adaptives)
        {
            var lista = new List<Attachment>();
            foreach (var item in adaptives)
            {
                lista.Add(CriarAttachmentFromFile(item));
            }

            var opts = new PromptOptions
            {
                Prompt = new Activity
                {
                    AttachmentLayout = AttachmentLayoutTypes.Carousel,
                    Attachments = lista,
                    Type = ActivityTypes.Message,
                },
            };



            return opts;
        }

        public PromptOptions RetornarAdaptiveCard(IList<CardsViewModel> cards)
        {
            var lista = new List<Attachment>();
            foreach (var item in cards)
            {
                lista.Add(CriarAttachment(item.ConteudoJson));
            }

            var opts = new PromptOptions
            {
                Prompt = new Activity
                {
                    AttachmentLayout = AttachmentLayoutTypes.Carousel,
                    Attachments = lista,
                    Type = ActivityTypes.Message,
                },
            };



            return opts;
        }

        public Attachment CriarAttachmentFromFile(string adaptiveName)
        {
            var cardResourcePath = GetType().Assembly.GetManifestResourceNames().First(name => name.EndsWith(adaptiveName + ".json"));
            using (var stream = GetType().Assembly.GetManifestResourceStream(cardResourcePath))
            {
                using (var reader = new StreamReader(stream))
                {
                    var adaptiveCard = reader.ReadToEnd();
                    return new Attachment()
                    {
                        ContentType = "application/vnd.microsoft.card.adaptive",
                        Content = JsonConvert.DeserializeObject(adaptiveCard),
                    };
                }
            }
        }

        public Attachment CriarAttachment(string adaptiveCard)
        {
            return new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject(adaptiveCard),
            };


        }
    }
}
