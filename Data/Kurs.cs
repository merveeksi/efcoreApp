namespace efcoreApp.Data;

public class Kurs
{
    public int KursId { get; set; }
    public string? Baslik { get; set; }

    public int OgretmenId { get; set; }

    public Ogretmen Ogretmen { get; set; } = null!; //çoklu seçenek için

    public ICollection<KursKayit> KursKayitlari { get; set; } = new List<KursKayit>();

}