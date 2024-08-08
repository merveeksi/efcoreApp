namespace efcoreApp.Data;

public class Kurs
{
    public int KursId { get; set; }

    public string? Baslik { get; set; }
    
    public List<KursKayit> Kurskayitlar { get; set; }
}