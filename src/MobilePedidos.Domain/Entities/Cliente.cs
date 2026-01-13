namespace MobilePedidos.Domain.Entities
{
    public class Cliente
    {
        public int id_ctcoa { get; set; }
        public string ccod_coa { get; set; }
        public string ctipo_coa { get; set; }
        public string cdsc_coa { get; set; }
        public string ca_paterno { get; set; }
        public string ca_materno { get; set; }
        public string cnombres { get; set; }
        public string cdireccion { get; set; }
        public string ctelef1 { get; set; }
        public string cmon_cred { get; set; }
        public string cforma_pago { get; set; }
        public string ccod_vend { get; set; }
        public string cmail1 { get; set; }
        public bool btranportista { get; set; }
    }
}
