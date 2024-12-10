namespace Arbk.Models;

internal class Business
{
    public TeDhenatBiznesit TeDhenatBiznesit { get; set; }
    public List<Pronaret> Pronaret { get; set; }
    public List<Perfaqesuesit> Perfaqesuesit { get; set; }
    public List<Aktivitetet> Aktivitetet { get; set; }
    public List<object> Njesite { get; set; }
    public object Gabim { get; set; }
}
