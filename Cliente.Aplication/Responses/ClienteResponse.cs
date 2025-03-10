namespace Cliente.Aplication.Responses;

public class ClienteResponse
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public DateTime FechaNacimiento { get; set; }
    public int Edad { get; set; }
    public string EMail { get; set; }
    public DateTime FechaAlta { get; set; }
}
