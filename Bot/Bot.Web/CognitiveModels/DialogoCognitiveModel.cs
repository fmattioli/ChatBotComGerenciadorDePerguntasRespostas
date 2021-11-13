using Microsoft.Bot.Builder;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bot.Web.CognitiveModels
{
    public class DialogoCognitiveModel : IRecognizerConvert
    {
        public string Text { get; set; }
        public string AlteredText { get; set; }
        public string Intencao { get; set; }


        public void Convert(dynamic result)
        {
            var app = JsonConvert.DeserializeObject<DialogoCognitiveModel>(JsonConvert.SerializeObject(result, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
            Text = app.Text;
            AlteredText = app.AlteredText;
            foreach (var item in result.Intents)
            {
                Intencao = item.Key;
            }
            
        }

    }
}
