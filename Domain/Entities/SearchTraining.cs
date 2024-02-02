namespace RpaAeC.Domain.Entities
{
    public class SearchTraining
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Professor { get; set; } = string.Empty;
        public string CargaHoraria { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
    }
}
