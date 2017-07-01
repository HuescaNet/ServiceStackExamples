using ServiceStack;

namespace AutoQuery
{
    class AutoQueryConCamposPropiosRequest : QueryDb<OrderLine>
    {
        [QueryDbField(Operand = "=", Field = "Description")]
        public string Descripcion { get; set; }
    }
}