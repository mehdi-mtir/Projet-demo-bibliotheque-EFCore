namespace Bibliotheque.Demo;

public class Livre
{
    public int Id { get; set; }                       
    public string Titre { get; set; } = string.Empty;
    public DateTime DateParution { get; set; }
    public string Genre { get; set; } = string.Empty;
    public bool Disponible { get; set; } = true;

    public int AuteurId { get; set; }                  
    public Auteur Auteur { get; set; } = null!;        
}
