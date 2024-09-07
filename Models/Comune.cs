namespace ApiRestNETcore_BaseExample.Models
{
    public class Comune
    {
        public string? comune { get; set; }
        public string? regione { get; set; }
        public string? provincia { get; set; }
        public string? prefisso { get; set; }
        public int? numeroresidenti { get; set; }
    }

    public class ListaComuni
    {
        public string? response_status { get; set; }
        public string? response_error { get; set; }
        public List<Comune>? Comuni { get; set; }
    }
}
