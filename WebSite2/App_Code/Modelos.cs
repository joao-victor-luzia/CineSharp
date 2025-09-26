using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace WebSite1
{
    public class Filme
    {
        [JsonProperty("Title")]
        public string Titulo { get; set; }

        [JsonProperty("Year")]
        public string Ano { get; set; }

        public string imdbID { get; set; }

        [JsonProperty("Poster")]
        public string PosterURL { get; set; }

    }

    public class ResultadoBuscaFilme
    {
        [JsonProperty("Search")]
        public List<Filme> Filmes { get; set; }

        public string Response { get; set; }
    }
}
